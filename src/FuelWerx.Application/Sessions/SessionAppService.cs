using Abp.Application.Services;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using FuelWerx.Sessions.Dto;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Sessions
{
	[AbpAuthorize(new string[] {  })]
	public class SessionAppService : FuelWerxAppServiceBase, ISessionAppService, IApplicationService, ITransientDependency
	{
		public SessionAppService()
		{
		}

		[DisableAuditing]
		public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
		{
			GetCurrentLoginInformationsOutput getCurrentLoginInformationsOutput = new GetCurrentLoginInformationsOutput();
			GetCurrentLoginInformationsOutput getCurrentLoginInformationsOutput1 = getCurrentLoginInformationsOutput;
			User currentUserAsync = await this.GetCurrentUserAsync();
			getCurrentLoginInformationsOutput1.User = currentUserAsync.MapTo<UserLoginInfoDto>();
			GetCurrentLoginInformationsOutput getCurrentLoginInformationsOutput2 = getCurrentLoginInformationsOutput;
			getCurrentLoginInformationsOutput1 = null;
			getCurrentLoginInformationsOutput = null;
			if (this.AbpSession.TenantId.HasValue)
			{
				getCurrentLoginInformationsOutput = getCurrentLoginInformationsOutput2;
				Tenant currentTenantAsync = await this.GetCurrentTenantAsync();
				getCurrentLoginInformationsOutput.Tenant = currentTenantAsync.MapTo<TenantLoginInfoDto>();
				getCurrentLoginInformationsOutput = null;
			}
			return getCurrentLoginInformationsOutput2;
		}
	}
}