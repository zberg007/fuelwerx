using FuelWerx.Dto;
using FuelWerx.Estimates.Dto;
using System.Collections.Generic;

namespace FuelWerx.Estimates.Exporting
{
	public interface IEstimateListExcelExporter
	{
		FileDto ExportToFile(List<EstimateListDto> estimateListDtos);
	}
}