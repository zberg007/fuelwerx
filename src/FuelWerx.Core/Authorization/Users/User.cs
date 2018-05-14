using Abp.Authorization.Users;
using Abp.Extensions;
using FuelWerx.MultiTenancy;
using Microsoft.AspNet.Identity;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users
{
	public class User : AbpUser<FuelWerx.MultiTenancy.Tenant, User>
	{
		public const int MinPlainPasswordLength = 6;

		public virtual Guid? ProfilePictureId
		{
			get;
			set;
		}

		public virtual bool ShouldChangePasswordOnNextLogin
		{
			get;
			set;
		}

		public User()
		{
		}

		public static string CreateRandomPassword()
		{
			Guid guid = Guid.NewGuid();
			return guid.ToString("N").Truncate(16);
		}

		public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
		{
			return new User()
			{
				TenantId = new int?(tenantId),
				UserName = "admin",
				Name = "admin",
				Surname = "admin",
				EmailAddress = emailAddress,
				Password = (new PasswordHasher()).HashPassword(password)
			};
		}
	}
}