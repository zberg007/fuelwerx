using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using FuelWerx.Products.SpecificPrices.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.SpecificPrices.Exporting
{
	public class SpecificPriceListExcelExporter : EpPlusExcelExporterBase, ISpecificPriceListExcelExporter
	{
		public SpecificPriceListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<ProductSpecificPriceListDto> productSpecificPriceListDtos)
		{
			return base.CreateExcelPackage("ProductSpecificPriceList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("ProductSpecificPrices"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("ProductSpecificPriceIdentifier"), this.L("ProductSpecificPriceUnitCost"), this.L("Active"), this.L("CreationTime") });

				AddObjects<ProductSpecificPriceListDto>(excelWorksheet, 2, productSpecificPriceListDtos, new Func<ProductSpecificPriceListDto, object>[] {
 						l => l.Id,
 						l => l.Cost,
 						l => l.IsActive,
 						l => l.CreationTime
                    });
				excelWorksheet.Column(4).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 3; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}
	}
}