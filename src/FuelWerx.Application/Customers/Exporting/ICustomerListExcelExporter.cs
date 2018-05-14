using FuelWerx.Customers.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Customers.Exporting
{
	public interface ICustomerListExcelExporter
	{
		FileDto ExportToFile(List<CustomerListDto> customerListDtos);

		FileDto ExportToFile(List<CustomerWithAddressListDto> customerWithAddressListDtos);
	}
}