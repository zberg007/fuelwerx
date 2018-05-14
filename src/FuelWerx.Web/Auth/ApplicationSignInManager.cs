using Abp.Dependency;
using FuelWerx.Authorization.Users;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;

namespace FuelWerx.Web.Auth
{
	public class ApplicationSignInManager : SignInManager<User, long>, ITransientDependency
	{
		public ApplicationSignInManager(FuelWerx.Authorization.Users.UserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
		{
		}
	}
}