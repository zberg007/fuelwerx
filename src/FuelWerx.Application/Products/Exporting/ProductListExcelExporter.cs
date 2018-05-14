using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using FuelWerx.Products.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Exporting
{
	public class ProductListExcelExporter : EpPlusExcelExporterBase, IProductListExcelExporter
	{
		public ProductListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<ProductListDto> productListDtos)
		{
			return base.CreateExcelPackage("ProductList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Products"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("ProductIdentifier"), this.L("ProductName"), this.L("ProductReference"), this.L("Active"), this.L("CreationTime") });

				AddObjects(excelWorksheet, 2, productListDtos, new Func<ProductListDto, object>[] {
						l => l.Id,
						l => l.Name,
						l => l.Reference,
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