using Abp.Web.Mvc.Views;
using System;

namespace FuelWerx.Web.Views
{
	public abstract class FuelWerxWebViewPageBase<TModel> : AbpWebViewPage<TModel>
	{
		protected FuelWerxWebViewPageBase()
		{
			base.LocalizationSourceName = "FuelWerx";
		}
	}
}