using Abp.Configuration;
using Abp.Dependency;
using Abp.Extensions;
using System;

namespace FuelWerx.Web
{
	public class WebUrlService : IWebUrlService, ITransientDependency
	{
		public const string TenancyNamePlaceHolder = "{TENANCY_NAME}";

		private readonly ISettingManager _settingManager;

		public WebUrlService(ISettingManager settingManager)
		{
			this._settingManager = settingManager;
		}

		public string GetSiteRootAddress(string tenancyName = null)
		{
			string str = this._settingManager.GetSettingValue("App.General.WebSiteRootAddress").EnsureEndsWith('/');
			if (!str.Contains("{TENANCY_NAME}"))
			{
				return str;
			}
			if (str.Contains("{TENANCY_NAME}."))
			{
				str = str.Replace("{TENANCY_NAME}.", "{TENANCY_NAME}");
			}
			if (tenancyName.IsNullOrEmpty())
			{
				return str.Replace("{TENANCY_NAME}", "");
			}
			return str.Replace("{TENANCY_NAME}", string.Concat(tenancyName, "."));
		}
	}
}