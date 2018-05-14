using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Customers.Dto;
using FuelWerx.Customers.Exporting;
using FuelWerx.Customers.Model;
using FuelWerx.Dto;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Customers
{
	[AbpAuthorize(new string[] { "Pages.Tenant.Customers", "Rights.Customer.CanAddPayments", "Rights.Customer.CanViewPayments" })]
	public class CustomerAppService : FuelWerxAppServiceBase, ICustomerAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Customer, long> _customerRepository;

		private readonly IRepository<Address, long> _addressRepository;

		private readonly IRepository<Phone, long> _phoneRepository;

		private readonly ICustomerListExcelExporter _customerListExcelExporter;

		private readonly IGenericAppService _genericAppService;

		public CustomerAppService(IRepository<Customer, long> customerRepository, IRepository<Address, long> addressRepository, IRepository<Phone, long> phoneRepository, ICustomerListExcelExporter customerListExcelExporter, IGenericAppService genericAppService)
		{
			this._customerRepository = customerRepository;
			this._addressRepository = addressRepository;
			this._phoneRepository = phoneRepository;
			this._customerListExcelExporter = customerListExcelExporter;
			this._genericAppService = genericAppService;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Customers.Create" })]
		public async Task CreateOrUpdateCustomer(CreateOrUpdateCustomerInput input)
		{
			if (!input.Customer.Id.HasValue)
			{
				await this._customerRepository.InsertAsync(input.Customer.MapTo<Customer>());
			}
			else
			{
				await this._customerRepository.UpdateAsync(input.Customer.MapTo<Customer>());
			}
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Customers.Delete" })]
		public async Task DeleteCustomer(IdInput input)
		{
			await this._customerRepository.DeleteAsync((long)input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Customers", "Rights.Customer.CanViewPayments" })]
		public async Task<Customer> GetCustomerById(long customerId)
		{
			return await this._customerRepository.GetAsync(customerId);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Customers.Create", "Pages.Tenant.Customers.Edit", "Rights.Customer.CanAddPayments" })]
		public async Task<GetCustomerForEditOutput> GetCustomerForEdit(NullableIdInput<long> input)
		{
			CustomerEditDto customerEditDto;
			if (!input.Id.HasValue)
			{
				customerEditDto = new CustomerEditDto();
			}
			else
			{
				IRepository<Customer, long> repository = this._customerRepository;
				Customer async = await repository.GetAsync(input.Id.Value);
				customerEditDto = async.MapTo<CustomerEditDto>();
			}
			return new GetCustomerForEditOutput()
			{
				Customer = customerEditDto
			};
		}

		public async Task<PagedResultOutput<CustomerListDto>> GetCustomers(GetCustomersInput input)
		{
			PagedResultOutput<CustomerListDto> pagedResultOutput;
			ParameterExpression parameterExpression;
			if (input.Filter.Contains("id:"))
			{
				string filter = input.Filter;
				char[] chrArray = new char[] { ':' };
				long theId = long.Parse(filter.Split(chrArray)[1]);
				IQueryable<Customer> all = _customerRepository.GetAll();
                var customers = all.Where(m => m.Id == theId);
				int num = await customers.CountAsync<Customer>();
				List<Customer> listAsync = await customers.OrderBy<Customer>(input.Sorting, new object[0]).PageBy<Customer>(input).ToListAsync<Customer>();
				pagedResultOutput = new PagedResultOutput<CustomerListDto>(num, listAsync.MapTo<List<CustomerListDto>>());
			}
			else
			{
				IQueryable<Address> addresses = this._addressRepository.GetAll();
                var addresses1 = addresses.Include(i => i.Country);
                var addresses2 = addresses1.Include(i => i.CountryRegion);
                var addresses3 = addresses2.Where(m => (int?)m.TenantId == AbpSession.TenantId && m.OwnerType == "Customer");
                var addresses4 = addresses3.WhereIf(!input.Filter.IsNullOrEmpty(),
					p => p.Type.Contains(input.Filter) ||
					p.Type.Contains(input.Filter) ||
					p.PrimaryAddress.Contains(input.Filter) ||
					p.SecondaryAddress.Contains(input.Filter) ||
					p.City.Contains(input.Filter) ||
					p.PostalCode.Contains(input.Filter) ||
					p.ContactName.Contains(input.Filter) ||
					p.Country.Code.Contains(input.Filter) ||
					p.Country.Name.Contains(input.Filter) ||
					p.CountryRegion.Code.Contains(input.Filter) ||
					p.CountryRegion.Name.Contains(input.Filter));

                var collection = await addresses4.Select(s => new
                {
                    CustomerId = s.OwnerId
                }).ToListAsync();
				IQueryable<Phone> phones = this._phoneRepository.GetAll();
                var phones1 = phones.Where(m => (int?)m.TenantId == AbpSession.TenantId && m.OwnerType == "Customer");
                var phones2 = phones.WhereIf(!input.Filter.IsNullOrEmpty(), 
                    p => p.Type.Contains(input.Filter) ||
                    p.PhoneNumber.Contains(input.Filter));
                var listAsync1 = await phones2.Select(p => new
                {
                    CustomerId = p.OwnerId
                }).ToListAsync();
				IQueryable<Customer> all1 = this._customerRepository.GetAll();
                var customers1 = all1.WhereIf(!input.Filter.IsNullOrEmpty(), p => 
                    p.FirstName.Contains(input.Filter) ||
                    p.LastName.Contains(input.Filter) ||
                    p.BusinessName.Contains(input.Filter) ||
                    p.Email.Contains(input.Filter));
                var listAsync2 = await customers1.Select(s => new
                {
                    CustomerId = s.Id
                }).ToListAsync();
				List<long> list = (
					from s in collection
					select s.CustomerId).ToList();
				IEnumerable<long> nums = list.Union((
					from s in listAsync1
					select s.CustomerId).ToList());
				nums.Union((
					from s in listAsync2
					select s.CustomerId).ToList());
                var all2 = _customerRepository.GetAll();
				parameterExpression = Expression.Parameter(typeof(Customer), "m");
                var customers2 = all2.Where(m => nums.Contains(m.Id));
				int matchCount = await customers2.CountAsync();
				List<Customer> listAsync3 = await customers2.OrderBy<Customer>(input.Sorting, new object[0]).PageBy<Customer>(input).ToListAsync<Customer>();
				pagedResultOutput = new PagedResultOutput<CustomerListDto>(matchCount, listAsync3.MapTo<List<CustomerListDto>>());
			}
			return pagedResultOutput;
		}

		public async Task<List<NameValue>> GetCustomersAddresses(long customerId)
		{
			string str;
			await this._customerRepository.GetAsync(customerId);
			IRepository<Address, long> repository = this._addressRepository;
			List<Address> allListAsync = await repository.GetAllListAsync((Address x) => x.OwnerId == customerId && x.OwnerType == "Customer");
			List<NameValue> nameValues = new List<NameValue>();
			foreach (Address address in allListAsync)
			{
				ListResultOutput<CountryRegionInCountryListDto> countryRegions = this._genericAppService.GetCountryRegions(address.CountryRegionId, null);
				string[] primaryAddress = new string[] { address.PrimaryAddress, " ", address.City, null, null, null };
				str = (countryRegions.Items.Count == 1 ? string.Concat(", ", countryRegions.Items[0].Code) : "");
				primaryAddress[3] = str;
				primaryAddress[4] = " ";
				primaryAddress[5] = address.PostalCode;
				string str1 = string.Concat(primaryAddress);
				long id = address.Id;
				nameValues.Add(new NameValue(id.ToString(), str1));
			}
			return nameValues;
		}

		public async Task<List<NameValue>> GetCustomersAfterAddNew()
		{
			IRepository<Customer, long> repository = this._customerRepository;
			List<Customer> allListAsync = await repository.GetAllListAsync((Customer x) => x.IsActive);
			List<NameValue> nameValues = new List<NameValue>();
			foreach (Customer customer in allListAsync)
			{
				string str = string.Concat(customer.LastName, ", ", customer.FirstName);
				long id = customer.Id;
				nameValues.Add(new NameValue(id.ToString(), str));
			}
			return nameValues;
		}

		public async Task<List<Customer>> GetCustomersForBusiness()
		{
			IRepository<Customer, long> repository = this._customerRepository;
			List<Customer> allListAsync = await repository.GetAllListAsync((Customer a) => a.IsActive);
			List<Customer> list = (
				from o in allListAsync
				orderby o.LastName
				select o).ToList<Customer>();
			return list;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Customers.ExportData" })]
		public async Task<FileDto> GetCustomersToExcel()
		{
			List<Customer> allListAsync = await this._customerRepository.GetAllListAsync();
			List<CustomerListDto> customerListDtos = allListAsync.MapTo<List<CustomerListDto>>();
			return this._customerListExcelExporter.ExportToFile(customerListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Customers.ExportData" })]
		public async Task<FileDto> GetCustomerWithAddressesToExcel()
		{
			string str;
			string str1;
			List<Customer> allListAsync = await this._customerRepository.GetAllListAsync();
			List<CustomerWithAddressListDto> customerWithAddressListDtos = new List<CustomerWithAddressListDto>();
			foreach (Customer customer in allListAsync)
			{
				IRepository<Address, long> repository = this._addressRepository;
				List<Address> addresses = await repository.GetAllListAsync((Address m) => m.OwnerId == customer.Id && m.OwnerType == "Customer");
				foreach (Address address in addresses)
				{
					CustomerWithAddressListDto customerWithAddressListDto = new CustomerWithAddressListDto()
					{
						CustomerId = customer.Id,
						AllowBillPay = customer.AllowBillPay,
						IsActive = customer.IsActive,
						FirstName = customer.FirstName,
						LastName = customer.LastName,
						BusinessName = customer.BusinessName,
						CreationTime = new DateTime?(customer.CreationTime)
					};
					CustomerWithAddressListDto primaryAddress = customerWithAddressListDto;
					primaryAddress.PrimaryAddress = address.PrimaryAddress;
					primaryAddress.SecondaryAddress = address.SecondaryAddress;
					primaryAddress.City = address.City;
					int? countryRegionId = address.CountryRegionId;
					if (!countryRegionId.HasValue)
					{
						primaryAddress.CountryRegionCode = "";
					}
					else
					{
						IGenericAppService genericAppService = this._genericAppService;
						countryRegionId = address.CountryRegionId;
						int? nullable = new int?(countryRegionId.Value);
						countryRegionId = null;
						ListResultOutput<CountryRegionInCountryListDto> countryRegions = genericAppService.GetCountryRegions(nullable, countryRegionId);
						CustomerWithAddressListDto customerWithAddressListDto1 = primaryAddress;
						str1 = (countryRegions == null || countryRegions.Items.Count != 1 ? "" : countryRegions.Items[0].Code);
						customerWithAddressListDto1.CountryRegionCode = str1;
					}
					if (address.CountryId <= 0)
					{
						primaryAddress.CountryCode = "";
					}
					else
					{
						ListResultOutput<CountriesListDto> countries = this._genericAppService.GetCountries(new int?(address.CountryId));
						CustomerWithAddressListDto customerWithAddressListDto2 = primaryAddress;
						str = (countries == null || countries.Items.Count != 1 ? "" : countries.Items[0].Code);
						customerWithAddressListDto2.CountryCode = str;
					}
					primaryAddress.PostalCode = address.PostalCode;
					primaryAddress.Latitude = address.Latitude;
					primaryAddress.Longitude = address.Longitude;
					primaryAddress.ContactName = address.ContactName;
					primaryAddress.Type = address.Type;
					primaryAddress.AddressId = address.Id;
					primaryAddress.AddressIsActive = address.IsActive;
					customerWithAddressListDtos.Add(primaryAddress);
				}
			}
			return this._customerListExcelExporter.ExportToFile(customerWithAddressListDtos);
		}

		public async Task<string> TypeAheadCustomers(string q)
		{
			string str;
			str = (string.IsNullOrEmpty(q) ? "" : q.Trim());
			string str1 = str;
			string empty = string.Empty;
			IRepository<Customer, long> repository = this._customerRepository;
			List<Customer> allListAsync = await repository.GetAllListAsync((Customer x) => ((x.FirstName + " ") + x.LastName).StartsWith(str1) || ((x.FirstName + " ") + x.LastName).Contains(str1) || x.FirstName.StartsWith(str1) || x.LastName.StartsWith(str1) || x.BusinessName.StartsWith(str1) || x.BusinessName.Contains(str1) || x.Email.Contains(str1) || x.Email.StartsWith(str1) || ((((x.FirstName + " ") + x.LastName) + " - ") + x.Email) == str1 || ((((x.FirstName + " ") + x.LastName) + " - ") + x.Email).StartsWith(str1));
			IEnumerable<TypeAheadCustomers> typeAheadCustomer =
				from c in allListAsync
				select new TypeAheadCustomers()
				{
					id = c.Id,
					name = string.Concat(((c.FullName.Length > 0 ? c.FullName : c.BusinessName)).Replace("'", "&apos;"), " - ", c.Email)
				};
			string str2 = JsonConvert.SerializeObject((
				from o in typeAheadCustomer
				orderby o.name
				select o).ToList<TypeAheadCustomers>());
			return str2;
		}
	}
}