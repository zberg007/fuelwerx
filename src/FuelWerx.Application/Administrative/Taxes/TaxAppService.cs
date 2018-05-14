using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Taxes.Dto;
using FuelWerx.Administrative.Taxes.Exporting;
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

namespace FuelWerx.Administrative.Taxes
{
	[AbpAuthorize(new string[] { "Pages.Administration.Taxes" })]
	public class TaxAppService : FuelWerxAppServiceBase, ITaxAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Tax, long> _taxRepository;

		private readonly ITaxListExcelExporter _taxListExcelExporter;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public TaxAppService(IRepository<Tax, long> taxRepository, ITaxListExcelExporter taxListExcelExporter, IBinaryObjectManager binaryObjectManager)
		{
			this._taxRepository = taxRepository;
			this._taxListExcelExporter = taxListExcelExporter;
			this._binaryObjectManager = binaryObjectManager;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Taxes.Create", "Pages.Administration.Taxes.Edit" })]
		public async Task<long> CreateOrUpdateTax(CreateOrUpdateTaxInput input)
		{
			long value;
			if (!input.Tax.Id.HasValue)
			{
				long num = await this._taxRepository.InsertAndGetIdAsync(input.Tax.MapTo<Tax>());
				value = num;
			}
			else
			{
				value = input.Tax.Id.Value;
				await this._taxRepository.UpdateAsync(input.Tax.MapTo<Tax>());
			}
			return value;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Taxes.Delete" })]
		public async Task DeleteTax(IdInput<long> input)
		{
			await this._taxRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Taxes" })]
		public async Task<PagedResultOutput<TaxListDto>> GetTaxes(GetTaxesInput input)
		{
			decimal num;
			bool flag = decimal.TryParse(input.Filter, out num);
			IQueryable<Tax> all = this._taxRepository.GetAll();
			IQueryable<Tax> taxes = all.WhereIf<Tax>(!input.Filter.IsNullOrEmpty(), (Tax p) => p.Name.Contains(input.Filter) || p.Caption.Contains(input.Filter));
			if (flag)
			{
				IQueryable<Tax> all1 = this._taxRepository.GetAll();
				taxes = all1.WhereIf<Tax>(true, (Tax p) => p.Rate == num);
			}
			int num1 = await taxes.CountAsync<Tax>();
			List<Tax> listAsync = await taxes.OrderBy<Tax>(input.Sorting, new object[0]).PageBy<Tax>(input).ToListAsync<Tax>();
			return new PagedResultOutput<TaxListDto>(num1, listAsync.MapTo<List<TaxListDto>>());
		}

		public async Task<List<Tax>> GetTaxesForTaxRules()
		{
			IRepository<Tax, long> repository = this._taxRepository;
			List<Tax> allListAsync = await repository.GetAllListAsync((Tax m) => (int?)m.TenantId == this.AbpSession.TenantId && m.IsActive);
			return allListAsync;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Taxes.ExportData" })]
		public async Task<FileDto> GetTaxesToExcel()
		{
			List<Tax> allListAsync = await this._taxRepository.GetAllListAsync();
			List<TaxListDto> taxListDtos = allListAsync.MapTo<List<TaxListDto>>();
			return this._taxListExcelExporter.ExportToFile(taxListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Taxes.Edit" })]
		public async Task<GetTaxForEditOutput> GetTaxForEdit(NullableIdInput<long> input)
		{
			TaxEditDto taxEditDto;
			if (!input.Id.HasValue)
			{
				taxEditDto = new TaxEditDto();
			}
			else
			{
				IRepository<Tax, long> repository = this._taxRepository;
				Tax async = await repository.GetAsync(input.Id.Value);
				taxEditDto = async.MapTo<TaxEditDto>();
			}
			return new GetTaxForEditOutput()
			{
				Tax = taxEditDto
			};
		}
	}
}