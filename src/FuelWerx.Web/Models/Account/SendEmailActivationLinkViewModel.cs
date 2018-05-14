using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class SendEmailActivationLinkViewModel
	{
		[Required]
		public string EmailAddress
		{
			get;
			set;
		}

		public string TenancyName
		{
			get;
			set;
		}

		public SendEmailActivationLinkViewModel()
		{
		}
	}
}