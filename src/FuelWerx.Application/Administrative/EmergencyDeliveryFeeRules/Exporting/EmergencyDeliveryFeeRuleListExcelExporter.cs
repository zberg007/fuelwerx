using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFeeRules.Exporting
{
	public class EmergencyDeliveryFeeRuleListExcelExporter : EpPlusExcelExporterBase, IEmergencyDeliveryFeeRuleListExcelExporter
	{
		public EmergencyDeliveryFeeRuleListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<EmergencyDeliveryFeeRuleListDto> emergencyDeliveryFeeRuleListDtos)
		{
			return base.CreateExcelPackage("EmergencyDeliveryFeeRuleList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("EmergencyDeliveryFeeRules"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("EmergencyDeliveryFeeRuleIdentifier"), this.L("EmergencyDeliveryFeeRuleName"), this.L("EmergencyDeliveryFeeRuleCaption"), this.L("Active"), this.L("CreationTime") });

				AddObjects<EmergencyDeliveryFeeRuleListDto>(excelWorksheet, 2, emergencyDeliveryFeeRuleListDtos, new Func<EmergencyDeliveryFeeRuleListDto, object>[] {
						l => l.Id,
						l => l.Name,
						l => l.Caption,
						l => l.IsActive,
						l => l.CreationTime
                    });
				excelWorksheet.Column(5).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 3; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}
	}
}