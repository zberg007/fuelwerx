using System;

namespace FuelWerx.Web
{
	public interface IWebUrlService
	{
		string GetSiteRootAddress(string tenancyName = null);
	}
}