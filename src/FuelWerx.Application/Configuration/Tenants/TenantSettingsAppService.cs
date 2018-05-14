using Abp;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.Zero.Ldap.Configuration;
using FuelWerx;
using FuelWerx.Configuration.Host.Dto;
using FuelWerx.Configuration.Tenants.Dto;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.MultiTenancy;
using FuelWerx.Tenants;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FuelWerx.Configuration.Tenants
{
	public class TenantSettingsAppService : FuelWerxAppServiceBase, ITenantSettingsAppService, IApplicationService, ITransientDependency
	{
		private readonly IMultiTenancyConfig _multiTenancyConfig;

		private readonly IRepository<TenantLogos, long> _tenantLogosRepository;

		private readonly IRepository<TenantHour, long> _tenantHoursRepository;

		private readonly IRepository<TenantCustomerService, long> _tenantCustomerServicesRepository;

		private readonly IRepository<TenantNotifications, long> _tenantNotificationsRepository;

		private readonly IRepository<TenantDateTimeSettings, long> _tenantDateTimeSettingsRepository;

		private readonly IRepository<TenantPaymentSettings, long> _tenantPaymentSettingsRepository;

		private readonly IRepository<TenantPaymentGatewaySettings, long> _tenantPaymentGatewaySettingsRepository;

		private readonly IAbpZeroLdapModuleConfig _ldapModuleConfig;

		private readonly IRepository<TenantDetail, long> _tenantDetailRepository;

		private readonly IRepository<CountryRegion> _countryRegionRepository;

		private readonly IRepository<Country> _countryRepository;

		private object invoiceNumberLock = new object();

		public TenantSettingsAppService(IMultiTenancyConfig multiTenancyConfig, IRepository<TenantLogos, long> tenantLogosRepository, IRepository<TenantHour, long> tenantHoursRepository, IRepository<TenantCustomerService, long> tenantCustomerServicesRepository, IRepository<TenantDetail, long> tenantDetailRepository, IRepository<TenantDateTimeSettings, long> tenantDateTimeSettingsRepository, IRepository<TenantPaymentSettings, long> tenantPaymentSettingsRepository, IRepository<TenantPaymentGatewaySettings, long> tenantPaymentGatewaySettingsRepository, IRepository<TenantNotifications, long> tenantNotificationsRepository, IRepository<CountryRegion> countryRegionRepository, IRepository<Country> countryRepository, IAbpZeroLdapModuleConfig ldapModuleConfig)
		{
			this._multiTenancyConfig = multiTenancyConfig;
			this._tenantLogosRepository = tenantLogosRepository;
			this._tenantHoursRepository = tenantHoursRepository;
			this._tenantCustomerServicesRepository = tenantCustomerServicesRepository;
			this._tenantPaymentSettingsRepository = tenantPaymentSettingsRepository;
			this._tenantPaymentGatewaySettingsRepository = tenantPaymentGatewaySettingsRepository;
			this._tenantNotificationsRepository = tenantNotificationsRepository;
			this._tenantDateTimeSettingsRepository = tenantDateTimeSettingsRepository;
			this._tenantDetailRepository = tenantDetailRepository;
			this._countryRegionRepository = countryRegionRepository;
			this._countryRepository = countryRepository;
			this._ldapModuleConfig = ldapModuleConfig;
		}

		public async Task<bool> CheckForInvoiceNumber()
		{
			bool flag;
			int? impersonatorTenantId;
			int value;
			if (this.AbpSession.ImpersonatorTenantId.HasValue)
			{
				impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
				value = impersonatorTenantId.Value;
			}
			else
			{
				impersonatorTenantId = this.AbpSession.TenantId;
				value = impersonatorTenantId.Value;
			}
			int num = value;
			IRepository<TenantPaymentSettings, long> repository = this._tenantPaymentSettingsRepository;
			List<TenantPaymentSettings> allListAsync = await repository.GetAllListAsync((TenantPaymentSettings x) => x.TenantId == num);
			flag = (allListAsync[0].InvoiceNumber_StartingNumber.HasValue ? true : false);
			return flag;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Tenant.Settings" })]
		public async Task<TenantSettingsEditDto> GetAllSettings(int? tenantId = null)
		{
			int? countryRegionId;
			CountryRegionDto countryRegionDto;
			CountryDto countryDto;
			int num;
			TenantDetailEditDto details;
			num = (tenantId.HasValue ? tenantId.Value : this.AbpSession.GetTenantId());
			int num1 = num;
			TenantSettingsEditDto tenantSettingsEditDto = new TenantSettingsEditDto();
			TenantSettingsEditDto tenantSettingsEditDto1 = tenantSettingsEditDto;
			TenantUserManagementSettingsEditDto tenantUserManagementSettingsEditDto = new TenantUserManagementSettingsEditDto();
			TenantUserManagementSettingsEditDto tenantUserManagementSettingsEditDto1 = tenantUserManagementSettingsEditDto;
			bool settingValueAsync = await this.SettingManager.GetSettingValueAsync<bool>("App.UserManagement.AllowSelfRegistration");
			tenantUserManagementSettingsEditDto1.AllowSelfRegistration = settingValueAsync;
			TenantUserManagementSettingsEditDto tenantUserManagementSettingsEditDto2 = tenantUserManagementSettingsEditDto;
			bool flag = await this.SettingManager.GetSettingValueAsync<bool>("App.UserManagement.IsNewRegisteredUserActiveByDefault");
			tenantUserManagementSettingsEditDto2.IsNewRegisteredUserActiveByDefault = flag;
			TenantUserManagementSettingsEditDto tenantUserManagementSettingsEditDto3 = tenantUserManagementSettingsEditDto;
			bool settingValueAsync1 = await this.SettingManager.GetSettingValueAsync<bool>("Abp.Zero.UserManagement.IsEmailConfirmationRequiredForLogin");
			tenantUserManagementSettingsEditDto3.IsEmailConfirmationRequiredForLogin = settingValueAsync1;
			TenantUserManagementSettingsEditDto tenantUserManagementSettingsEditDto4 = tenantUserManagementSettingsEditDto;
			bool flag1 = await this.SettingManager.GetSettingValueAsync<bool>("App.UserManagement.UseCaptchaOnRegistration");
			tenantUserManagementSettingsEditDto4.UseCaptchaOnRegistration = flag1;
			tenantUserManagementSettingsEditDto.RequireOnePhoneNumberForRegistration = this.SettingManager.GetSettingValue<bool>("App.UserManagement.RequireOnePhoneNumberForRegistration");
			tenantUserManagementSettingsEditDto.SendEmailAfterRegistration = this.SettingManager.GetSettingValue<bool>("App.UserManagement.SendEmailAfterRegistration");
			tenantUserManagementSettingsEditDto.SendEmailAfterRegistrationMessage = this.SettingManager.GetSettingValue("App.UserManagement.SendEmailAfterRegistrationMessage");
			TenantUserManagementSettingsEditDto tenantUserManagementSettingsEditDto5 = tenantUserManagementSettingsEditDto;
			bool settingValueAsync2 = await this.SettingManager.GetSettingValueAsync<bool>("App.UserManagment.EnableEmergencyDeliveryFees");
			tenantUserManagementSettingsEditDto5.EnableEmergencyDeliveryFees = settingValueAsync2;
			tenantSettingsEditDto1.UserManagement = tenantUserManagementSettingsEditDto;
			TenantSettingsEditDto tenantHoursEditDto = tenantSettingsEditDto;
			tenantSettingsEditDto1 = null;
			tenantUserManagementSettingsEditDto1 = null;
			tenantUserManagementSettingsEditDto2 = null;
			tenantUserManagementSettingsEditDto3 = null;
			tenantUserManagementSettingsEditDto4 = null;
			tenantUserManagementSettingsEditDto5 = null;
			tenantUserManagementSettingsEditDto = null;
			tenantSettingsEditDto = null;
			Tenant currentTenantById = this.GetCurrentTenantById(num1);
			IRepository<TenantDetail, long> repository = this._tenantDetailRepository;
			TenantDetail tenantDetail = await repository.FirstOrDefaultAsync((TenantDetail m) => m.TenantId == currentTenantById.Id);
			TenantDetail tenantDetail1 = tenantDetail;
			if (tenantDetail1 == null)
			{
				TenantSettingsEditDto tenantSettingsEditDto2 = tenantHoursEditDto;
				TenantDetailEditDto tenantDetailEditDto = new TenantDetailEditDto()
				{
					TenantId = currentTenantById.Id,
					CountryRegion = new CountryRegionDto(),
					MailCountryRegion = new CountryRegionDto(),
					Country = new CountryDto(),
					MailCountry = new CountryDto()
				};
				tenantSettingsEditDto2.Details = tenantDetailEditDto;
			}
			else
			{
				tenantHoursEditDto.Details = tenantDetail1.MapTo<TenantDetailEditDto>();
				if (!tenantHoursEditDto.Details.CountryRegionId.HasValue)
				{
					tenantHoursEditDto.Details.CountryRegion = new CountryRegionDto();
				}
				else
				{
					details = tenantHoursEditDto.Details;
					IRepository<CountryRegion> repository1 = this._countryRegionRepository;
					countryRegionId = tenantHoursEditDto.Details.CountryRegionId;
					CountryRegion async = await repository1.GetAsync(countryRegionId.Value);
					countryRegionDto = async.MapTo<CountryRegionDto>();
					details.CountryRegion = countryRegionDto;
					details = null;
				}
				if (tenantHoursEditDto.Details.CountryId <= 0)
				{
					tenantHoursEditDto.Details.Country = new CountryDto();
				}
				else
				{
					details = tenantHoursEditDto.Details;
					Country country = await this._countryRepository.GetAsync(tenantHoursEditDto.Details.CountryId);
					countryDto = country.MapTo<CountryDto>();
					details.Country = countryDto;
					details = null;
				}
				if (!tenantHoursEditDto.Details.MailCountryRegionId.HasValue)
				{
					tenantHoursEditDto.Details.MailCountryRegion = new CountryRegionDto();
				}
				else
				{
					details = tenantHoursEditDto.Details;
					IRepository<CountryRegion> repository2 = this._countryRegionRepository;
					countryRegionId = tenantHoursEditDto.Details.MailCountryRegionId;
					CountryRegion countryRegion = await repository2.GetAsync(countryRegionId.Value);
					countryRegionDto = countryRegion.MapTo<CountryRegionDto>();
					details.MailCountryRegion = countryRegionDto;
					details = null;
				}
				if (tenantHoursEditDto.Details.MailCountryId <= 0)
				{
					tenantHoursEditDto.Details.MailCountry = new CountryDto();
				}
				else
				{
					details = tenantHoursEditDto.Details;
					Country async1 = await this._countryRepository.GetAsync(tenantHoursEditDto.Details.MailCountryId);
					countryDto = async1.MapTo<CountryDto>();
					details.MailCountry = countryDto;
					details = null;
				}
			}
			IRepository<TenantHour, long> repository3 = this._tenantHoursRepository;
			TenantHour tenantHour = await repository3.FirstOrDefaultAsync((TenantHour m) => m.TenantId == currentTenantById.Id);
			TenantHour tenantHour1 = tenantHour;
			if (tenantHour1 == null)
			{
				tenantHoursEditDto.Hours = new TenantHoursEditDto()
				{
					TenantId = currentTenantById.Id
				};
			}
			else
			{
				tenantHoursEditDto.Hours = tenantHour1.MapTo<TenantHoursEditDto>();
			}
			IRepository<TenantCustomerService, long> repository4 = this._tenantCustomerServicesRepository;
			TenantCustomerService tenantCustomerService = await repository4.FirstOrDefaultAsync((TenantCustomerService m) => m.TenantId == currentTenantById.Id);
			TenantCustomerService tenantCustomerService1 = tenantCustomerService;
			if (tenantCustomerService1 == null)
			{
				tenantHoursEditDto.CustomerService = new TenantCustomerServiceEditDto()
				{
					TenantId = currentTenantById.Id
				};
			}
			else
			{
				tenantHoursEditDto.CustomerService = tenantCustomerService1.MapTo<TenantCustomerServiceEditDto>();
			}
			IRepository<TenantNotifications, long> repository5 = this._tenantNotificationsRepository;
			TenantNotifications tenantNotification = await repository5.FirstOrDefaultAsync((TenantNotifications m) => m.TenantId == currentTenantById.Id);
			TenantNotifications tenantNotification1 = tenantNotification;
			if (tenantNotification1 == null)
			{
				tenantHoursEditDto.Notifications = new TenantNotificationsEditDto()
				{
					TenantId = currentTenantById.Id
				};
			}
			else
			{
				tenantHoursEditDto.Notifications = tenantNotification1.MapTo<TenantNotificationsEditDto>();
			}
			IRepository<TenantPaymentSettings, long> repository6 = this._tenantPaymentSettingsRepository;
			TenantPaymentSettings tenantPaymentSetting = await repository6.FirstOrDefaultAsync((TenantPaymentSettings m) => m.TenantId == currentTenantById.Id);
			TenantPaymentSettings tenantPaymentSetting1 = tenantPaymentSetting;
			if (tenantPaymentSetting1 == null)
			{
				tenantHoursEditDto.PaymentSettings = new TenantPaymentSettingsEditDto()
				{
					TenantId = currentTenantById.Id
				};
			}
			else
			{
				tenantHoursEditDto.PaymentSettings = tenantPaymentSetting1.MapTo<TenantPaymentSettingsEditDto>();
			}
			IRepository<TenantPaymentGatewaySettings, long> repository7 = this._tenantPaymentGatewaySettingsRepository;
			TenantPaymentGatewaySettings tenantPaymentGatewaySetting = await repository7.FirstOrDefaultAsync((TenantPaymentGatewaySettings m) => m.TenantId == currentTenantById.Id);
			TenantPaymentGatewaySettings tenantPaymentGatewaySetting1 = tenantPaymentGatewaySetting;
			if (tenantPaymentGatewaySetting1 == null)
			{
				tenantHoursEditDto.PaymentGatewaySettings = new TenantPaymentGatewaySettingsEditDto()
				{
					TenantId = currentTenantById.Id
				};
			}
			else
			{
				tenantHoursEditDto.PaymentGatewaySettings = tenantPaymentGatewaySetting1.MapTo<TenantPaymentGatewaySettingsEditDto>();
			}
			IRepository<TenantDateTimeSettings, long> repository8 = this._tenantDateTimeSettingsRepository;
			TenantDateTimeSettings tenantDateTimeSetting = await repository8.FirstOrDefaultAsync((TenantDateTimeSettings m) => m.TenantId == currentTenantById.Id);
			TenantDateTimeSettings tenantDateTimeSetting1 = tenantDateTimeSetting;
			if (tenantDateTimeSetting1 == null)
			{
				tenantHoursEditDto.DateTimeSettings = new TenantDateTimeSettingsEditDto()
				{
					TenantId = currentTenantById.Id
				};
			}
			else
			{
				tenantHoursEditDto.DateTimeSettings = tenantDateTimeSetting1.MapTo<TenantDateTimeSettingsEditDto>();
			}
			tenantSettingsEditDto = tenantHoursEditDto;
			tenantSettingsEditDto.Logo = await this.GetTenantLogo(num1);
			tenantSettingsEditDto = null;
			if (!this._multiTenancyConfig.IsEnabled)
			{
				tenantSettingsEditDto = tenantHoursEditDto;
				GeneralSettingsEditDto generalSettingsEditDto = new GeneralSettingsEditDto();
				GeneralSettingsEditDto generalSettingsEditDto1 = generalSettingsEditDto;
				generalSettingsEditDto1.WebSiteRootAddress = await this.SettingManager.GetSettingValueAsync("App.General.WebSiteRootAddress");
				tenantSettingsEditDto.General = generalSettingsEditDto;
				tenantSettingsEditDto = null;
				generalSettingsEditDto1 = null;
				generalSettingsEditDto = null;
				tenantSettingsEditDto = tenantHoursEditDto;
				EmailSettingsEditDto emailSettingsEditDto = new EmailSettingsEditDto();
				EmailSettingsEditDto emailSettingsEditDto1 = emailSettingsEditDto;
				emailSettingsEditDto1.DefaultFromAddress = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.DefaultFromAddress");
				EmailSettingsEditDto emailSettingsEditDto2 = emailSettingsEditDto;
				emailSettingsEditDto2.DefaultFromDisplayName = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.DefaultFromDisplayName");
				EmailSettingsEditDto settingValueAsync3 = emailSettingsEditDto;
				settingValueAsync3.SmtpHost = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.Smtp.Host");
				EmailSettingsEditDto emailSettingsEditDto3 = emailSettingsEditDto;
				int num2 = await this.SettingManager.GetSettingValueAsync<int>("Abp.Net.Mail.Smtp.Port");
				emailSettingsEditDto3.SmtpPort = num2;
				EmailSettingsEditDto settingValueAsync4 = emailSettingsEditDto;
				settingValueAsync4.SmtpUserName = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.Smtp.UserName");
				EmailSettingsEditDto emailSettingsEditDto4 = emailSettingsEditDto;
				emailSettingsEditDto4.SmtpPassword = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.Smtp.Password");
				EmailSettingsEditDto settingValueAsync5 = emailSettingsEditDto;
				settingValueAsync5.SmtpDomain = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.Smtp.Domain");
				EmailSettingsEditDto emailSettingsEditDto5 = emailSettingsEditDto;
				bool flag2 = await this.SettingManager.GetSettingValueAsync<bool>("Abp.Net.Mail.Smtp.EnableSsl");
				emailSettingsEditDto5.SmtpEnableSsl = flag2;
				EmailSettingsEditDto emailSettingsEditDto6 = emailSettingsEditDto;
				bool flag3 = await this.SettingManager.GetSettingValueAsync<bool>("Abp.Net.Mail.Smtp.UseDefaultCredentials");
				emailSettingsEditDto6.SmtpUseDefaultCredentials = flag3;
				tenantSettingsEditDto.Email = emailSettingsEditDto;
				tenantSettingsEditDto = null;
				emailSettingsEditDto1 = null;
				emailSettingsEditDto2 = null;
				settingValueAsync3 = null;
				emailSettingsEditDto3 = null;
				settingValueAsync4 = null;
				emailSettingsEditDto4 = null;
				settingValueAsync5 = null;
				emailSettingsEditDto5 = null;
				emailSettingsEditDto6 = null;
				emailSettingsEditDto = null;
				if (!this._ldapModuleConfig.IsEnabled)
				{
					tenantHoursEditDto.Ldap = new LdapSettingsEditDto()
					{
						IsModuleEnabled = false
					};
				}
				else
				{
					tenantSettingsEditDto = tenantHoursEditDto;
					LdapSettingsEditDto ldapSettingsEditDto = new LdapSettingsEditDto()
					{
						IsModuleEnabled = true
					};
					LdapSettingsEditDto ldapSettingsEditDto1 = ldapSettingsEditDto;
					bool flag4 = await this.SettingManager.GetSettingValueAsync<bool>("Abp.Zero.Ldap.IsEnabled");
					ldapSettingsEditDto1.IsEnabled = flag4;
					LdapSettingsEditDto ldapSettingsEditDto2 = ldapSettingsEditDto;
					ldapSettingsEditDto2.Domain = await this.SettingManager.GetSettingValueAsync("Abp.Zero.Ldap.Domain");
					LdapSettingsEditDto ldapSettingsEditDto3 = ldapSettingsEditDto;
					ldapSettingsEditDto3.UserName = await this.SettingManager.GetSettingValueAsync("Abp.Zero.Ldap.UserName");
					LdapSettingsEditDto ldapSettingsEditDto4 = ldapSettingsEditDto;
					ldapSettingsEditDto4.Password = await this.SettingManager.GetSettingValueAsync("Abp.Zero.Ldap.Password");
					tenantSettingsEditDto.Ldap = ldapSettingsEditDto;
					tenantSettingsEditDto = null;
					ldapSettingsEditDto1 = null;
					ldapSettingsEditDto2 = null;
					ldapSettingsEditDto3 = null;
					ldapSettingsEditDto4 = null;
					ldapSettingsEditDto = null;
				}
			}
			return tenantHoursEditDto;
		}

		public async Task<TenantSettingsEditDto> GetAllSettingsByTenantId(int tenantId)
		{
			return await this.GetAllSettings(new int?(tenantId));
		}

		public async Task<decimal> GetBillingTypeRateByBillingType(string billingType)
		{
			int? impersonatorTenantId;
			int value;
			decimal num;
			decimal num1;
			decimal num2;
			decimal num3;
			billingType = billingType.Replace(" ", "");
			decimal num4 = new decimal();
			if (this.AbpSession.ImpersonatorTenantId.HasValue)
			{
				impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
				value = impersonatorTenantId.Value;
			}
			else
			{
				impersonatorTenantId = this.AbpSession.TenantId;
				value = impersonatorTenantId.Value;
			}
			int num5 = value;
			if (billingType.Trim().Length > 0)
			{
				IRepository<TenantPaymentSettings, long> repository = this._tenantPaymentSettingsRepository;
				List<TenantPaymentSettings> allListAsync = await repository.GetAllListAsync((TenantPaymentSettings x) => x.TenantId == num5);
				List<TenantPaymentSettings> tenantPaymentSettings = allListAsync;
				if (tenantPaymentSettings.Count == 1)
				{
					TenantPaymentSettings item = tenantPaymentSettings[0];
					string str = billingType;
					if (str == "HourlyProjectRate")
					{
						num = (item.HourlyProjectRate > decimal.Zero ? item.HourlyProjectRate : decimal.Zero);
						num4 = num;
					}
					else if (str == "HourlyStaffRate")
					{
						num1 = (item.HourlyStaffRate > decimal.Zero ? item.HourlyStaffRate : decimal.Zero);
						num4 = num1;
					}
					else if (str == "HourlyTaskRate")
					{
						num2 = (item.HourlyTaskRate > decimal.Zero ? item.HourlyTaskRate : decimal.Zero);
						num4 = num2;
					}
					else if (str == "FlatProjectAmount")
					{
						num3 = (item.FlatProjectAmount > decimal.Zero ? item.FlatProjectAmount : decimal.Zero);
						num4 = num3;
					}
				}
			}
			return num4;
		}

		public async Task<string> GetNextInvoiceNumberWithPrefix()
		{
			int? impersonatorTenantId;
			int value;
			if (this.AbpSession.ImpersonatorTenantId.HasValue)
			{
				impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
				value = impersonatorTenantId.Value;
			}
			else
			{
				impersonatorTenantId = this.AbpSession.TenantId;
				value = impersonatorTenantId.Value;
			}
			int num = value;
			IRepository<TenantPaymentSettings, long> repository = this._tenantPaymentSettingsRepository;
			List<TenantPaymentSettings> allListAsync = await repository.GetAllListAsync((TenantPaymentSettings x) => x.TenantId == num);
			TenantPaymentSettings item = allListAsync[0];
			long value1 = (long)-1;
			string empty = string.Empty;
			lock (this.invoiceNumberLock)
			{
				if (item.InvoiceNumber_Prefix != null && item.InvoiceNumber_StartingNumber.HasValue && item.InvoiceNumber_Prefix.Length > 0)
				{
					empty = string.Concat(empty, item.InvoiceNumber_Prefix);
				}
				if (item.InvoiceNumber_StartingNumber.HasValue)
				{
					value1 = item.InvoiceNumber_StartingNumber.Value + (long)1;
					empty = string.Concat(empty, value1.ToString());
				}
			}
			if (value1 > (long)-1)
			{
				item.InvoiceNumber_StartingNumber = new long?(value1);
				await this._tenantPaymentSettingsRepository.UpdateAsync(item);
			}
			return empty;
		}

		public async Task<string> GetTenantCoordinates(long tenantId)
		{
			IRepository<TenantDetail, long> repository = this._tenantDetailRepository;
			List<TenantDetail> allListAsync = await repository.GetAllListAsync((TenantDetail m) => (long)m.TenantId == tenantId);
			TenantDetail item = allListAsync[0];
			string empty = string.Empty;
			if (!(item != null) || !item.Latitude.HasValue || !item.Longitude.HasValue)
			{
				empty = "{lat:40.758701,lng:-111.876183}";
			}
			else
			{
				object[] value = new object[] { "{lat:", null, null, null, null };
				double? latitude = item.Latitude;
				value[1] = latitude.Value;
				value[2] = ",lng:";
				latitude = item.Longitude;
				value[3] = latitude.Value;
				value[4] = "}";
				empty = string.Concat(value);
			}
			return empty;
		}

		private async Task<TenantLogosEditDto> GetTenantLogo(int tenantId)
		{
			TenantLogosEditDto tenantLogosEditDto;
			IRepository<TenantLogos, long> repository = this._tenantLogosRepository;
			TenantLogos tenantLogo = await repository.FirstOrDefaultAsync((TenantLogos m) => m.TenantId == tenantId);
			TenantLogos tenantLogo1 = tenantLogo;
			if (tenantLogo1 == null)
			{
				tenantLogosEditDto = new TenantLogosEditDto()
				{
					TenantId = tenantId
				};
			}
			else
			{
				TenantLogosEditDto tenantLogosEditDto1 = new TenantLogosEditDto()
				{
					TenantId = tenantId,
					HeaderImageId = tenantLogo1.HeaderImageId,
					HeaderMobileImageId = tenantLogo1.HeaderMobileImageId,
					MailImageId = tenantLogo1.MailImageId,
					InvoiceImageId = tenantLogo1.InvoiceImageId
				};
				tenantLogosEditDto = tenantLogosEditDto1;
			}
			return tenantLogosEditDto;
		}

		public async Task<TenantLogosEditDto> GetTenantLogos(int tenantId)
		{
			return await this.GetTenantLogo(tenantId);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Tenant.Settings" })]
		public async Task UpdateAllSettings(TenantSettingsEditDto input)
		{
			string domain;
			string userName;
			string password;
			ISettingManager settingManager = this.SettingManager;
			int tenantId = this.AbpSession.GetTenantId();
			bool allowSelfRegistration = input.UserManagement.AllowSelfRegistration;
			await settingManager.ChangeSettingForTenantAsync(tenantId, "App.UserManagement.AllowSelfRegistration", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			ISettingManager settingManager1 = this.SettingManager;
			int num = this.AbpSession.GetTenantId();
			allowSelfRegistration = input.UserManagement.IsNewRegisteredUserActiveByDefault;
			await settingManager1.ChangeSettingForTenantAsync(num, "App.UserManagement.IsNewRegisteredUserActiveByDefault", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			ISettingManager settingManager2 = this.SettingManager;
			int tenantId1 = this.AbpSession.GetTenantId();
			allowSelfRegistration = input.UserManagement.IsEmailConfirmationRequiredForLogin;
			await settingManager2.ChangeSettingForTenantAsync(tenantId1, "Abp.Zero.UserManagement.IsEmailConfirmationRequiredForLogin", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			ISettingManager settingManager3 = this.SettingManager;
			int num1 = this.AbpSession.GetTenantId();
			allowSelfRegistration = input.UserManagement.UseCaptchaOnRegistration;
			await settingManager3.ChangeSettingForTenantAsync(num1, "App.UserManagement.UseCaptchaOnRegistration", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			ISettingManager settingManager4 = this.SettingManager;
			int tenantId2 = this.AbpSession.GetTenantId();
			allowSelfRegistration = input.UserManagement.RequireOnePhoneNumberForRegistration;
			await settingManager4.ChangeSettingForTenantAsync(tenantId2, "App.UserManagement.RequireOnePhoneNumberForRegistration", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			ISettingManager settingManager5 = this.SettingManager;
			int num2 = this.AbpSession.GetTenantId();
			allowSelfRegistration = input.UserManagement.SendEmailAfterRegistration;
			await settingManager5.ChangeSettingForTenantAsync(num2, "App.UserManagement.SendEmailAfterRegistration", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			await this.SettingManager.ChangeSettingForTenantAsync(this.AbpSession.GetTenantId(), "App.UserManagement.SendEmailAfterRegistrationMessage", input.UserManagement.SendEmailAfterRegistrationMessage.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			ISettingManager settingManager6 = this.SettingManager;
			int tenantId3 = this.AbpSession.GetTenantId();
			allowSelfRegistration = input.UserManagement.EnableEmergencyDeliveryFees;
			await settingManager6.ChangeSettingForTenantAsync(tenantId3, "App.UserManagment.EnableEmergencyDeliveryFees", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			if (!this._multiTenancyConfig.IsEnabled)
			{
				input.ValidateHostSettings();
				await this.SettingManager.ChangeSettingForApplicationAsync("App.General.WebSiteRootAddress", input.General.WebSiteRootAddress.EnsureEndsWith('/'));
				await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.DefaultFromAddress", input.Email.DefaultFromAddress);
				await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.DefaultFromDisplayName", input.Email.DefaultFromDisplayName);
				await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.Host", input.Email.SmtpHost);
				ISettingManager settingManager7 = this.SettingManager;
				int smtpPort = input.Email.SmtpPort;
				await settingManager7.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.Port", smtpPort.ToString(CultureInfo.InvariantCulture));
				await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.UserName", input.Email.SmtpUserName);
				await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.Password", input.Email.SmtpPassword);
				await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.Domain", input.Email.SmtpDomain);
				ISettingManager settingManager8 = this.SettingManager;
				allowSelfRegistration = input.Email.SmtpEnableSsl;
				await settingManager8.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.EnableSsl", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
				ISettingManager settingManager9 = this.SettingManager;
				allowSelfRegistration = input.Email.SmtpUseDefaultCredentials;
				await settingManager9.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.UseDefaultCredentials", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
				if (this._ldapModuleConfig.IsEnabled)
				{
					ISettingManager settingManager10 = this.SettingManager;
					int num3 = this.AbpSession.GetTenantId();
					allowSelfRegistration = input.Ldap.IsEnabled;
					await settingManager10.ChangeSettingForTenantAsync(num3, "Abp.Zero.Ldap.IsEnabled", allowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
					ISettingManager settingManager11 = this.SettingManager;
					int tenantId4 = this.AbpSession.GetTenantId();
					if (input.Ldap.Domain.IsNullOrWhiteSpace())
					{
						domain = null;
					}
					else
					{
						domain = input.Ldap.Domain;
					}
					await settingManager11.ChangeSettingForTenantAsync(tenantId4, "Abp.Zero.Ldap.Domain", domain);
					ISettingManager settingManager12 = this.SettingManager;
					int num4 = this.AbpSession.GetTenantId();
					if (input.Ldap.UserName.IsNullOrWhiteSpace())
					{
						userName = null;
					}
					else
					{
						userName = input.Ldap.UserName;
					}
					await settingManager12.ChangeSettingForTenantAsync(num4, "Abp.Zero.Ldap.UserName", userName);
					ISettingManager settingManager13 = this.SettingManager;
					int tenantId5 = this.AbpSession.GetTenantId();
					if (input.Ldap.Password.IsNullOrWhiteSpace())
					{
						password = null;
					}
					else
					{
						password = input.Ldap.Password;
					}
					await settingManager13.ChangeSettingForTenantAsync(tenantId5, "Abp.Zero.Ldap.Password", password);
				}
			}
			if (input.Details != null)
			{
				if (input.Details.TenantId == 0)
				{
					input.Details.TenantId = this.AbpSession.GetTenantId();
				}
				TenantDetail nullable = input.Details.MapTo<TenantDetail>();
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
				string[] address = new string[] { nullable.Address, ",", nullable.City, ",", empty, ",", nullable.PostalCode };
				string str = string.Concat(address);
				XDocument xDocument = XDocument.Load(WebRequest.Create(string.Format("http://maps.google.com/maps/api/geocode/xml?address={0}&sensor=false", str.Replace(",,", ",").Replace(" ", "+"))).GetResponse().GetResponseStream());
				if (!xDocument.ToString().Contains("<GeocodeResponse>") || xDocument.ToString().Contains("ZERO_RESULTS"))
				{
					longitude = null;
					nullable.Longitude = longitude;
					longitude = null;
					nullable.Latitude = longitude;
					nullable.Location = null;
				}
				else
				{
					XElement xElement = xDocument.Element("GeocodeResponse").Element("result").Element("geometry").Element("location");
					nullable.Latitude = new double?(double.Parse(xElement.Element("lat").Value));
					nullable.Longitude = new double?(double.Parse(xElement.Element("lng").Value));
				}
				xDocument = null;
			Label0:
				TenantDetail tenantDetail = nullable;
				longitude = nullable.Longitude;
				object value = longitude.Value;
				longitude = nullable.Latitude;
				tenantDetail.Location = DbGeography.PointFromText(string.Format("POINT({0} {1})", value, longitude.Value), 4326);
				await this._tenantDetailRepository.InsertOrUpdateAsync(nullable);
				nullable = null;
			}
			if (input.CustomerService != null)
			{
				if (input.CustomerService.TenantId == 0)
				{
					input.CustomerService.TenantId = this.AbpSession.GetTenantId();
				}
				await this._tenantCustomerServicesRepository.InsertOrUpdateAsync(input.CustomerService.MapTo<TenantCustomerService>());
			}
			if (input.Hours != null)
			{
				if (input.CustomerService.TenantId == 0)
				{
					input.CustomerService.TenantId = this.AbpSession.GetTenantId();
				}
				await this._tenantHoursRepository.InsertOrUpdateAsync(input.Hours.MapTo<TenantHour>());
			}
			if (input.Notifications != null)
			{
				input.ValidateEmailLists();
				if (input.Notifications.TenantId == 0)
				{
					input.Notifications.TenantId = this.AbpSession.GetTenantId();
				}
				await this._tenantNotificationsRepository.InsertOrUpdateAsync(input.Notifications.MapTo<TenantNotifications>());
			}
			if (input.PaymentSettings != null)
			{
				if (input.PaymentSettings.TenantId == 0)
				{
					input.PaymentSettings.TenantId = this.AbpSession.GetTenantId();
				}
				await this._tenantPaymentSettingsRepository.InsertOrUpdateAsync(input.PaymentSettings.MapTo<TenantPaymentSettings>());
			}
			if (input.PaymentGatewaySettings != null)
			{
				if (input.PaymentGatewaySettings.TenantId == 0)
				{
					input.PaymentGatewaySettings.TenantId = this.AbpSession.GetTenantId();
				}
				await this._tenantPaymentGatewaySettingsRepository.InsertOrUpdateAsync(input.PaymentGatewaySettings.MapTo<TenantPaymentGatewaySettings>());
			}
			if (input.DateTimeSettings != null)
			{
				if (input.DateTimeSettings.TenantId == 0)
				{
					input.DateTimeSettings.TenantId = this.AbpSession.GetTenantId();
				}
				await this._tenantDateTimeSettingsRepository.InsertOrUpdateAsync(input.DateTimeSettings.MapTo<TenantDateTimeSettings>());
			}
		}

		public async Task UpdateTenantLogos(TenantLogosEditDto input)
		{
			Guid? headerImageId;
			IRepository<TenantLogos, long> repository = this._tenantLogosRepository;
			TenantLogos tenantLogo = await repository.FirstOrDefaultAsync((TenantLogos m) => m.TenantId == input.TenantId);
			TenantLogos headerMobileImageId = tenantLogo;
			if (headerMobileImageId == null)
			{
				headerMobileImageId = new TenantLogos()
				{
					TenantId = input.TenantId
				};
				headerImageId = input.HeaderImageId;
				if (!headerImageId.HasValue)
				{
					headerImageId = null;
					headerMobileImageId.HeaderImageId = headerImageId;
				}
				else
				{
					headerMobileImageId.HeaderImageId = input.HeaderImageId;
				}
				headerImageId = input.HeaderMobileImageId;
				if (!headerImageId.HasValue)
				{
					headerImageId = null;
					headerMobileImageId.HeaderMobileImageId = headerImageId;
				}
				else
				{
					headerMobileImageId.HeaderMobileImageId = input.HeaderMobileImageId;
				}
				headerImageId = input.MailImageId;
				if (!headerImageId.HasValue)
				{
					headerImageId = null;
					headerMobileImageId.MailImageId = headerImageId;
				}
				else
				{
					headerMobileImageId.MailImageId = input.MailImageId;
				}
				headerImageId = input.InvoiceImageId;
				if (!headerImageId.HasValue)
				{
					headerImageId = null;
					headerMobileImageId.InvoiceImageId = headerImageId;
				}
				else
				{
					headerMobileImageId.InvoiceImageId = input.InvoiceImageId;
				}
			}
			else
			{
				headerMobileImageId.TenantId = input.TenantId;
				headerImageId = input.HeaderImageId;
				if (!headerImageId.HasValue)
				{
					headerImageId = null;
					headerMobileImageId.HeaderImageId = headerImageId;
				}
				else
				{
					headerMobileImageId.HeaderImageId = input.HeaderImageId;
				}
				headerImageId = input.HeaderMobileImageId;
				if (!headerImageId.HasValue)
				{
					headerImageId = null;
					headerMobileImageId.HeaderMobileImageId = headerImageId;
				}
				else
				{
					headerMobileImageId.HeaderMobileImageId = input.HeaderMobileImageId;
				}
				headerImageId = input.MailImageId;
				if (!headerImageId.HasValue)
				{
					headerImageId = null;
					headerMobileImageId.MailImageId = headerImageId;
				}
				else
				{
					headerMobileImageId.MailImageId = input.MailImageId;
				}
				headerImageId = input.InvoiceImageId;
				if (!headerImageId.HasValue)
				{
					headerImageId = null;
					headerMobileImageId.InvoiceImageId = headerImageId;
				}
				else
				{
					headerMobileImageId.InvoiceImageId = input.InvoiceImageId;
				}
			}
			await this._tenantLogosRepository.InsertOrUpdateAndGetIdAsync(headerMobileImageId);
		}
	}
}