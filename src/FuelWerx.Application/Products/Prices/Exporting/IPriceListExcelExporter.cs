using FuelWerx.Dto;
using FuelWerx.Products.Prices.Dto;
using System.Collections.Generic;

namespace FuelWerx.Products.Prices.Exporting
{
	public interface IPriceListExcelExporter
	{
		FileDto ExportToFile(List<ProductPriceListDto> productPriceListDtos);
	}
}