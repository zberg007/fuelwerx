using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Customers.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using FuelWerx.Projects.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Exporting
{
	public class ProjectListExcelExporter : EpPlusExcelExporterBase, IProjectListExcelExporter
	{
		public ProjectListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<ProjectListDto> projectListDtos)
		{
			return base.CreateExcelPackage("ProjectList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Projects"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("ProjectIdentifier"), this.L("ProjectLabel"), this.L("ProjectNumber"), this.L("ProjectCustomer"), this.L("ProjectDescription"), this.L("ProjectDiscount"), this.L("ProjectHours"), this.L("ProjectLineTotal"), this.L("ProjectLogDataAndTasksVisibleToCustomer"), this.L("ProjectPONumber"), this.L("ProjectRate"), this.L("ProjectTaskTotal"), this.L("ProjectTax"), this.L("ProjectTerms"), this.L("ProjectTimeEntryLog"), this.L("Active"), this.L("CreationTime") });

				AddObjects<ProjectListDto>(excelWorksheet, 2, projectListDtos, new Func<ProjectListDto, object>[] {
						l => l.Id,
						l => l.Label,
						l => l.Number,
						l => l.Customer.FullName,
						l => l.Description,
						l => l.Discount,
						l => l.Hours,
						l => l.LineTotal,
						l => l.LogDataAndTasksVisibleToCustomer,
						l => l.PONumber,
						l => l.Rate,
						l => l.TaskTotal,
						l => l.Tax,
						l => l.Terms,
						l => l.TimeEntryLog,
						l => l.IsActive,
						l => l.CreationTime
                    });
				excelWorksheet.Column(17).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 16; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}
	}
}