using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Customers.Dto;
using FuelWerx.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelWerx.Customers
{
	public interface ICustomerAppService : IApplicationService, ITransientDependency
	{
		Task CreateOrUpdateCustomer(CreateOrUpdateCustomerInput input);

		Task DeleteCustomer(IdInput input);

		Task<Customer> GetCustomerById(long customerId);

		Task<GetCustomerForEditOutput> GetCustomerForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<CustomerListDto>> GetCustomers(GetCustomersInput input);

		Task<List<NameValue>> GetCustomersAddresses(long customerId);

		Task<List<NameValue>> GetCustomersAfterAddNew();

		Task<List<Customer>> GetCustomersForBusiness();

		Task<FileDto> GetCustomersToExcel();

		Task<FileDto> GetCustomerWithAddressesToExcel();

		Task<string> TypeAheadCustomers(string q);
	}
}