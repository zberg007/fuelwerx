using Abp.Runtime.Validation;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	public class LdapSettingsEditDto : IValidate
	{
		public string Domain
		{
			get;
			set;
		}

		public bool IsEnabled
		{
			get;
			set;
		}

		public bool IsModuleEnabled
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public LdapSettingsEditDto()
		{
		}
	}
}