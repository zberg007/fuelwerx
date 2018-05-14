using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.WebApi.Models
{
	public class LoginModel
	{
		[Required]
		public string Password
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

		public LoginModel()
		{
		}
	}
}