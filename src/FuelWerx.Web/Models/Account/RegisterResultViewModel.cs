using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class RegisterResultViewModel
	{
		public string EmailAddress
		{
			get;
			set;
		}

		public bool IsActive
		{
			get;
			set;
		}

		public bool IsEmailConfirmationRequired
		{
			get;
			set;
		}

		public string NameAndSurname
		{
			get;
			set;
		}

		public string TenancyName
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public RegisterResultViewModel()
		{
		}
	}
}