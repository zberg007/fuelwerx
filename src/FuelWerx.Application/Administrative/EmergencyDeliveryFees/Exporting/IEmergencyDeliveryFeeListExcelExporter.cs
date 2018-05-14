using FuelWerx.Administrative.EmergencyDeliveryFees.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Administrative.EmergencyDeliveryFees.Exporting
{
	public interface IEmergencyDeliveryFeeListExcelExporter
	{
		FileDto ExportToFile(List<EmergencyDeliveryFeeListDto> emergencyDeliveryFeeListDtos);
	}
}