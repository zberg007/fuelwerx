using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class ResetPasswordFormViewModel
	{
		[Required]
		public string Password
		{
			get;
			set;
		}

		[Required]
		public string ResetCode
		{
			get;
			set;
		}

		[Required]
		public string UserId
		{
			get;
			set;
		}

		public ResetPasswordFormViewModel()
		{
		}
	}
}