using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Dto;
using FuelWerx.Products.SpecificPrices.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Products.SpecificPrices
{
	public interface ISpecificPriceAppService : IApplicationService, ITransientDependency
	{
		Task<string> CheckForSpecificPrice(CheckSpecificPriceInputDto input);

		Task<long> CreateOrUpdateProductSpecificPrice(CreateOrUpdateProductSpecificPriceInput input);

		Task DeleteProductSpecificPrice(IdInput<long> input);

		Task<GetProductSpecificPriceForEditOutput> GetProductSpecificPriceForEdit(NullableIdInput<long> input);

		Task<FileDto> GetProductSpecificPricesToExcel();

		Task<PagedResultOutput<ProductSpecificPriceListDto>> GetSpecificPrices(GetProductSpecificPricesInput input);
	}
}