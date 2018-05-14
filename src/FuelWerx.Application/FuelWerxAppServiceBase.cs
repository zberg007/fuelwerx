using Abp;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using Microsoft.AspNet.Identity;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx
{
	public abstract class FuelWerxAppServiceBase : ApplicationService
	{
		public FuelWerx.MultiTenancy.TenantManager TenantManager
		{
			get;
			set;
		}

		public FuelWerx.Authorization.Users.UserManager UserManager
		{
			get;
			set;
		}

		protected FuelWerxAppServiceBase()
		{
			base.LocalizationSourceName = "FuelWerx";
		}

		protected virtual void CheckErrors(IdentityResult identityResult)
		{
			identityResult.CheckErrors(base.LocalizationManager);
		}

		protected virtual Tenant GetCurrentTenant()
		{
			return this.TenantManager.GetById<Tenant, Role, User>(base.AbpSession.GetTenantId());
		}

		protected virtual Task<Tenant> GetCurrentTenantAsync()
		{
			return this.TenantManager.GetByIdAsync(base.AbpSession.GetTenantId());
		}

		protected virtual Tenant GetCurrentTenantById(int tenantId)
		{
			return this.TenantManager.GetById<Tenant, Role, User>(tenantId);
		}

		protected virtual User GetCurrentUser()
		{
			User user = UserManager.FindById<User, long>(base.AbpSession.GetUserId());
			if (user == null)
			{
				throw new ApplicationException("There is no current user!");
			}
			return user;
		}

		protected virtual Task<User> GetCurrentUserAsync()
		{
			Task<User> task = UserManager.FindByIdAsync(base.AbpSession.GetUserId());
			if (task == null)
			{
				throw new ApplicationException("There is no current user!");
			}
			return task;
		}
	}
}