using FuelWerx.Dto;
using FuelWerx.Products.SpecificPrices.Dto;
using System.Collections.Generic;

namespace FuelWerx.Products.SpecificPrices.Exporting
{
	public interface ISpecificPriceListExcelExporter
	{
		FileDto ExportToFile(List<ProductSpecificPriceListDto> productPriceListDtos);
	}
}