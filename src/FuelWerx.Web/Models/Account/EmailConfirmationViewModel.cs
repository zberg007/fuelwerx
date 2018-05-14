using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class EmailConfirmationViewModel
	{
		[Required]
		public string ConfirmationCode
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

		public EmailConfirmationViewModel()
		{
		}
	}
}