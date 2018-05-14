using System;

namespace FuelWerx.Web.MultiTenancy
{
	public interface ITenancyNameFinder
	{
		string GetCurrentTenancyNameOrNull();
	}
}