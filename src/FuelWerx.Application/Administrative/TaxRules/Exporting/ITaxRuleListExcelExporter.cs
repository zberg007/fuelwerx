using FuelWerx.Administrative.TaxRules.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Administrative.TaxRules.Exporting
{
	public interface ITaxRuleListExcelExporter
	{
		FileDto ExportToFile(List<TaxRuleListDto> taxRuleListDtos);
	}
}