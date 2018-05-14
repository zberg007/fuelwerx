using FuelWerx.WebApi.Controllers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using Owin;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace FuelWerx.Web
{
	public class Startup
	{
		public Startup()
		{
		}

		public void Configuration(IAppBuilder app)
		{
			app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);
			app.UseCookieAuthentication(new CookieAuthenticationOptions()
			{
				AuthenticationType = "ApplicationCookie",
				LoginPath = new PathString("/Account/Login")
			});
			app.UseExternalSignInCookie("ExternalCookie");
			if (Startup.IsTrue("ExternalAuth.Facebook.IsEnabled"))
			{
				app.UseFacebookAuthentication(Startup.CreateFacebookAuthOptions());
			}
			if (Startup.IsTrue("ExternalAuth.Twitter.IsEnabled"))
			{
				app.UseTwitterAuthentication(Startup.CreateTwitterAuthOptions());
			}
			if (Startup.IsTrue("ExternalAuth.Google.IsEnabled"))
			{
				app.UseGoogleAuthentication(Startup.CreateGoogleAuthOptions());
			}
		}

		private static FacebookAuthenticationOptions CreateFacebookAuthOptions()
		{
			FacebookAuthenticationOptions facebookAuthenticationOption = new FacebookAuthenticationOptions()
			{
				AppId = ConfigurationManager.AppSettings["ExternalAuth.Facebook.AppId"],
				AppSecret = ConfigurationManager.AppSettings["ExternalAuth.Facebook.AppSecret"]
			};
			facebookAuthenticationOption.Scope.Add("email");
			facebookAuthenticationOption.Scope.Add("public_profile");
			return facebookAuthenticationOption;
		}

		private static GoogleOAuth2AuthenticationOptions CreateGoogleAuthOptions()
		{
			return new GoogleOAuth2AuthenticationOptions()
			{
				ClientId = ConfigurationManager.AppSettings["ExternalAuth.Google.ClientId"],
				ClientSecret = ConfigurationManager.AppSettings["ExternalAuth.Google.ClientSecret"]
			};
		}

		private static TwitterAuthenticationOptions CreateTwitterAuthOptions()
		{
			return new TwitterAuthenticationOptions()
			{
				ConsumerKey = ConfigurationManager.AppSettings["ExternalAuth.Twitter.ConsumerKey"],
				ConsumerSecret = ConfigurationManager.AppSettings["ExternalAuth.Twitter.ConsumerSecret"]
			};
		}

		private static bool IsTrue(string appSettingName)
		{
			return string.Equals(ConfigurationManager.AppSettings[appSettingName], "true", StringComparison.InvariantCultureIgnoreCase);
		}
	}
}