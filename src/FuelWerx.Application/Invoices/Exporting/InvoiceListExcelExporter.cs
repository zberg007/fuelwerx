using Abp;
using Abp.Application.Services.Dto;
using FuelWerx.Customers.Dto;
using FuelWerx.DataExporting.Excel.EpPlus;
using FuelWerx.Dto;
using FuelWerx.Invoices.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Exporting
{
	public class InvoiceListExcelExporter : EpPlusExcelExporterBase, IInvoiceListExcelExporter
	{
		public InvoiceListExcelExporter()
		{
		}

		public FileDto ExportToFile(List<InvoiceListDto> invoiceListDtos)
		{
			return base.CreateExcelPackage("InvoiceList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("Invoices"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("InvoiceIdentifier"), this.L("InvoiceLabel"), this.L("InvoiceNumber"), this.L("Active"), this.L("CreationTime") });
				AddObjects(excelWorksheet, 2, invoiceListDtos, new Func<InvoiceListDto, object>[] {
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

		public FileDto ExportToFile(List<InvoicePaymentListDto> invoicePaymentListDtos)
		{
			return base.CreateExcelPackage("InvoicePaymentList.xlsx", (ExcelPackage excelPackage) => {
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(this.L("InvoicePayments"));
				excelWorksheet.OutLineApplyStyle = true;
				base.AddHeader(excelWorksheet, new string[] { this.L("InvoicePaymentIdentifier"), this.L("InvoiceNumber"), this.L("TransactionDateTime"), this.L("CustomerName"), this.L("Status"), this.L("Amount"), this.L("Response"), this.L("AuthorizationCode"), this.L("TransactionId"), this.L("ExportedToQuickBooks") });
				AddObjects(excelWorksheet, 2, invoicePaymentListDtos, new Func<InvoicePaymentListDto, object>[] {
						l => l.Id,
						l => l.Invoice.Number,
						l => l.TransactionDateTime,
						l => string.Concat(l.Customer.BusinessName, " ", l.Customer.FirstName, l.Customer.LastName),
						l => l.X_Response_Reason_Text,
						l => l.DollarAmount,
						l => l.P_Exact_Ctr,
						l => l.X_Auth_Code,
						l => l.X_Trans_Id,
						l => l.ExportedToReporting
                    });
				excelWorksheet.Column(3).Style.Numberformat.Format = "mm-dd-yy";
				for (int i = 1; i <= 10; i++)
				{
					excelWorksheet.Column(i).AutoFit();
				}
			});
		}
	}
}