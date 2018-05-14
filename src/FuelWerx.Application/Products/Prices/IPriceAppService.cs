using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Dto;
using FuelWerx.Products.Prices.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Products.Prices
{
	public interface IPriceAppService : IApplicationService, ITransientDependency
	{
		Task<bool> CreateOrUpdateProductPrice(CreateOrUpdateProductPriceInput input);

		Task DeleteProductPrice(IdInput<long> input);

		Task<PagedResultOutput<ProductPriceListDto>> GetPrices(GetProductPricesInput input);

		Task<GetProductPriceForEditOutput> GetProductPriceForEdit(NullableIdInput<long> input);

		Task<FileDto> GetProductPricesToExcel();
	}
}