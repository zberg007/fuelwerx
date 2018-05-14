using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using System;

namespace FuelWerx.Authorization.Ldap
{
	public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
	{
		public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig) : base(settings, ldapModuleConfig)
		{
		}
	}
}