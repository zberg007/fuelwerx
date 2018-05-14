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
using FuelWerx.Administrative.TaxRules.Dto;
using FuelWerx.Administrative.TaxRules.Exporting;
using FuelWerx.Dto;
using FuelWerx.Generic;
using FuelWerx.Products;
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

namespace FuelWerx.Administrative.TaxRules
{
	[AbpAuthorize(new string[] { "Pages.Administration.TaxRules" })]
	public class TaxRuleAppService : FuelWerxAppServiceBase, ITaxRuleAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Tax, long> _taxRepository;

		private readonly IRepository<TaxRule, long> _taxRuleRepository;

		private readonly IRepository<Country> _countryRepository;

		private readonly IRepository<CountryRegion> _countryRegionRepository;

		private readonly IRepository<TaxRuleRule, long> _taxRuleRuleRepository;

		private readonly IRepository<ProductPriceTaxRule, long> _productPriceTaxRuleRepository;

		private readonly ITaxRuleListExcelExporter _taxRuleListExcelExporter;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public TaxRuleAppService(IRepository<TaxRule, long> taxRuleRepository, IRepository<Tax, long> taxRepository, IRepository<TaxRuleRule, long> taxRuleRuleRepository, IRepository<Country> countryRepository, IRepository<CountryRegion> countryRegionRepository, IRepository<ProductPriceTaxRule, long> productPriceTaxRuleRepository, ITaxRuleListExcelExporter taxRuleListExcelExporter, IBinaryObjectManager binaryObjectManager)
		{
			this._taxRepository = taxRepository;
			this._taxRuleRepository = taxRuleRepository;
			this._taxRuleRuleRepository = taxRuleRuleRepository;
			this._countryRepository = countryRepository;
			this._countryRegionRepository = countryRegionRepository;
			this._productPriceTaxRuleRepository = productPriceTaxRuleRepository;
			this._taxRuleListExcelExporter = taxRuleListExcelExporter;
			this._binaryObjectManager = binaryObjectManager;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.TaxRules.Create", "Pages.Administration.TaxRules.Edit" })]
		public async Task<long> CreateOrUpdateTaxRule(CreateOrUpdateTaxRuleInput input)
		{
			long value;
			if (!input.TaxRule.Id.HasValue)
			{
				long num = await this._taxRuleRepository.InsertAndGetIdAsync(input.TaxRule.MapTo<TaxRule>());
				value = num;
			}
			else
			{
				value = input.TaxRule.Id.Value;
				await this._taxRuleRepository.UpdateAsync(input.TaxRule.MapTo<TaxRule>());
			}
			return value;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.TaxRules.Create", "Pages.Administration.TaxRules.Edit" })]
		public async Task<long> CreateOrUpdateTaxRuleRule(CreateOrUpdateTaxRuleRuleInput input)
		{
			long value;
			if (!input.TaxRuleRule.Id.HasValue)
			{
				long num = await this._taxRuleRuleRepository.InsertAndGetIdAsync(input.TaxRuleRule.MapTo<TaxRuleRule>());
				value = num;
			}
			else
			{
				value = input.TaxRuleRule.Id.Value;
				await this._taxRuleRuleRepository.UpdateAsync(input.TaxRuleRule.MapTo<TaxRuleRule>());
			}
			return value;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.TaxRules.Delete" })]
		public async Task<bool> DeleteTaxRule(IdInput<long> input)
		{
			bool flag = true;
			IRepository<ProductPriceTaxRule, long> repository = this._productPriceTaxRuleRepository;
			List<ProductPriceTaxRule> allListAsync = await repository.GetAllListAsync((ProductPriceTaxRule x) => x.TaxRuleId == input.Id);
			if (!allListAsync.Any<ProductPriceTaxRule>())
			{
				await this._taxRuleRepository.DeleteAsync(input.Id);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.TaxRules.Delete" })]
		public async Task DeleteTaxRuleRule(IdInput<long> input)
		{
			await this._taxRuleRuleRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.TaxRules.Edit" })]
		public async Task<GetTaxRuleForEditOutput> GetTaxRuleForEdit(NullableIdInput<long> input)
		{
			TaxRuleEditDto taxRuleEditDto;
			if (!input.Id.HasValue)
			{
				taxRuleEditDto = new TaxRuleEditDto();
			}
			else
			{
				IRepository<TaxRule, long> repository = this._taxRuleRepository;
				TaxRule async = await repository.GetAsync(input.Id.Value);
				taxRuleEditDto = async.MapTo<TaxRuleEditDto>();
			}
			return new GetTaxRuleForEditOutput()
			{
				TaxRule = taxRuleEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Administration.TaxRules.Edit" })]
		public async Task<GetTaxRuleRuleForEditOutput> GetTaxRuleRuleForEdit(NullableIdInput<long> input)
		{
			TaxRuleRuleEditDto taxRuleRuleEditDto;
			if (!input.Id.HasValue)
			{
				taxRuleRuleEditDto = new TaxRuleRuleEditDto();
			}
			else
			{
				IRepository<TaxRuleRule, long> repository = this._taxRuleRuleRepository;
				TaxRuleRule async = await repository.GetAsync(input.Id.Value);
				taxRuleRuleEditDto = async.MapTo<TaxRuleRuleEditDto>();
			}
			return new GetTaxRuleRuleForEditOutput()
			{
				TaxRuleRule = taxRuleRuleEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Administration.TaxRules" })]
		public async Task<PagedResultOutput<TaxRuleRuleListDto>> GetTaxRuleRules(GetTaxRuleRulesInput input)
		{
			bool flag;
			IQueryable<TaxRuleRule> all = this._taxRuleRuleRepository.GetAll();
			flag = (!input.TaxRuleId.HasValue ? false : input.TaxRuleId.Value > (long)0);
			IQueryable<TaxRuleRule> taxRuleRules = all.WhereIf<TaxRuleRule>(flag, (TaxRuleRule p) => p.TaxRuleId == input.TaxRuleId.Value);
			int num = await taxRuleRules.CountAsync<TaxRuleRule>();
			List<TaxRuleRule> listAsync = await taxRuleRules.OrderBy<TaxRuleRule>(input.Sorting, new object[0]).ToListAsync<TaxRuleRule>();
			return new PagedResultOutput<TaxRuleRuleListDto>(num, listAsync.MapTo<List<TaxRuleRuleListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Administration.TaxRules" })]
		public async Task<PagedResultOutput<TaxRuleListDto>> GetTaxRules(GetTaxRulesInput input)
		{
			IQueryable<TaxRule> all = this._taxRuleRepository.GetAll();
			IQueryable<TaxRule> taxRules = all.WhereIf<TaxRule>(!input.Filter.IsNullOrEmpty(), (TaxRule p) => p.Name.Contains(input.Filter) || p.Caption.Contains(input.Filter));
			int num = await taxRules.CountAsync<TaxRule>();
			List<TaxRule> listAsync = await taxRules.OrderBy<TaxRule>(input.Sorting, new object[0]).PageBy<TaxRule>(input).ToListAsync<TaxRule>();
			return new PagedResultOutput<TaxRuleListDto>(num, listAsync.MapTo<List<TaxRuleListDto>>());
		}

		public async Task<ListResultDto<TaxRuleListDto>> GetTaxRulesByTenantId(int tenantId, bool active)
		{
			IQueryable<TaxRule> all = this._taxRuleRepository.GetAll();
			IQueryable<TaxRule> taxRules = 
				from p in all
				where p.TenantId == tenantId
				select p;
			bool flag = active;
			List<TaxRule> listAsync = await taxRules.WhereIf<TaxRule>(flag, (TaxRule p) => p.IsActive == active).OrderBy<TaxRule>("Name", new object[0]).ToListAsync<TaxRule>();
			return new ListResultDto<TaxRuleListDto>(listAsync.MapTo<List<TaxRuleListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Administration.TaxRules.ExportData" })]
		public async Task<FileDto> GetTaxRulesToExcel()
		{
			List<TaxRule> allListAsync = await this._taxRuleRepository.GetAllListAsync();
			List<TaxRuleListDto> taxRuleListDtos = allListAsync.MapTo<List<TaxRuleListDto>>();
			return this._taxRuleListExcelExporter.ExportToFile(taxRuleListDtos);
		}
	}
}