using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Assets.FillLots.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.FillLots.Exporting
{
	public class FillLotListExcelExporter : EpPlusExcelExporterBase, IFillLotListExcelExporter
	{
		public FillLotListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<FillLotListDto> fillLotListDtos)
		{
			return base.CreateExcelPackage("FillLotList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("FuelLots"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("FuelLotIdentifier"), this.L("Label"), this.L("ShortLabel"), this.L("Description"), this.L("Active"), this.L("CreationTime") });

				AddObjects(excelWorksheet, 2, fillLotListDtos, new Func<FillLotListDto, object>[] {
						l => l.Id,
						l => l.Label,
						l => l.ShortLabel,
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