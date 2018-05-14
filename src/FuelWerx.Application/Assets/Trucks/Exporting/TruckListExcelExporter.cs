using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Assets.Trucks.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.Trucks.Exporting
{
	public class TruckListExcelExporter : EpPlusExcelExporterBase, ITruckListExcelExporter
	{
		public TruckListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<TruckListDto> truckListDtos)
		{
			return base.CreateExcelPackage("TruckList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Trucks"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("TruckIdentifier"), this.L("Name"), this.L("Number"), this.L("Description"), this.L("Active"), this.L("CreationTime") });

				AddObjects(excelWorksheet, 2, truckListDtos, new Func<TruckListDto, object>[] {
						l => l.Id,
						l => l.Name,
						l => l.Number,
						l => l.Description,
						l => l.IsActive,
						l => l.CreationTime
                    });
				excelWorksheet.Column(6).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 6; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}
	}
}