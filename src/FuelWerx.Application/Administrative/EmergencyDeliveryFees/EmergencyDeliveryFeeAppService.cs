using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using FuelWerx;
using FuelWerx.Administrative;
using FuelWerx.Administrative.EmergencyDeliveryFees.Dto;
using FuelWerx.Administrative.EmergencyDeliveryFees.Exporting;
using FuelWerx.Dto;
using FuelWerx.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.EmergencyDeliveryFees
{
	[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFees" })]
	public class EmergencyDeliveryFeeAppService : FuelWerxAppServiceBase, IEmergencyDeliveryFeeAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<EmergencyDeliveryFee, long> _emergencyDeliveryFeeRepository;

		private readonly IEmergencyDeliveryFeeListExcelExporter _emergencyDeliveryFeeListExcelExporter;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public EmergencyDeliveryFeeAppService(IRepository<EmergencyDeliveryFee, long> emergencyDeliveryFeeRepository, IEmergencyDeliveryFeeListExcelExporter emergencyDeliveryFeeListExcelExporter, IBinaryObjectManager binaryObjectManager)
		{
			this._emergencyDeliveryFeeRepository = emergencyDeliveryFeeRepository;
			this._emergencyDeliveryFeeListExcelExporter = emergencyDeliveryFeeListExcelExporter;
			this._binaryObjectManager = binaryObjectManager;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFees.Create", "Pages.Administration.EmergencyDeliveryFees.Edit" })]
		public async Task<long> CreateOrUpdateEmergencyDeliveryFee(CreateOrUpdateEmergencyDeliveryFeeInput input)
		{
			long value;
			if (!input.EmergencyDeliveryFee.Id.HasValue)
			{
				long num = await this._emergencyDeliveryFeeRepository.InsertAndGetIdAsync(input.EmergencyDeliveryFee.MapTo<EmergencyDeliveryFee>());
				value = num;
			}
			else
			{
				value = input.EmergencyDeliveryFee.Id.Value;
				await this._emergencyDeliveryFeeRepository.UpdateAsync(input.EmergencyDeliveryFee.MapTo<EmergencyDeliveryFee>());
			}
			return value;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFees.Delete" })]
		public async Task DeleteEmergencyDeliveryFee(IdInput<long> input)
		{
			await this._emergencyDeliveryFeeRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFees.Edit" })]
		public async Task<GetEmergencyDeliveryFeeForEditOutput> GetEmergencyDeliveryFeeForEdit(NullableIdInput<long> input)
		{
			EmergencyDeliveryFeeEditDto emergencyDeliveryFeeEditDto;
			if (!input.Id.HasValue)
			{
				emergencyDeliveryFeeEditDto = new EmergencyDeliveryFeeEditDto();
			}
			else
			{
				IRepository<EmergencyDeliveryFee, long> repository = this._emergencyDeliveryFeeRepository;
				EmergencyDeliveryFee async = await repository.GetAsync(input.Id.Value);
				emergencyDeliveryFeeEditDto = async.MapTo<EmergencyDeliveryFeeEditDto>();
			}
			return new GetEmergencyDeliveryFeeForEditOutput()
			{
				EmergencyDeliveryFee = emergencyDeliveryFeeEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFees" })]
		public async Task<PagedResultOutput<EmergencyDeliveryFeeListDto>> GetEmergencyDeliveryFees(GetEmergencyDeliveryFeesInput input)
		{
			decimal num;
			bool flag = decimal.TryParse(input.Filter, out num);
			IQueryable<EmergencyDeliveryFee> all = this._emergencyDeliveryFeeRepository.GetAll();
			IQueryable<EmergencyDeliveryFee> emergencyDeliveryFees = all.WhereIf<EmergencyDeliveryFee>(!input.Filter.IsNullOrEmpty(), (EmergencyDeliveryFee p) => p.Name.Contains(input.Filter) || p.Caption.Contains(input.Filter) || p.Zone.Name.Contains(input.Filter) || p.Zone.Caption.Contains(input.Filter));
			if (flag)
			{
				IQueryable<EmergencyDeliveryFee> all1 = this._emergencyDeliveryFeeRepository.GetAll();
				IQueryable<EmergencyDeliveryFee> emergencyDeliveryFees1 = System.Data.Entity.QueryableExtensions.Include<EmergencyDeliveryFee, Zone>(all1, (EmergencyDeliveryFee m) => m.Zone);
				emergencyDeliveryFees = emergencyDeliveryFees1.WhereIf<EmergencyDeliveryFee>(true, (EmergencyDeliveryFee p) => p.Fee == num);
			}
			int num1 = await emergencyDeliveryFees.CountAsync<EmergencyDeliveryFee>();
			List<EmergencyDeliveryFee> listAsync = await emergencyDeliveryFees.OrderBy<EmergencyDeliveryFee>(input.Sorting, new object[0]).PageBy<EmergencyDeliveryFee>(input).ToListAsync<EmergencyDeliveryFee>();
			return new PagedResultOutput<EmergencyDeliveryFeeListDto>(num1, listAsync.MapTo<List<EmergencyDeliveryFeeListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFees.ExportData" })]
		public async Task<FileDto> GetEmergencyDeliveryFeesToExcel()
		{
			List<EmergencyDeliveryFee> allListAsync = await this._emergencyDeliveryFeeRepository.GetAllListAsync();
			List<EmergencyDeliveryFeeListDto> emergencyDeliveryFeeListDtos = allListAsync.MapTo<List<EmergencyDeliveryFeeListDto>>();
			return this._emergencyDeliveryFeeListExcelExporter.ExportToFile(emergencyDeliveryFeeListDtos);
		}
	}
}