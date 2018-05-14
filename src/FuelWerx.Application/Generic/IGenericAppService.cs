using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Generic.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelWerx.Generic
{
	public interface IGenericAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateAddress(CreateOrUpdateAddressInput input);

		Task CreateOrUpdateBank(CreateOrUpdateBankInput input);

		Task CreateOrUpdateDriversData(CreateOrUpdateDriversDataInput input);

		Task CreateOrUpdatePhone(CreateOrUpdatePhoneInput input);

		Task CreateOrUpdateQuickMenuItem(CreateOrUpdateQuickMenuItemInput input);

		Task CreateOrUpdateService(CreateOrUpdateServiceInput input);

		Task DeleteAddress(IdInput<long> input);

		Task DeleteBank(IdInput<long> input);

		Task DeleteDriversData(IdInput<long> input);

		Task DeletePhone(IdInput<long> input);

		Task DeleteQuickMenuItem(IdInput<long> input);

		Task DeleteService(IdInput<long> input);

		Task<PagedResultOutput<AddressListDto>> GetAddresses(GetAddressesInput input);

		Task<GetAddressForEditOutput> GetAddressForEdit(NullableIdInput<long> input);

		Task<GetBankForEditOutput> GetBankForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<BankListDto>> GetBanks(GetBanksInput input);

		ListResultOutput<CountriesListDto> GetCountries(int? countryId = null);

		ListResultOutput<CountryRegionInCountryListDto> GetCountryRegions(int? countryRegionId = null, int? countryId = null);

		Task<GetDriversDataForEditOutput> GetDriversDataForEdit(long driversId);

		Task<PagedResultOutput<DriversDataListDto>> GetDriversDatas(GetDriversDatasInput input);

		Task<GetPhoneForEditOutput> GetPhoneForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<PhoneListDto>> GetPhones(GetPhonesInput input);

		Task<GetQuickMenuItemForEditOutput> GetQuickMenuItemForEdit(NullableIdInput<long> input);

		Task<List<QuickMenuItemListDto>> GetQuickMenuItems(GetQuickMenuItemsInput input);

		Task<GetServiceForEditOutput> GetServiceForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<ServiceListDto>> GetServices(GetServicesInput input);
	}
}