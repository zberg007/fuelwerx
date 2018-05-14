using Abp.WebApi.Controllers;
using System;

namespace FuelWerx.WebApi
{
	public abstract class FuelWerxApiControllerBase : AbpApiController
	{
		protected FuelWerxApiControllerBase()
		{
			base.LocalizationSourceName = "FuelWerx";
		}
	}
}