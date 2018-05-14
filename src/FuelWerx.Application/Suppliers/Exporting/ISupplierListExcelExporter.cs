using FuelWerx.Dto;
using FuelWerx.Suppliers.Dto;
using System.Collections.Generic;

namespace FuelWerx.Suppliers.Exporting
{
	public interface ISupplierListExcelExporter
	{
		FileDto ExportToFile(List<SupplierListDto> supplierListDtos);
	}
}