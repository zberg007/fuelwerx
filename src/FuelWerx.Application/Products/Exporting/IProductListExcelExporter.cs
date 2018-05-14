using FuelWerx.Dto;
using FuelWerx.Products.Dto;
using System.Collections.Generic;

namespace FuelWerx.Products.Exporting
{
	public interface IProductListExcelExporter
	{
		FileDto ExportToFile(List<ProductListDto> productListDtos);
	}
}