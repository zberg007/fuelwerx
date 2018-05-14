using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using FuelWerx.Estimates.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Exporting
{
	public class EstimateListExcelExporter : EpPlusExcelExporterBase, IEstimateListExcelExporter
	{
		public EstimateListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<EstimateListDto> estimateListDtos)
		{
			return base.CreateExcelPackage("EstimateList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Estimates"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("EstimateIdentifier"), this.L("EstimateLabel"), this.L("EstimateNumber"), this.L("Active"), this.L("CreationTime") });
				AddObjects<EstimateListDto>(excelWorksheet, 2, estimateListDtos, new Func<EstimateListDto, object>[] {
						l => l.Id,
						l => l.Label,
						l => l.Number,
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