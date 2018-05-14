using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Administrative.Titles.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Titles.Exporting
{
	public class TitleListExcelExporter : EpPlusExcelExporterBase, ITitleListExcelExporter
	{
		public TitleListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<TitleListDto> titleListDtos)
		{
			return base.CreateExcelPackage("TitleList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Titles"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("TitleIdentifier"), this.L("TitleName"), this.L("TitleType"), this.L("Active"), this.L("CreationTime") });

				AddObjects(excelWorksheet, 2, titleListDtos, new Func<TitleListDto, object>[] {
						l => l.Id,
						l => l.Name,
						l => l.Type,
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