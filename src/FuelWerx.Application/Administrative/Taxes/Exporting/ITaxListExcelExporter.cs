using FuelWerx.Administrative.Taxes.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Administrative.Taxes.Exporting
{
	public interface ITaxListExcelExporter
	{
		FileDto ExportToFile(List<TaxListDto> taxListDtos);
	}
}