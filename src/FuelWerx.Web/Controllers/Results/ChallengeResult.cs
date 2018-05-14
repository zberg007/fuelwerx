using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers.Results
{
	public class ChallengeResult : HttpUnauthorizedResult
	{
		public string LoginProvider
		{
			get;
			set;
		}

		public string RedirectUri
		{
			get;
			set;
		}

		public string UserId
		{
			get;
			set;
		}

		public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
		{
		}

		public ChallengeResult(string provider, string redirectUri, string userId)
		{
			this.LoginProvider = provider;
			this.RedirectUri = redirectUri;
			this.UserId = userId;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			AuthenticationProperties authenticationProperty = new AuthenticationProperties()
			{
				RedirectUri = this.RedirectUri
			};
			if (this.UserId != null)
			{
				authenticationProperty.Dictionary["XsrfId"] = this.UserId;
			}
			context.HttpContext.GetOwinContext().Authentication.Challenge(authenticationProperty, new string[] { this.LoginProvider });
		}
	}
}