using Abp.Application.Services;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using FuelWerx;
using FuelWerx.Authorization.Users;
using FuelWerx.Authorization.Users.Profile.Dto;
using Microsoft.AspNet.Identity;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Authorization.Users.Profile
{
	[AbpAuthorize(new string[] {  })]
	public class ProfileAppService : FuelWerxAppServiceBase, IProfileAppService, IApplicationService, ITransientDependency
	{
		public ProfileAppService()
		{
		}

		[DisableAuditing]
		public async Task ChangePassword(ChangePasswordInput input)
		{
			User currentUserAsync = await this.GetCurrentUserAsync();
			IdentityResult identityResult = await this.UserManager.ChangePasswordAsync(currentUserAsync.Id, input.CurrentPassword, input.NewPassword);
			this.CheckErrors(identityResult);
		}

		public async Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit()
		{
			return (await this.GetCurrentUserAsync()).MapTo<CurrentUserProfileEditDto>();
		}

		public async Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input)
		{
			User currentUserAsync = await this.GetCurrentUserAsync();
			input.MapTo<CurrentUserProfileEditDto, User>(currentUserAsync);
			this.CheckErrors(await this.UserManager.UpdateAsync(currentUserAsync));
		}
	}
}