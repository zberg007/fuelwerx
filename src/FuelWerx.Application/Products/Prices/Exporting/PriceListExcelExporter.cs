using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using FuelWerx.Products.Prices.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Prices.Exporting
{
	public class PriceListExcelExporter : EpPlusExcelExporterBase, IPriceListExcelExporter
	{
		public PriceListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<ProductPriceListDto> productPriceListDtos)
		{
			return base.CreateExcelPackage("ProductPriceList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("ProductPrices"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("ProductPriceIdentifier"), this.L("ProducPricetCost"), this.L("ProductPriceUnitCost"), this.L("Active"), this.L("CreationTime") });

				AddObjects(excelWorksheet, 2, productPriceListDtos, new Func<ProductPriceListDto, object>[] {
						l => l.Id,
						l => l.Cost,
						l => l.UnitPrice,
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