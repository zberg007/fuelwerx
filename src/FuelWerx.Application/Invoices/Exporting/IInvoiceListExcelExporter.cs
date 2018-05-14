using FuelWerx.Dto;
using FuelWerx.Invoices.Dto;
using System.Collections.Generic;

namespace FuelWerx.Invoices.Exporting
{
	public interface IInvoiceListExcelExporter
	{
		FileDto ExportToFile(List<InvoiceListDto> invoiceListDtos);

		FileDto ExportToFile(List<InvoicePaymentListDto> invoicePaymentListDtos);
	}
}