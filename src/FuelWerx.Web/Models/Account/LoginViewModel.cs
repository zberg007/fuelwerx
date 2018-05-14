using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class LoginViewModel
	{
		[Required]
		public string Password
		{
			get;
			set;
		}

		public bool RememberMe
		{
			get;
			set;
		}

		public string TenancyName
		{
			get;
			set;
		}

		[Required]
		public string UsernameOrEmailAddress
		{
			get;
			set;
		}

		public LoginViewModel()
		{
		}
	}
}