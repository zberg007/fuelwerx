using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using FuelWerx;
using FuelWerx.Authorization.Users;
using FuelWerx.Dto;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.ServiceTanks;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FuelWerx.Suppliers
{
	public class GenericAppService : FuelWerxAppServiceBase, IGenericAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Country> _countryRepository;

		private readonly IRepository<CountryRegion> _countryRegionRepository;

		private readonly IRepository<Phone, long> _phoneRepository;

		private readonly IRepository<Service, long> _serviceRepository;

		private readonly IRepository<DriversData, long> _driversDataRepository;

		private readonly IRepository<Address, long> _addressRepository;

		private readonly IRepository<QuickMenuItem, long> _quickMenuItemRepository;

		private readonly IRepository<Bank, long> _bankRepository;

		private readonly FuelWerx.Authorization.Users.UserManager _userManager;

		private readonly IRepository<ServiceTank, long> _serviceTankRepository;

		public GenericAppService(IRepository<Country> countryRepository, IRepository<CountryRegion> countryRegionRepository, IRepository<Phone, long> phoneRepository, IRepository<Service, long> serviceRepository, IRepository<Address, long> addressRepository, IRepository<DriversData, long> driversDataRepository, IRepository<QuickMenuItem, long> quickMenuItemRepository, IRepository<Bank, long> bankRepository, FuelWerx.Authorization.Users.UserManager userManager, IRepository<ServiceTank, long> serviceTankRepository)
		{
			this._countryRepository = countryRepository;
			this._countryRegionRepository = countryRegionRepository;
			this._phoneRepository = phoneRepository;
			this._driversDataRepository = driversDataRepository;
			this._serviceRepository = serviceRepository;
			this._addressRepository = addressRepository;
			this._quickMenuItemRepository = quickMenuItemRepository;
			this._bankRepository = bankRepository;
			this._userManager = userManager;
			this._serviceTankRepository = serviceTankRepository;
		}

		public async Task<long> CreateOrUpdateAddress(CreateOrUpdateAddressInput input)
		{
			long id;
			Address nullable = input.Address.MapTo<Address>();
			double? longitude = nullable.Longitude;
			if (longitude.HasValue)
			{
				longitude = nullable.Latitude;
				if (longitude.HasValue)
				{
					goto Label0;
				}
			}
			string empty = string.Empty;
			if (nullable.CountryRegionId.HasValue)
			{
				IRepository<CountryRegion> repository = this._countryRegionRepository;
				int? countryRegionId = nullable.CountryRegionId;
				empty = (await repository.GetAsync(countryRegionId.Value)).Code;
			}
			string[] primaryAddress = new string[] { nullable.PrimaryAddress, ",", nullable.City, ",", empty, ",", nullable.PostalCode };
			string str = string.Concat(primaryAddress);
			XDocument xDocument = XDocument.Load(WebRequest.Create(string.Format("http://maps.google.com/maps/api/geocode/xml?address={0}&sensor=false", str.Replace(",,", ",").Replace(" ", "+"))).GetResponse().GetResponseStream());
			if (!xDocument.ToString().Contains("<GeocodeResponse>") || xDocument.ToString().Contains("ZERO_RESULTS"))
			{
				longitude = null;
				nullable.Longitude = longitude;
				longitude = null;
				nullable.Latitude = longitude;
				nullable.Location = null;
				nullable.IsActive = false;
			}
			else
			{
				XElement xElement = xDocument.Element("GeocodeResponse").Element("result").Element("geometry").Element("location");
				nullable.Latitude = new double?(double.Parse(xElement.Element("lat").Value));
				nullable.Longitude = new double?(double.Parse(xElement.Element("lng").Value));
			}
			xDocument = null;
		Label0:
			longitude = nullable.Longitude;
			if (longitude.HasValue)
			{
				longitude = nullable.Latitude;
				if (longitude.HasValue)
				{
					Address address = nullable;
					longitude = nullable.Longitude;
					object value = longitude.Value;
					longitude = nullable.Latitude;
					address.Location = DbGeography.PointFromText(string.Format("POINT({0} {1})", value, longitude.Value), 4326);
				}
			}
			if (!input.Address.Id.HasValue)
			{
				id = await this._addressRepository.InsertAndGetIdAsync(nullable);
			}
			else
			{
				await this._addressRepository.UpdateAsync(nullable);
				id = nullable.Id;
			}
			return id;
		}

		public async Task CreateOrUpdateBank(CreateOrUpdateBankInput input)
		{
			if (!input.Bank.Id.HasValue)
			{
				await this._bankRepository.InsertAsync(input.Bank.MapTo<Bank>());
			}
			else
			{
				await this._bankRepository.UpdateAsync(input.Bank.MapTo<Bank>());
			}
		}

		public async Task CreateOrUpdateDriversData(CreateOrUpdateDriversDataInput input)
		{
			if (!input.DriversData.Id.HasValue)
			{
				await this._driversDataRepository.InsertAsync(input.DriversData.MapTo<DriversData>());
			}
			else
			{
				await this._driversDataRepository.UpdateAsync(input.DriversData.MapTo<DriversData>());
			}
		}

		public async Task CreateOrUpdatePhone(CreateOrUpdatePhoneInput input)
		{
			if (!input.Phone.Id.HasValue)
			{
				await this._phoneRepository.InsertAsync(input.Phone.MapTo<Phone>());
			}
			else
			{
				await this._phoneRepository.UpdateAsync(input.Phone.MapTo<Phone>());
			}
		}

		public async Task CreateOrUpdateQuickMenuItem(CreateOrUpdateQuickMenuItemInput input)
		{
			if (input.QuickMenuItem.OwnerId <= (long)0)
			{
				input.QuickMenuItem.OwnerId = this.AbpSession.UserId.Value;
			}
			if (!input.QuickMenuItem.Id.HasValue)
			{
				await this._quickMenuItemRepository.InsertAsync(input.QuickMenuItem.MapTo<QuickMenuItem>());
			}
			else
			{
				await this._quickMenuItemRepository.UpdateAsync(input.QuickMenuItem.MapTo<QuickMenuItem>());
			}
		}

		public async Task CreateOrUpdateService(CreateOrUpdateServiceInput input)
		{
			bool flag;
			long recordId;
			if (!input.Service.Id.HasValue)
			{
				flag = true;
				Service service = new Service();
				Mapper.Map<ServiceEditDto, Service>(input.Service, service);
				recordId = await this._serviceRepository.InsertAndGetIdAsync(service);
			}
			else
			{
				flag = false;
				recordId = input.Service.Id.Value;
				Service service1 = new Service();
				Mapper.Map<ServiceEditDto, Service>(input.Service, service1);
				service1.Tanks = null;
				await this._serviceRepository.UpdateAsync(service1);
			}
			IRepository<ServiceTank, long> repository = this._serviceTankRepository;
			List<ServiceTank> allListAsync = await repository.GetAllListAsync((ServiceTank x) => x.ServiceId == recordId);
			List<ServiceTank> serviceTanks = allListAsync;
			if (input.Service.Tanks.Any<ServiceTankDto>())
			{
				if (!flag)
				{
					if (!serviceTanks.Any<ServiceTank>())
					{
						foreach (ServiceTankDto tank in input.Service.Tanks)
						{
							tank.ServiceId = recordId;
							await this._serviceTankRepository.InsertAsync(tank.MapTo<ServiceTank>());
						}
					}
					else
					{
						List<ServiceTank> serviceTanks1 = serviceTanks;
						List<long> list = (
							from x in serviceTanks1
							select x.Id).ToList<long>();
						foreach (ServiceTankDto serviceTankDto in input.Service.Tanks)
						{
							if (serviceTankDto.Id <= (long)0)
							{
								serviceTankDto.ServiceId = recordId;
								await this._serviceTankRepository.InsertAndGetIdAsync(serviceTankDto.MapTo<ServiceTank>());
							}
							else
							{
								ServiceTank async = await this._serviceTankRepository.GetAsync(serviceTankDto.Id);
								async.Name = serviceTankDto.Name;
								async.Number = serviceTankDto.Number;
								async.BaseTemperature = serviceTankDto.BaseTemperature;
								async.Capacity = serviceTankDto.Capacity;
								async.RemainingCapacity = serviceTankDto.RemainingCapacity;
								async.Description = serviceTankDto.Description;
								async.LastInspectionComments = serviceTankDto.LastInspectionComments;
								async.LastInspectionDate = serviceTankDto.LastInspectionDate;
								async.IsActive = serviceTankDto.IsActive;
								async.IsOwned = serviceTankDto.IsOwned;
								await this._serviceTankRepository.UpdateAsync(async);
								list.Remove(async.Id);
								async = null;
							}
						}
						if (list.Any<long>())
						{
							IRepository<ServiceTank, long> repository1 = this._serviceTankRepository;
							await repository1.DeleteAsync((ServiceTank x) => x.ServiceId == recordId && list.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Service.Tanks.Any<ServiceTankDto>() && serviceTanks.Any<ServiceTank>())
			{
				List<ServiceTank> serviceTanks2 = serviceTanks;
				List<long> nums = (
					from x in serviceTanks2
					select x.Id).ToList<long>();
				IRepository<ServiceTank, long> repository2 = this._serviceTankRepository;
				await repository2.DeleteAsync((ServiceTank x) => x.ServiceId == recordId && nums.Contains(x.Id));
			}
		}

		public async Task DeleteAddress(IdInput<long> input)
		{
			await this._addressRepository.DeleteAsync(input.Id);
		}

		public async Task DeleteBank(IdInput<long> input)
		{
			await this._bankRepository.DeleteAsync(input.Id);
		}

		public async Task DeleteDriversData(IdInput<long> input)
		{
			await this._driversDataRepository.DeleteAsync(input.Id);
		}

		public async Task DeletePhone(IdInput<long> input)
		{
			await this._phoneRepository.DeleteAsync(input.Id);
		}

		public async Task DeleteQuickMenuItem(IdInput<long> input)
		{
			await this._quickMenuItemRepository.DeleteAsync(input.Id);
		}

		public async Task DeleteService(IdInput<long> input)
		{
			await this._serviceRepository.DeleteAsync(input.Id);
		}

		public async Task<PagedResultOutput<AddressListDto>> GetAddresses(GetAddressesInput input)
		{
			if (!input.OwnerId.HasValue || input.OwnerType.Length == 0)
			{
				throw new UserFriendlyException("MissingParameters");
			}
			IQueryable<Address> all = this._addressRepository.GetAll();
			IQueryable<Address> ownerId = 
				from p in all
				where p.OwnerId == input.OwnerId.Value
				select p;
			IQueryable<Address> addresses = ownerId.WhereIf<Address>(!string.IsNullOrEmpty(input.OwnerType), (Address p) => p.OwnerType == input.OwnerType);
			int num = await addresses.CountAsync<Address>();
			List<Address> listAsync = await addresses.OrderBy<Address>(input.Sorting, new object[0]).PageBy<Address>(input).ToListAsync<Address>();
			return new PagedResultOutput<AddressListDto>(num, listAsync.MapTo<List<AddressListDto>>());
		}

		public async Task<GetAddressForEditOutput> GetAddressForEdit(NullableIdInput<long> input)
		{
			AddressEditDto addressEditDto;
			if (!input.Id.HasValue)
			{
				addressEditDto = new AddressEditDto();
			}
			else
			{
				IRepository<Address, long> repository = this._addressRepository;
				Address async = await repository.GetAsync(input.Id.Value);
				addressEditDto = async.MapTo<AddressEditDto>();
			}
			return new GetAddressForEditOutput()
			{
				Address = addressEditDto
			};
		}

		public async Task<GetBankForEditOutput> GetBankForEdit(NullableIdInput<long> input)
		{
			BankEditDto bankEditDto;
			if (!input.Id.HasValue)
			{
				bankEditDto = new BankEditDto();
			}
			else
			{
				IRepository<Bank, long> repository = this._bankRepository;
				Bank async = await repository.GetAsync(input.Id.Value);
				bankEditDto = async.MapTo<BankEditDto>();
			}
			return new GetBankForEditOutput()
			{
				Bank = bankEditDto
			};
		}

		public async Task<PagedResultOutput<BankListDto>> GetBanks(GetBanksInput input)
		{
			bool flag;
			IQueryable<Bank> all = this._bankRepository.GetAll();
			flag = (!input.OwnerId.HasValue ? false : input.OwnerId.Value > (long)0);
			IQueryable<Bank> banks = all.WhereIf<Bank>(flag, (Bank p) => p.OwnerId == input.OwnerId.Value);
			IQueryable<Bank> banks1 = banks.WhereIf<Bank>(!string.IsNullOrEmpty(input.OwnerType), (Bank p) => p.OwnerType == input.OwnerType);
			int num = await banks1.CountAsync<Bank>();
			IQueryable<Bank> banks2 = banks1;
			List<Bank> listAsync = await (
				from o in banks2
				orderby o.AccountType
				select o).ToListAsync<Bank>();
			return new PagedResultOutput<BankListDto>(num, listAsync.MapTo<List<BankListDto>>());
		}

		public ListResultOutput<CountriesListDto> GetCountries(int? countryId)
		{
			return new ListResultOutput<CountriesListDto>((
				from p in this._countryRepository.GetAll().WhereIf<Country>((!countryId.HasValue ? false : countryId.Value > 0), (Country p) => (int?)p.Id == countryId)
				orderby p.Name
				select p).ToList<Country>().MapTo<List<CountriesListDto>>());
		}

		public ListResultOutput<CountryRegionInCountryListDto> GetCountryRegions(int? countryRegionId, int? countryId)
		{
			bool flag = true;
			IQueryable<CountryRegion> all = this._countryRegionRepository.GetAll();
			if (countryRegionId.HasValue && countryRegionId.Value > 0 && countryId.HasValue && countryId.Value > 0)
			{
				all = 
					from p in all
					where (int?)p.Id == countryRegionId && (int?)p.Country.Id == countryId
					select p;
			}
			else if (countryRegionId.HasValue && countryRegionId.Value > 0 && !countryId.HasValue)
			{
				all = 
					from p in all
					where (int?)p.Id == countryRegionId
					select p;
				flag = false;
			}
			else if (!countryRegionId.HasValue && countryId.HasValue && countryId.Value > 0)
			{
				all = 
					from p in all
					where (int?)p.Country.Id == countryId
					select p;
			}
			if (flag)
			{
				all = 
					from p in all
					orderby p.Name
					select p;
			}
			all.ToList<CountryRegion>();
			return new ListResultOutput<CountryRegionInCountryListDto>(all.MapTo<List<CountryRegionInCountryListDto>>());
		}

		public async Task<GetDriversDataForEditOutput> GetDriversDataForEdit(long driversId)
		{
			DriversDataEditDto driversDataEditDto;
			IQueryable<DriversData> all = this._driversDataRepository.GetAll();
			DriversData driversDatum = await (
				from m in all
				where m.OwnerId == driversId
				select m).FirstOrDefaultAsync<DriversData>();
			DriversData driversDatum1 = driversDatum;
			driversDataEditDto = (driversDatum1 == null ? new DriversDataEditDto()
			{
				OwnerId = driversId
			} : driversDatum1.MapTo<DriversDataEditDto>());
			User user = await this._userManager.FindByIdAsync(driversId);
			string empty = string.Empty;
			if (user != null)
			{
				empty = user.UserName.ToString();
			}
			return new GetDriversDataForEditOutput()
			{
				DriversData = driversDataEditDto,
				DriversDataName = empty
			};
		}

		public async Task<PagedResultOutput<DriversDataListDto>> GetDriversDatas(GetDriversDatasInput input)
		{
			bool flag;
			IQueryable<DriversData> all = this._driversDataRepository.GetAll();
			flag = (!input.OwnerId.HasValue ? false : input.OwnerId.Value > (long)0);
			IQueryable<DriversData> driversDatas = all.WhereIf<DriversData>(flag, (DriversData p) => p.OwnerId == input.OwnerId.Value);
			int num = await driversDatas.CountAsync<DriversData>();
			IQueryable<DriversData> driversDatas1 = driversDatas;
			List<DriversData> listAsync = await (
				from o in driversDatas1
				orderby o.CDLExpiration
				select o).ToListAsync<DriversData>();
			return new PagedResultOutput<DriversDataListDto>(num, listAsync.MapTo<List<DriversDataListDto>>());
		}

		public async Task<GetPhoneForEditOutput> GetPhoneForEdit(NullableIdInput<long> input)
		{
			PhoneEditDto phoneEditDto;
			if (!input.Id.HasValue)
			{
				phoneEditDto = new PhoneEditDto();
			}
			else
			{
				IRepository<Phone, long> repository = this._phoneRepository;
				Phone async = await repository.GetAsync(input.Id.Value);
				phoneEditDto = async.MapTo<PhoneEditDto>();
			}
			return new GetPhoneForEditOutput()
			{
				Phone = phoneEditDto
			};
		}

		public async Task<PagedResultOutput<PhoneListDto>> GetPhones(GetPhonesInput input)
		{
			bool flag;
			IQueryable<Phone> all = this._phoneRepository.GetAll();
			flag = (!input.OwnerId.HasValue ? false : input.OwnerId.Value > (long)0);
			IQueryable<Phone> phones = all.WhereIf<Phone>(flag, (Phone p) => p.OwnerId == input.OwnerId.Value);
			IQueryable<Phone> phones1 = phones.WhereIf<Phone>(!string.IsNullOrEmpty(input.OwnerType), (Phone p) => p.OwnerType == input.OwnerType);
			int num = await phones1.CountAsync<Phone>();
			IQueryable<Phone> phones2 = phones1;
			List<Phone> listAsync = await (
				from o in phones2
				orderby o.Type
				select o).ToListAsync<Phone>();
			return new PagedResultOutput<PhoneListDto>(num, listAsync.MapTo<List<PhoneListDto>>());
		}

		public async Task<GetQuickMenuItemForEditOutput> GetQuickMenuItemForEdit(NullableIdInput<long> input)
		{
			QuickMenuItemEditDto quickMenuItemEditDto;
			if (!input.Id.HasValue)
			{
				quickMenuItemEditDto = new QuickMenuItemEditDto();
			}
			else
			{
				IRepository<QuickMenuItem, long> repository = this._quickMenuItemRepository;
				QuickMenuItem async = await repository.GetAsync(input.Id.Value);
				quickMenuItemEditDto = async.MapTo<QuickMenuItemEditDto>();
			}
			return new GetQuickMenuItemForEditOutput()
			{
				QuickMenuItem = quickMenuItemEditDto
			};
		}

		public async Task<List<QuickMenuItemListDto>> GetQuickMenuItems(GetQuickMenuItemsInput input)
		{
			long? userId;
			bool value;
			bool flag;
			long? ownerId = input.OwnerId;
			if (ownerId.HasValue)
			{
				ownerId = input.OwnerId;
				if (ownerId.HasValue)
				{
					ownerId = input.OwnerId;
					userId = this.AbpSession.UserId;
					flag = (ownerId.GetValueOrDefault() == userId.GetValueOrDefault() ? ownerId.HasValue != userId.HasValue : true);
					if (!flag)
					{
						goto Label0;
					}
				}
				else
				{
					goto Label0;
				}
			}
			input.OwnerId = this.AbpSession.UserId;
		Label0:
			IQueryable<QuickMenuItem> all = this._quickMenuItemRepository.GetAll();
			userId = input.OwnerId;
			if (!userId.HasValue)
			{
				value = false;
			}
			else
			{
				userId = input.OwnerId;
				value = userId.Value > (long)0;
			}
			IQueryable<QuickMenuItem> quickMenuItems = all.WhereIf<QuickMenuItem>(value, (QuickMenuItem p) => p.OwnerId == input.OwnerId.Value);
			await quickMenuItems.CountAsync<QuickMenuItem>();
			IQueryable<QuickMenuItem> quickMenuItems1 = quickMenuItems;
			List<QuickMenuItem> listAsync = await (
				from o in quickMenuItems1
				orderby o.Label
				select o).ToListAsync<QuickMenuItem>();
			return new List<QuickMenuItemListDto>(listAsync.MapTo<List<QuickMenuItemListDto>>());
		}

		public async Task<GetServiceForEditOutput> GetServiceForEdit(NullableIdInput<long> input)
		{
			ServiceEditDto serviceEditDto;
			if (!input.Id.HasValue)
			{
				serviceEditDto = new ServiceEditDto()
				{
					Tanks = new List<ServiceTankDto>()
				};
			}
			else
			{
				IRepository<Service, long> repository = this._serviceRepository;
				Service async = await repository.GetAsync(input.Id.Value);
				serviceEditDto = async.MapTo<ServiceEditDto>();
				IRepository<ServiceTank, long> repository1 = this._serviceTankRepository;
				List<ServiceTank> allListAsync = await repository1.GetAllListAsync((ServiceTank m) => m.ServiceId == async.Id);
				serviceEditDto.Tanks = allListAsync.MapTo<List<ServiceTankDto>>();
			}
			return new GetServiceForEditOutput()
			{
				Service = serviceEditDto
			};
		}

		public async Task<PagedResultOutput<ServiceListDto>> GetServices(GetServicesInput input)
		{
			bool flag;
			IQueryable<Service> all = this._serviceRepository.GetAll();
			flag = (!input.OwnerId.HasValue ? false : input.OwnerId.Value > (long)0);
			IQueryable<Service> services = all.WhereIf<Service>(flag, (Service p) => p.OwnerId == input.OwnerId.Value);
			bool hasValue = input.AddressId.HasValue;
			IQueryable<Service> services1 = services.WhereIf<Service>(hasValue, (Service p) => (long?)p.AddressId == input.AddressId);
			IQueryable<Service> services2 = services1.WhereIf<Service>(!string.IsNullOrEmpty(input.OwnerType), (Service p) => p.OwnerType == input.OwnerType);
			int num = await services2.CountAsync<Service>();
			IQueryable<Service> services3 = services2;
			List<Service> listAsync = await (
				from o in services3
				orderby o.Type
				select o).ToListAsync<Service>();
			return new PagedResultOutput<ServiceListDto>(num, listAsync.MapTo<List<ServiceListDto>>());
		}
	}
}