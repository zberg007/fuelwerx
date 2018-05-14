using FuelWerx.Assets.FillLots.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Assets.FillLots.Exporting
{
	public interface IFillLotListExcelExporter
	{
		FileDto ExportToFile(List<FillLotListDto> fillLotListDtos);
	}
}