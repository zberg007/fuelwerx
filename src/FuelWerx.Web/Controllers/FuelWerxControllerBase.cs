using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Specialized;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public abstract class FuelWerxControllerBase : AbpController
	{
		internal string apiKey_degreeDaysAccountKey = WebConfigurationManager.AppSettings["FuelCast.DegreeDays.Api.AccountKey"].ToString();

		internal string apiKey_degreeDaysSecurityKey = WebConfigurationManager.AppSettings["FuelCast.DegreeDays.Api.SecurityKey"].ToString();

		protected FuelWerxControllerBase()
		{
			base.LocalizationSourceName = "FuelWerx";
		}

		protected void CheckErrors(IdentityResult identityResult)
		{
			identityResult.CheckErrors(base.LocalizationManager);
		}

		protected virtual void CheckModelState()
		{
			if (!base.ModelState.IsValid)
			{
				throw new UserFriendlyException(this.L("FormIsNotValidMessage"));
			}
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
		}
	}
}