using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class LoginFormViewModel
	{
		public bool IsSelfRegistrationEnabled
		{
			get;
			set;
		}

		public string SuccessMessage
		{
			get;
			set;
		}

		public string TenancyName
		{
			get;
			set;
		}

		public string UserNameOrEmailAddress
		{
			get;
			set;
		}

		public LoginFormViewModel()
		{
		}
	}
}