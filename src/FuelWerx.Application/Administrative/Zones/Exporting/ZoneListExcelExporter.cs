using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Administrative.Zones.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Zones.Exporting
{
	public class ZoneListExcelExporter : EpPlusExcelExporterBase, IZoneListExcelExporter
	{
		public ZoneListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<ZoneListDto> zoneListDtos)
		{
			return base.CreateExcelPackage("ZoneList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Zones"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("ZoneIdentifier"), this.L("ZoneName"), this.L("ZoneCaption"), this.L("Active"), this.L("CreationTime") });
				
				AddObjects(excelWorksheet, 2, zoneListDtos, new Func<ZoneListDto, object>[] {
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