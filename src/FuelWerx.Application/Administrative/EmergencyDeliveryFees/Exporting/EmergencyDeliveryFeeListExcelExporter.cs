using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Administrative.EmergencyDeliveryFees.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFees.Exporting
{
	public class EmergencyDeliveryFeeListExcelExporter : EpPlusExcelExporterBase, IEmergencyDeliveryFeeListExcelExporter
	{
		public EmergencyDeliveryFeeListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<EmergencyDeliveryFeeListDto> emergencyDeliveryFeeRuleListDtos)
		{
			return base.CreateExcelPackage("EmergencyDeliveryFeeList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("EmergencyDeliveryFees"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("EmergencyDeliveryFeeIdentifier"), this.L("EmergencyDeliveryFeeName"), this.L("EmergencyDeliveryFeeFee"), this.L("EmergencyDeliveryFeeZone"), this.L("EmergencyDeliveryFeeCaption"), this.L("Active"), this.L("CreationTime") });
				AddObjects<EmergencyDeliveryFeeListDto>(excelWorksheet, 2, emergencyDeliveryFeeRuleListDtos, new Func<EmergencyDeliveryFeeListDto, object>[] {
						l => l.Id,
						l => l.Name,
						l => l.Fee,
						l => l.ZoneId,
						l => l.Caption,
						l => l.IsActive,
						l => l.CreationTime
                    });
				excelWorksheet.Column(7).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 5; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}
	}
}