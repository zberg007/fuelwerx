using Abp.Application.Services;
using Abp.Dependency;
using FuelWerx.Configuration.Tenants.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Configuration.Tenants
{
	public interface ITenantSettingsAppService : IApplicationService, ITransientDependency
	{
		Task<bool> CheckForInvoiceNumber();

		Task<TenantSettingsEditDto> GetAllSettings(int? input);

		Task<TenantSettingsEditDto> GetAllSettingsByTenantId(int input);

		Task<decimal> GetBillingTypeRateByBillingType(string billingType);

		Task<string> GetNextInvoiceNumberWithPrefix();

		Task<string> GetTenantCoordinates(long tenantId);

		Task<TenantLogosEditDto> GetTenantLogos(int tenantId);

		Task UpdateAllSettings(TenantSettingsEditDto input);

		Task UpdateTenantLogos(TenantLogosEditDto input);
	}
}