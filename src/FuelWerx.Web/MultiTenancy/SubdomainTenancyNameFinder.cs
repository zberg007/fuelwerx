using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Text;
using System;
using System.Web;

namespace FuelWerx.Web.MultiTenancy
{
	public class SubdomainTenancyNameFinder : ITenancyNameFinder, ITransientDependency
	{
		private readonly ISettingManager _settingManager;

		private readonly IMultiTenancyConfig _multiTenancyConfig;

		public SubdomainTenancyNameFinder(ISettingManager settingManager, IMultiTenancyConfig multiTenancyConfig)
		{
			this._settingManager = settingManager;
			this._multiTenancyConfig = multiTenancyConfig;
		}

		private static string GetCurrentSiteRootAddress()
		{
			Uri url = HttpContext.Current.Request.Url;
			string[] scheme = new string[] { url.Scheme, Uri.SchemeDelimiter, url.Host, null, null };
			scheme[3] = (url.IsDefaultPort ? "" : string.Concat(":", url.Port));
			scheme[4] = HttpContext.Current.Request.ApplicationPath;
			return string.Concat(scheme);
		}

		public string GetCurrentTenancyNameOrNull()
		{
			string[] strArrays;
			if (!this._multiTenancyConfig.IsEnabled)
			{
				return "Default";
			}
			string str = this._settingManager.GetSettingValue("App.General.WebSiteRootAddress").EnsureEndsWith('/');
			if (!str.Contains("{TENANCY_NAME}"))
			{
				return null;
			}
			if (HttpContext.Current == null || HttpContext.Current.Request.Url == null)
			{
				return null;
			}
			if (!FormattedStringValueExtracter.IsMatch(SubdomainTenancyNameFinder.GetCurrentSiteRootAddress().EnsureEndsWith('/'), str, out strArrays, true))
			{
				return null;
			}
			if (strArrays.Length == 0)
			{
				return null;
			}
			return strArrays[0];
		}
	}
}