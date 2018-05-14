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
using FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto;
using FuelWerx.Administrative.EmergencyDeliveryFeeRules.Exporting;
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

namespace FuelWerx.Administrative.EmergencyDeliveryFeeRules
{
	[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFeeRules" })]
	public class EmergencyDeliveryFeeRuleAppService : FuelWerxAppServiceBase, IEmergencyDeliveryFeeRuleAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<EmergencyDeliveryFeeRule, long> _emergencyDeliveryFeeRuleRepository;

		private readonly IEmergencyDeliveryFeeRuleListExcelExporter _emergencyDeliveryFeeRuleListExcelExporter;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public EmergencyDeliveryFeeRuleAppService(IRepository<EmergencyDeliveryFeeRule, long> emergencyDeliveryFeeRuleRepository, IEmergencyDeliveryFeeRuleListExcelExporter emergencyDeliveryFeeRuleListExcelExporter, IBinaryObjectManager binaryObjectManager)
		{
			this._emergencyDeliveryFeeRuleRepository = emergencyDeliveryFeeRuleRepository;
			this._emergencyDeliveryFeeRuleListExcelExporter = emergencyDeliveryFeeRuleListExcelExporter;
			this._binaryObjectManager = binaryObjectManager;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFeeRules.Create", "Pages.Administration.EmergencyDeliveryFeeRules.Edit" })]
		public async Task<long> CreateOrUpdateEmergencyDeliveryFeeRule(CreateOrUpdateEmergencyDeliveryFeeRuleInput input)
		{
			long value;
			if (!input.EmergencyDeliveryFeeRule.Id.HasValue)
			{
				long num = await this._emergencyDeliveryFeeRuleRepository.InsertAndGetIdAsync(input.EmergencyDeliveryFeeRule.MapTo<EmergencyDeliveryFeeRule>());
				value = num;
			}
			else
			{
				value = input.EmergencyDeliveryFeeRule.Id.Value;
				await this._emergencyDeliveryFeeRuleRepository.UpdateAsync(input.EmergencyDeliveryFeeRule.MapTo<EmergencyDeliveryFeeRule>());
			}
			return value;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFeeRules.Delete" })]
		public async Task DeleteEmergencyDeliveryFeeRule(IdInput<long> input)
		{
			await this._emergencyDeliveryFeeRuleRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFeeRules.Edit" })]
		public async Task<GetEmergencyDeliveryFeeRuleForEditOutput> GetEmergencyDeliveryFeeRuleForEdit(NullableIdInput<long> input)
		{
			EmergencyDeliveryFeeRuleEditDto emergencyDeliveryFeeRuleEditDto;
			if (!input.Id.HasValue)
			{
				emergencyDeliveryFeeRuleEditDto = new EmergencyDeliveryFeeRuleEditDto();
			}
			else
			{
				IRepository<EmergencyDeliveryFeeRule, long> repository = this._emergencyDeliveryFeeRuleRepository;
				EmergencyDeliveryFeeRule async = await repository.GetAsync(input.Id.Value);
				emergencyDeliveryFeeRuleEditDto = async.MapTo<EmergencyDeliveryFeeRuleEditDto>();
			}
			return new GetEmergencyDeliveryFeeRuleForEditOutput()
			{
				EmergencyDeliveryFeeRule = emergencyDeliveryFeeRuleEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFeeRules" })]
		public async Task<PagedResultOutput<EmergencyDeliveryFeeRuleListDto>> GetEmergencyDeliveryFeeRules(GetEmergencyDeliveryFeeRulesInput input)
		{
			IQueryable<EmergencyDeliveryFeeRule> all = this._emergencyDeliveryFeeRuleRepository.GetAll();
			IQueryable<EmergencyDeliveryFeeRule> emergencyDeliveryFeeRules = all.WhereIf<EmergencyDeliveryFeeRule>(!input.Filter.IsNullOrEmpty(), (EmergencyDeliveryFeeRule p) => p.Name.Contains(input.Filter) || p.Caption.Contains(input.Filter));
			int num = await emergencyDeliveryFeeRules.CountAsync<EmergencyDeliveryFeeRule>();
			List<EmergencyDeliveryFeeRule> listAsync = await emergencyDeliveryFeeRules.OrderBy<EmergencyDeliveryFeeRule>(input.Sorting, new object[0]).PageBy<EmergencyDeliveryFeeRule>(input).ToListAsync<EmergencyDeliveryFeeRule>();
			return new PagedResultOutput<EmergencyDeliveryFeeRuleListDto>(num, listAsync.MapTo<List<EmergencyDeliveryFeeRuleListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFeeRules.ExportData" })]
		public async Task<FileDto> GetEmergencyDeliveryFeeRulesToExcel()
		{
			List<EmergencyDeliveryFeeRule> allListAsync = await this._emergencyDeliveryFeeRuleRepository.GetAllListAsync();
			List<EmergencyDeliveryFeeRuleListDto> emergencyDeliveryFeeRuleListDtos = allListAsync.MapTo<List<EmergencyDeliveryFeeRuleListDto>>();
			return this._emergencyDeliveryFeeRuleListExcelExporter.ExportToFile(emergencyDeliveryFeeRuleListDtos);
		}
	}
}