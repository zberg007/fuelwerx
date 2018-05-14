using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Dto;
using FuelWerx.Generic;
using FuelWerx.Products.Dto;
using FuelWerx.Products.Exporting;
using FuelWerx.Products.Prices.Dto;
using FuelWerx.Storage;
using FuelWerx.Suppliers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Products
{
	public class ProductAppService : FuelWerxAppServiceBase, IProductAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Product, long> _productRepository;

		private readonly IRepository<ProductOption, long> _productOptionRepository;

		private readonly IProductListExcelExporter _productListExcelExporter;

		private readonly IRepository<ProductSupplier, long> _productSupplierRepository;

		private readonly IRepository<Supplier, long> _supplierRepository;

		private readonly IRepository<ProductResource, long> _productResourceRepository;

		private readonly IRepository<ProductPrice, long> _productPriceRepository;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public ProductAppService(IRepository<Product, long> productRepository, IRepository<ProductOption, long> productOptionRepository, IProductListExcelExporter productListExcelExporter, IRepository<ProductSupplier, long> productSupplierRepository, IRepository<Supplier, long> supplierRepository, IRepository<ProductResource, long> productResourceRepository, IRepository<ProductPrice, long> productPriceRepository, IBinaryObjectManager binaryObjectManager)
		{
			this._productRepository = productRepository;
			this._productOptionRepository = productOptionRepository;
			this._productListExcelExporter = productListExcelExporter;
			this._productSupplierRepository = productSupplierRepository;
			this._supplierRepository = supplierRepository;
			this._productPriceRepository = productPriceRepository;
			this._productResourceRepository = productResourceRepository;
			this._binaryObjectManager = binaryObjectManager;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task<long> CreateOrUpdateProduct(CreateOrUpdateProductInput input)
		{
			long value;
			if (!input.Product.Id.HasValue)
			{
				long num = await this._productRepository.InsertAndGetIdAsync(input.Product.MapTo<Product>());
				value = num;
			}
			else
			{
				value = input.Product.Id.Value;
				await this._productRepository.UpdateAsync(input.Product.MapTo<Product>());
			}
			if (input.Product.ProductOptions.Any<ProductOption>())
			{
				foreach (ProductOption productOption in input.Product.ProductOptions)
				{
					if (productOption.Name.IsNullOrEmpty() && productOption.Comment.IsNullOrEmpty() && productOption.Value.IsNullOrEmpty())
					{
						continue;
					}
					productOption.ProductId = value;
					await this._productOptionRepository.InsertOrUpdateAsync(productOption);
				}
			}
			return value;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task CreateOrUpdateProductSuppliers(CreateOrUpdateProductSupplierInput input)
		{
			IRepository<ProductSupplier, long> repository = this._productSupplierRepository;
			List<ProductSupplier> allListAsync = await repository.GetAllListAsync((ProductSupplier w) => (long?)w.ProductId == input.ProductId);
			List<ProductSupplier> productSuppliers = allListAsync;
			if (productSuppliers.Any<ProductSupplier>())
			{
				List<ProductSupplier> productSuppliers1 = productSuppliers;
				List<long> list = (
					from s in productSuppliers1
					select s.Id).ToList<long>();
				foreach (ProductSupplier productSupplier in productSuppliers)
				{
					if (!(
						from m in input.ProductSuppliers
						where m.SupplierId == productSupplier.SupplierId
						select m).Any<ProductSupplierEditDto>())
					{
						continue;
					}
					input.ProductSuppliers.RemoveAll((ProductSupplierEditDto x) => x.SupplierId == productSupplier.SupplierId);
					list.RemoveAll((long x) => x == productSupplier.Id);
				}
				if (list.Any<long>())
				{
					IRepository<ProductSupplier, long> repository1 = this._productSupplierRepository;
					await repository1.DeleteAsync((ProductSupplier m) => list.Contains(m.Id));
				}
			}
			List<ProductSupplierEditDto> productSupplierEditDtos = input.ProductSuppliers;
			List<long> nums = (
				from s in productSupplierEditDtos
				select s.SupplierId).ToList<long>();
			if (nums.Any<long>())
			{
				IRepository<Supplier, long> repository2 = this._supplierRepository;
				List<Supplier> suppliers = await repository2.GetAllListAsync((Supplier m) => nums.Contains(m.Id));
				List<Supplier> suppliers1 = suppliers;
				if (suppliers1.Any<Supplier>())
				{
					List<Supplier> suppliers2 = suppliers1;
					List<long> list1 = (
						from s in suppliers2
						select s.Id).ToList<long>();
					foreach (ProductSupplierEditDto productSupplierEditDto in input.ProductSuppliers)
					{
						if (!list1.Contains(productSupplierEditDto.SupplierId))
						{
							continue;
						}
						ProductSupplier productSupplier1 = new ProductSupplier()
						{
							ProductId = productSupplierEditDto.ProductId,
							SupplierId = productSupplierEditDto.SupplierId,
							IsActive = true
						};
						await this._productSupplierRepository.InsertAsync(productSupplier1);
					}
					list1 = null;
				}
			}
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Delete" })]
		public async Task DeleteProduct(IdInput<long> input)
		{
			await this.DeleteProductImage(input);
			await this._productRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Delete" })]
		private async Task<bool> DeleteProductImage(IdInput<long> input)
		{
			Guid guid;
			Product async = await this._productRepository.GetAsync(input.Id);
			Guid? imageId = async.ImageId;
			if (imageId.HasValue)
			{
				imageId = async.ImageId;
				if (Guid.TryParse(imageId.ToString(), out guid))
				{
					await this._binaryObjectManager.DeleteAsync(guid);
					imageId = null;
					async.ImageId = imageId;
				}
			}
			return true;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Delete" })]
		public async Task DeleteProductResource(IdInput<long> input)
		{
			ProductResource async = await this._productResourceRepository.GetAsync(input.Id);
			if (async != null)
			{
				await this._productResourceRepository.DeleteAsync(async.Id);
			}
		}

		private string GetCategoryFromFileExtension(string fileExt)
		{
			string empty = string.Empty;
			empty = (fileExt == ".pdf" || fileExt == ".xlsx" || fileExt == ".xls" || fileExt == ".docx" || fileExt == ".doc" || fileExt == ".zip" ? this.L("ResourceCategoryAttachment") : this.L("ResourceCategoryImage"));
			return empty;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products" })]
		public async Task<Product> GetProduct(long productId)
		{
			return await this._productRepository.GetAsync(productId);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Create" })]
		public async Task<GetProductForEditOutput> GetProductForEdit(NullableIdInput<long> input)
		{
			ProductEditDto productEditDto;
			if (!input.Id.HasValue)
			{
				productEditDto = new ProductEditDto()
				{
					ProductOptions = new List<ProductOption>()
				};
			}
			else
			{
				IRepository<Product, long> repository = this._productRepository;
				Product async = await repository.GetAsync(input.Id.Value);
				productEditDto = async.MapTo<ProductEditDto>();
				ProductEditDto productEditDto1 = productEditDto;
				IRepository<ProductOption, long> repository1 = this._productOptionRepository;
				List<ProductOption> allListAsync = await repository1.GetAllListAsync((ProductOption x) => x.ProductId == async.Id);
				productEditDto1.ProductOptions = allListAsync;
				productEditDto1 = null;
			}
			return new GetProductForEditOutput()
			{
				Product = productEditDto
			};
		}

		public async Task<ProductResourceEditDto> GetProductResourceDetailsByBinaryObjectId(Guid resourceId)
		{
			IRepository<ProductResource, long> repository = this._productResourceRepository;
			ProductResource productResource = await repository.FirstOrDefaultAsync((ProductResource m) => m.BinaryObjectId == resourceId);
			ProductResource productResource1 = productResource;
			if (productResource1 == null)
			{
				throw new Exception("Product Resource was not found in GetProductResourceDetailsByBinaryObjectId();");
			}
			return productResource1.MapTo<ProductResourceEditDto>();
		}

		public async Task<GetProductResourceForEditOutput> GetProductResourcesForEdit(NullableIdInput<long> input)
		{
			List<ProductResourceEditDto> productResourceEditDtos = new List<ProductResourceEditDto>();
			if (input.Id.HasValue)
			{
				IRepository<ProductResource, long> repository = this._productResourceRepository;
				List<ProductResource> allListAsync = await repository.GetAllListAsync((ProductResource m) => m.ProductId == input.Id.Value);
				List<ProductResource> productResources = allListAsync;
				if (productResources.Any<ProductResource>())
				{
					productResourceEditDtos = productResources.MapTo<List<ProductResourceEditDto>>();
				}
			}
			return new GetProductResourceForEditOutput()
			{
				ProductResources = productResourceEditDtos
			};
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products" })]
		public async Task<PagedResultOutput<ProductListDto>> GetProducts(GetProductsInput input)
		{
			//ProductAppService.<>c__DisplayClass9_0 variable = null;
			//ProductAppService.<>c__DisplayClass9_1 variable1 = null;
			decimal num;
			bool filterIsDecimal = decimal.TryParse(input.Filter, out num);
			IQueryable<ProductOption> all = this._productOptionRepository.GetAll();
            var productOptions = all.Where(m => m.TenantId == AbpSession.TenantId);
            var productOptions1 = productOptions.WhereIf(!input.Filter.IsNullOrEmpty(), p =>
				p.Name.Contains(input.Filter) ||
				p.Comment.Contains(input.Filter) ||
				p.Value.Contains(input.Filter));
			var productOptionIds = await productOptions1.Select(s => new { ProductId = s.ProductId }).ToListAsync();
			IQueryable<ProductResource> productResources = this._productResourceRepository.GetAll();
            var productResources1 = productResources.Where(m => m.TenantId == AbpSession.TenantId);
            var productResources2 = productResources1.WhereIf(!input.Filter.IsNullOrEmpty(), p =>
				p.Name.Contains(input.Filter) ||
				p.Description.Contains(input.Filter) ||
				p.Category.Contains(input.Filter) ||
				p.FileExtension.Contains(input.Filter) ||
				p.FileName.Contains(input.Filter));
			var productResourceIds = await productResources2.Select(s => new { ProductId = s.ProductId }).ToListAsync();
			IQueryable<Product> products = this._productRepository.GetAll();
            var products1 = products.WhereIf(!input.Filter.IsNullOrEmpty(), p =>
				p.Name.Contains(input.Filter) ||
				p.Reference.Contains(input.Filter) ||
				p.Sku.Contains(input.Filter) ||
				p.Description.Contains(input.Filter) ||
				p.QuantitySoldIn.Contains(input.Filter));
			var productIds = await products1.Select(s => new { ProductId = s.Id }).ToListAsync();
			List<long> list = (
				from s in productOptionIds
				select s.ProductId).ToList();
			IEnumerable<long> nums = list.Union((
				from s in productResourceIds
				select s.ProductId).ToList());
			IEnumerable<long> foundProductIdsFromInputFilter = nums.Union((
				from s in productIds
				select s.ProductId).ToList());
			IQueryable<ProductSupplier> productSuppliers = _productSupplierRepository.GetAll();
            var productSuppliers1 = productSuppliers.Where(m => m.TenantId == AbpSession.TenantId);
			List<long> distinctSuppliersWithProducts = await productSuppliers1.Select(s => s.SupplierId).ToListAsync();
			if (distinctSuppliersWithProducts.Any())
			{
				IQueryable<Supplier> suppliers = _supplierRepository.GetAll();
				IQueryable<Supplier> tenantSuppliers = suppliers.Where(m => m.TenantId == AbpSession.TenantId);
                var suppliers2 = tenantSuppliers.WhereIf(!input.Filter.IsNullOrEmpty(), p =>
                    distinctSuppliersWithProducts.Contains(p.Id) && (
                        p.Name.Contains(input.Filter) ||
                        p.Description.Contains(input.Filter) ||
                        p.PhoneNumber.Contains(input.Filter) ||
                        p.MobilePhoneNumber.Contains(input.Filter) ||
                        p.Address.Contains(input.Filter) ||
                        p.SecondaryAddress.Contains(input.Filter) ||
                        p.City.Contains(input.Filter) ||
                        p.Country.Code.Contains(input.Filter) ||
                        p.Country.Name.Contains(input.Filter) ||
                        p.CountryRegion.Code.Contains(input.Filter) ||
                        p.CountryRegion.Name.Contains(input.Filter) ||
                        p.PostalCode.Contains(input.Filter) ||
                        p.ContactName.Contains(input.Filter) ||
                        p.ContactEmail.Contains(input.Filter)
                    ));
				List<long> productSuppliersThatMatchSearch = await suppliers2.Select(s => s.Id).ToListAsync();
				if (productSuppliersThatMatchSearch.Any())
				{
					IQueryable<ProductSupplier> all1 = this._productSupplierRepository.GetAll();
                    var productSuppliers2 = all1.Where(m => m.TenantId == AbpSession.TenantId && productSuppliersThatMatchSearch.Contains(m.SupplierId));
					var listAsync4 = await productSuppliers2.Select(s => new { ProductId = s.ProductId }).ToListAsync();
                    foundProductIdsFromInputFilter = foundProductIdsFromInputFilter.Union((
						from s in listAsync4
                        select s.ProductId).ToList());
				}
			}
			if (filterIsDecimal)
			{
				IQueryable<ProductPrice> productPrices = _productPriceRepository.GetAll();
                var productPrices1 = productPrices.Where(m => m.TenantId == AbpSession.TenantId);
                var productPrices2 = productPrices1.WhereIf(!input.Filter.IsNullOrEmpty(), p =>
                    p.Cost == num ||
                    p.UnitPrice == num ||
                    p.SpecialDeliveryFee == num);
				var listAsync5 = await productPrices2.Select(s => new { ProductId = s.ProductId }).ToListAsync();
				IQueryable<Product> all2 = this._productRepository.GetAll();
                var products2 = all2.WhereIf(!input.Filter.IsNullOrEmpty(), p =>
                    p.BasePrice == num ||
                    p.FinalPrice == num ||
                    p.Surcharge == num);
				var listAsync6 = await products2.Select(s => new { ProductId = s.Id }).ToListAsync();
                foundProductIdsFromInputFilter = foundProductIdsFromInputFilter.Union((
					from s in listAsync5
					select s.ProductId).ToList());
                foundProductIdsFromInputFilter = foundProductIdsFromInputFilter.Union((
					from s in listAsync6
					select s.ProductId).ToList());
			}
			IQueryable<Product> all3 = this._productRepository.GetAll();
            var products3 = all3.Where(m => foundProductIdsFromInputFilter.Contains(m.Id));
			int num1 = await products3.CountAsync();
			List<Product> products4 = await products3.OrderBy(input.Sorting, new object[0]).PageBy(input).ToListAsync();
			return new PagedResultOutput<ProductListDto>(num1, products4.MapTo<List<ProductListDto>>());
		}

		public async Task<ListResultDto<ProductListDto>> GetProductsByTenantId(int tenantId, bool active = true, string query = "")
		{
			IQueryable<Product> all = this._productRepository.GetAll();
			IQueryable<Product> products =
				from p in all
				where p.TenantId == tenantId
				select p;
			IQueryable<Product> products1 = products.WhereIf<Product>(!string.IsNullOrEmpty(query), (Product p) => p.Description.Contains(query) || p.Name.Contains(query) || p.Reference.Contains(query) || p.Sku.Contains(query));
			bool flag = active;
			List<Product> listAsync = await products1.WhereIf<Product>(flag, (Product p) => p.IsActive == active).OrderBy<Product>("Name", new object[0]).ToListAsync<Product>();
			List<ProductListDto> productListDtos = listAsync.MapTo<List<ProductListDto>>();
			List<long> nums = new List<long>();
			foreach (ProductListDto productOptionDtos in productListDtos)
			{
				IRepository<ProductOption, long> repository = this._productOptionRepository;
				List<ProductOption> allListAsync = await repository.GetAllListAsync((ProductOption x) => x.ProductId == (long)productOptionDtos.Id && x.IsActive);
				List<ProductOption> productOptions = allListAsync;
				if (!productOptions.Any<ProductOption>())
				{
					productOptionDtos.ProductOptions = new List<ProductOptionDto>();
				}
				else
				{
					foreach (ProductOption productOption in productOptions)
					{
						if (productOptionDtos.ProductOptions == null)
						{
							productOptionDtos.ProductOptions = new List<ProductOptionDto>();
						}
						productOptionDtos.ProductOptions.Add(productOption.MapTo<ProductOptionDto>());
					}
				}
				IRepository<ProductPrice, long> repository1 = this._productPriceRepository;
				List<ProductPrice> productPrices = await repository1.GetAllListAsync((ProductPrice x) => x.ProductId == (long)productOptionDtos.Id && x.IsActive);
				List<ProductPrice> productPrices1 = productPrices;
				if (productPrices1.Any<ProductPrice>())
				{
					List<ProductPrice>.Enumerator enumerator = productPrices1.GetEnumerator();
					try
					{
						if (enumerator.MoveNext())
						{
							ProductPrice current = enumerator.Current;
							if (productOptionDtos.ProductPrices == null)
							{
								productOptionDtos.ProductPrices = new List<PriceDto>();
							}
							productOptionDtos.ProductPrices.Add(current.MapTo<PriceDto>());
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			return new ListResultDto<ProductListDto>(productListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.ExportData" })]
		public async Task<FileDto> GetProductsToExcel()
		{
			List<Product> allListAsync = await this._productRepository.GetAllListAsync();
			List<ProductListDto> productListDtos = allListAsync.MapTo<List<ProductListDto>>();
			return this._productListExcelExporter.ExportToFile(productListDtos);
		}

		public async Task<decimal> GetProductsTotalByIdList(string productIds)
		{
			decimal num;
			decimal num1;
			decimal num2 = new decimal();
			if (productIds.Trim().Length > 0)
			{
				string str = productIds;
				char[] chrArray = new char[] { ',' };
				List<long> list = str.Split(chrArray).Select<string, long>(new Func<string, long>(long.Parse)).ToList<long>();
				IRepository<Product, long> repository = this._productRepository;
				List<Product> allListAsync = await repository.GetAllListAsync((Product x) => list.Contains(x.Id) && x.TenantId == this.AbpSession.TenantId.Value);
				List<Product> products = allListAsync;
				if (products.Any<Product>())
				{
					foreach (Product product in products)
					{
						IRepository<ProductPrice, long> repository1 = this._productPriceRepository;
						ProductPrice productPrice = await repository1.FirstOrDefaultAsync((ProductPrice x) => x.ProductId == product.Id && x.IsActive);
						ProductPrice productPrice1 = productPrice;
						if (!(productPrice1 != null) || !decimal.TryParse(productPrice1.UnitPrice.ToString(), out num1))
						{
							continue;
						}
						num2 = num2 + num1;
					}
				}
				else
				{
					num = num2;
					return num;
				}
			}
			num = num2;
			return num;
		}

		public async Task<GetProductSuppliersForEditOutput> GetProductSuppliersForEdit(NullableIdInput<long> input)
		{
			List<ProductSupplierEditDto> productSupplierEditDtos = new List<ProductSupplierEditDto>();
			if (input.Id.HasValue)
			{
				IRepository<ProductSupplier, long> repository = this._productSupplierRepository;
				List<ProductSupplier> allListAsync = await repository.GetAllListAsync((ProductSupplier m) => m.ProductId == input.Id.Value);
				List<ProductSupplier> productSuppliers = allListAsync;
				if (productSuppliers.Any<ProductSupplier>())
				{
					productSupplierEditDtos = productSuppliers.MapTo<List<ProductSupplierEditDto>>();
				}
			}
			return new GetProductSuppliersForEditOutput()
			{
				ProductSuppliers = productSupplierEditDtos
			};
		}

		public async Task<bool> IncreaseQuantityOnHand(long productId, int quantity)
		{
			bool flag;
			Product async = await this._productRepository.GetAsync(productId);
			if (async == null)
			{
				flag = false;
			}
			else
			{
				async.QuantityOnHand = async.QuantityOnHand + quantity;
				await this._productRepository.UpdateAsync(async);
				flag = true;
			}
			return flag;
		}

		public async Task<bool> ReduceQuantityOnHand(long productId, int quantity)
		{
			bool flag;
			Product async = await this._productRepository.GetAsync(productId);
			if (async == null)
			{
				flag = false;
			}
			else
			{
				async.QuantityOnHand = async.QuantityOnHand - quantity;
				await this._productRepository.UpdateAsync(async);
				flag = true;
			}
			return flag;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task SaveProductImageAsync(UpdateProductImageInput input)
		{
			Product async = await this._productRepository.GetAsync(input.ProductId);
			Guid? imageId = input.ImageId;
			if (!imageId.HasValue)
			{
				imageId = null;
				async.ImageId = imageId;
			}
			else
			{
				imageId = input.ImageId;
				async.ImageId = new Guid?(imageId.Value);
			}
			await this._productRepository.UpdateAsync(async);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task SaveProductResourceAsync(UpdateProductResourceInput input)
		{
			Guid? resourceId;
			if (input.Id <= (long)0)
			{
				ProductResource productResource = new ProductResource()
				{
					Id = (long)0
				};
				resourceId = input.ResourceId;
				productResource.BinaryObjectId = Guid.Parse(resourceId.ToString());
				productResource.ProductId = input.ProductId;
				productResource.Name = input.FileName;
				productResource.FileName = input.FileName;
				productResource.FileExtension = input.FileExtension;
				productResource.FileSize = input.FileSize;
				productResource.Category = this.GetCategoryFromFileExtension(input.FileExtension);
				productResource.IsActive = input.IsActive;
				await this._productResourceRepository.InsertAsync(productResource);
			}
			else
			{
				ProductResource async = await this._productResourceRepository.GetAsync(input.Id);
				if (async != null)
				{
					resourceId = input.ResourceId;
					async.BinaryObjectId = Guid.Parse(resourceId.ToString());
					async.ProductId = input.ProductId;
					async.FileExtension = input.FileExtension;
					async.FileName = input.FileName;
					async.FileSize = input.FileSize;
					async.Category = this.GetCategoryFromFileExtension(input.FileExtension);
					async.IsActive = input.IsActive;
				}
				await this._productResourceRepository.UpdateAsync(async);
			}
		}

		public async Task SaveProductResourceDetails(long id, string name, string description, bool isActive)
		{
			ProductResource async = await this._productResourceRepository.GetAsync(id);
			if (async != null)
			{
				async.Description = description;
			}
			await this._productResourceRepository.UpdateAsync(async);
		}
	}
}