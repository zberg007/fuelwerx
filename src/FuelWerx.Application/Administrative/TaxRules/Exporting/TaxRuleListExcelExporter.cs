using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Administrative.TaxRules.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Exporting
{
	public class TaxRuleListExcelExporter : EpPlusExcelExporterBase, ITaxRuleListExcelExporter
	{
		public TaxRuleListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<TaxRuleListDto> taxRuleListDtos)
		{
			return base.CreateExcelPackage("TaxRuleList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("TaxRules"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("TaxRuleIdentifier"), this.L("TaxRuleName"), this.L("TaxRuleCaption"), this.L("Active"), this.L("CreationTime") });

				AddObjects(excelWorksheet, 2, taxRuleListDtos, new Func<TaxRuleListDto, object>[] {
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