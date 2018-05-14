using FuelWerx.Sessions.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Layout
{
	public class FooterViewModel
	{
		public GetCurrentLoginInformationsOutput LoginInformations
		{
			get;
			set;
		}

		public FooterViewModel()
		{
		}

		public string GetProductNameWithEdition()
		{
			string str = "FuelWerx";
			if (this.LoginInformations.Tenant != null && this.LoginInformations.Tenant.EditionDisplayName != null)
			{
				str = string.Concat(str, " ", this.LoginInformations.Tenant.EditionDisplayName);
			}
			return str;
		}
	}
}