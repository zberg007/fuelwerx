using Abp.Localization;
using FuelWerx.Generic.Dto;
using FuelWerx.Sessions.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Layout
{
	public class HeaderViewModel
	{
		public LanguageInfo CurrentLanguage
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

		public bool IsImpersonatedLogin
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

		public List<QuickMenuItemListDto> QuickMenuItems
		{
			get;
			set;
		}

		public HeaderViewModel()
		{
		}

		public string GetShownLoginName()
		{
			string str = string.Concat("<span id=\"HeaderCurrentUserName\">", this.LoginInformations.User.UserName, "</span>");
			if (!this.IsMultiTenancyEnabled)
			{
				return str;
			}
			if (this.LoginInformations.Tenant == null)
			{
				return string.Concat(".\\", str);
			}
			return string.Concat(this.LoginInformations.Tenant.TenancyName, "\\", str);
		}
	}
}