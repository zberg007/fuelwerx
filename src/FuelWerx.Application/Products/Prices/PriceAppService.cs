using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Dto;
using FuelWerx.Products;
using FuelWerx.Products.Prices.Dto;
using FuelWerx.Products.Prices.Exporting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Products.Prices
{
	[AbpAuthorize(new string[] { "Pages.Tenant.Products" })]
	public class PriceAppService : FuelWerxAppServiceBase, IPriceAppService, IApplicationService, ITransientDependency
	{
		private readonly IProductAppService _productAppService;

		private readonly IRepository<ProductPrice, long> _priceRepository;

		private readonly IPriceListExcelExporter _priceListExcelExporter;

		public PriceAppService(IRepository<ProductPrice, long> priceRepository, IPriceListExcelExporter priceListExcelExporter, IProductAppService productAppService)
		{
			this._priceRepository = priceRepository;
			this._priceListExcelExporter = priceListExcelExporter;
			this._productAppService = productAppService;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task<bool> CreateOrUpdateProductPrice(CreateOrUpdateProductPriceInput input)
		{
			DateTime? markedHistoricalAt;
			long value;
			long num;
			bool flag = false;
			long productId = (long)-1;
			bool flag1 = false;
			if (input.Price.Historical)
			{
				markedHistoricalAt = input.Price.MarkedHistoricalAt;
				if (markedHistoricalAt.HasValue)
				{
					goto Label1;
				}
				input.Price.MarkedHistoricalAt = new DateTime?(DateTime.Now);
				goto Label0;
			}
		Label1:
			if (!input.Price.Historical)
			{
				markedHistoricalAt = input.Price.MarkedHistoricalAt;
				if (markedHistoricalAt.HasValue)
				{
					markedHistoricalAt = null;
					input.Price.MarkedHistoricalAt = markedHistoricalAt;
				}
			}
		Label0:
			if (!input.Price.Id.HasValue || input.Price.Id.Value <= (long)0)
			{
				long num1 = await this._priceRepository.InsertAndGetIdAsync(input.Price.MapTo<ProductPrice>());
				num = num1;
			}
			else
			{
				IRepository<ProductPrice, long> repository = this._priceRepository;
				long? id = input.Price.Id;
				ProductPrice async = await repository.GetAsync(id.Value);
				async.Cost = input.Price.Cost;
				async.Historical = input.Price.Historical;
				async.MarkedHistoricalAt = input.Price.MarkedHistoricalAt;
				async.SpecialDeliveryFee = input.Price.SpecialDeliveryFee;
				async.UnitPrice = input.Price.UnitPrice;
				if (async.IsActive && !input.Price.IsActive)
				{
					productId = async.ProductId;
					flag = true;
				}
				async.IsActive = input.Price.IsActive;
				List<long> nums = new List<long>();
				bool flag2 = !input.Price.ProductPriceTaxRules.Any<ProductPriceTaxRule>();
				if (async.ProductPriceTaxRules.Any<ProductPriceTaxRule>())
				{
					foreach (ProductPriceTaxRule productPriceTaxRule in async.ProductPriceTaxRules)
					{
						if (flag2 || !(
							from x in input.Price.ProductPriceTaxRules
							where x.TaxRuleId == productPriceTaxRule.TaxRuleId
							select x).Any<ProductPriceTaxRule>())
						{
							productPriceTaxRule.IsDeleted = true;
							ProductPriceTaxRule nullable = productPriceTaxRule;
							if (this.AbpSession.ImpersonatorUserId.HasValue)
							{
								id = this.AbpSession.ImpersonatorUserId;
								value = id.Value;
							}
							else
							{
								id = this.AbpSession.UserId;
								value = id.Value;
							}
							nullable.DeleterUserId = new long?(value);
							productPriceTaxRule.DeletionTime = new DateTime?(DateTime.Now);
						}
						nums.Add(productPriceTaxRule.TaxRuleId);
					}
				}
				IEnumerable<ProductPriceTaxRule> productPriceTaxRules = 
					from x in input.Price.ProductPriceTaxRules
					where !nums.Contains(x.TaxRuleId)
					select x;
				if (productPriceTaxRules.Any<ProductPriceTaxRule>())
				{
					foreach (ProductPriceTaxRule productPriceTaxRule1 in productPriceTaxRules)
					{
						async.ProductPriceTaxRules.Add(productPriceTaxRule1);
					}
				}
				id = input.Price.Id;
				num = id.Value;
				await this._priceRepository.UpdateAsync(async);
			}
			if (flag && productId > (long)0)
			{
				(await this._productAppService.GetProduct(productId)).IsActive = false;
				flag1 = true;
			}
			if (input.Price.IsActive)
			{
				IRepository<ProductPrice, long> repository1 = this._priceRepository;
				List<ProductPrice> allListAsync = await repository1.GetAllListAsync((ProductPrice x) => x.ProductId == input.Price.ProductId && x.Id != num);
				List<ProductPrice> productPrices = allListAsync;
				if (productPrices.Any<ProductPrice>())
				{
					foreach (ProductPrice productPrice in productPrices)
					{
						productPrice.IsActive = false;
					}
				}
			}
			return flag1;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Delete" })]
		public async Task DeleteProductPrice(IdInput<long> input)
		{
			await this._priceRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products" })]
		public async Task<PagedResultOutput<ProductPriceListDto>> GetPrices(GetProductPricesInput input)
		{
			IQueryable<ProductPrice> all = this._priceRepository.GetAll();
			IQueryable<ProductPrice> productId = all.WhereIf<ProductPrice>(!input.Filter.IsNullOrEmpty(), (ProductPrice p) => p.Cost == decimal.Parse(input.Filter));
			if (input.ProductId > (long)0)
			{
				IQueryable<ProductPrice> productPrices = this._priceRepository.GetAll();
				productId = 
					from p in productPrices
					where p.ProductId == input.ProductId
					select p;
			}
			int num = await productId.CountAsync<ProductPrice>();
			List<ProductPrice> listAsync = await productId.OrderBy<ProductPrice>(input.Sorting, new object[0]).PageBy<ProductPrice>(input).ToListAsync<ProductPrice>();
			return new PagedResultOutput<ProductPriceListDto>(num, listAsync.MapTo<List<ProductPriceListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Create" })]
		public async Task<GetProductPriceForEditOutput> GetProductPriceForEdit(NullableIdInput<long> input)
		{
			ProductPriceEditDto productPriceEditDto;
			if (!input.Id.HasValue)
			{
				productPriceEditDto = new ProductPriceEditDto();
			}
			else
			{
				IRepository<ProductPrice, long> repository = this._priceRepository;
				ProductPrice async = await repository.GetAsync(input.Id.Value);
				productPriceEditDto = async.MapTo<ProductPriceEditDto>();
			}
			return new GetProductPriceForEditOutput()
			{
				Price = productPriceEditDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.ExportData" })]
		public async Task<FileDto> GetProductPricesToExcel()
		{
			List<ProductPrice> allListAsync = await this._priceRepository.GetAllListAsync();
			List<ProductPriceListDto> productPriceListDtos = allListAsync.MapTo<List<ProductPriceListDto>>();
			return this._priceListExcelExporter.ExportToFile(productPriceListDtos);
		}
	}
}