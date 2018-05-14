using Abp.Application.Navigation;
using Abp.Localization;
using FuelWerx.Sessions.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Layout
{
	public class HeaderViewModel
	{
		public LanguageInfo CurrentLanguage
		{
			get;
			set;
		}

		public string CurrentPageName
		{
			get;
			set;
		}

		public Guid? CurrentTenantHeaderLogoId
		{
			get;
			set;
		}

		public Guid? CurrentTenantHeaderMobileLogoId
		{
			get;
			set;
		}

		public bool IsMultiTenancyEnabled
		{
			get;
			set;
		}

		public IReadOnlyList<LanguageInfo> Languages
		{
			get;
			set;
		}

		public GetCurrentLoginInformationsOutput LoginInformations
		{
			get;
			set;
		}

		public UserMenu Menu
		{
			get;
			set;
		}

		public HeaderViewModel()
		{
		}

		public string GetShownLoginName()
		{
			if (!this.IsMultiTenancyEnabled)
			{
				return this.LoginInformations.User.UserName;
			}
			if (this.LoginInformations.Tenant == null)
			{
				return string.Concat(".\\", this.LoginInformations.User.UserName);
			}
			return string.Concat(this.LoginInformations.Tenant.TenancyName, "\\", this.LoginInformations.User.UserName);
		}
	}
}