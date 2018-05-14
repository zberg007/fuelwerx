using FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Administrative.EmergencyDeliveryFeeRules.Exporting
{
	public interface IEmergencyDeliveryFeeRuleListExcelExporter
	{
		FileDto ExportToFile(List<EmergencyDeliveryFeeRuleListDto> emergencyDeliveryFeeRuleListDtos);
	}
}