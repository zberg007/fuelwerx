using Abp;
using Abp.Domain.Services;
using System;

namespace FuelWerx
{
	public abstract class FuelWerxDomainServiceBase : DomainService
	{
		protected FuelWerxDomainServiceBase()
		{
			base.LocalizationSourceName = "FuelWerx";
		}
	}
}