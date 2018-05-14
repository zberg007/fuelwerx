using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Administrative.Taxes.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Taxes.Exporting
{
	public class TaxListExcelExporter : EpPlusExcelExporterBase, ITaxListExcelExporter
	{
		public TaxListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<TaxListDto> taxRuleListDtos)
		{
			return base.CreateExcelPackage("TaxList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Taxes"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("TaxIdentifier"), this.L("TaxName"), this.L("TaxRate"), this.L("TaxCaption"), this.L("Active"), this.L("CreationTime") });
				
				AddObjects(excelWorksheet, 2, taxRuleListDtos, new Func<TaxListDto, object>[] {
						l => l.Id,
						l => l.Name,
						l => l.Rate,
						l => l.Caption,
						l => l.IsActive,
						l => l.CreationTime
                    });
				excelWorksheet.Column(6).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 4; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}
	}
}