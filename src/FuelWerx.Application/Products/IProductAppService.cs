using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Dto;
using FuelWerx.Products.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Products
{
	public interface IProductAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateProduct(CreateOrUpdateProductInput input);

		Task CreateOrUpdateProductSuppliers(CreateOrUpdateProductSupplierInput input);

		Task DeleteProduct(IdInput<long> input);

		Task DeleteProductResource(IdInput<long> input);

		Task<Product> GetProduct(long productId);

		Task<GetProductForEditOutput> GetProductForEdit(NullableIdInput<long> input);

		Task<ProductResourceEditDto> GetProductResourceDetailsByBinaryObjectId(Guid resourceId);

		Task<GetProductResourceForEditOutput> GetProductResourcesForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<ProductListDto>> GetProducts(GetProductsInput input);

		Task<ListResultDto<ProductListDto>> GetProductsByTenantId(int tenantId, bool active = true, string query = "");

		Task<FileDto> GetProductsToExcel();

		Task<decimal> GetProductsTotalByIdList(string productIds);

		Task<GetProductSuppliersForEditOutput> GetProductSuppliersForEdit(NullableIdInput<long> input);

		Task<bool> IncreaseQuantityOnHand(long productId, int quantity);

		Task<bool> ReduceQuantityOnHand(long productId, int quantity);

		Task SaveProductImageAsync(UpdateProductImageInput updateProductImageInput);

		Task SaveProductResourceAsync(UpdateProductResourceInput updateProductResourceInput);

		Task SaveProductResourceDetails(long productResourceId, string name, string description, bool isActive);
	}
}