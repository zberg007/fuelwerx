using Abp;
using System;

namespace FuelWerx
{
	public abstract class FuelWerxServiceBase : AbpServiceBase
	{
		protected FuelWerxServiceBase()
		{
			base.LocalizationSourceName = "FuelWerx";
		}
	}
}