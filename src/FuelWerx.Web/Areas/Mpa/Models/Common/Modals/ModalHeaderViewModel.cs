using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Common.Modals
{
	public class ModalHeaderViewModel
	{
		public string Title
		{
			get;
			set;
		}

		public ModalHeaderViewModel(string title)
		{
			this.Title = title;
		}
	}
}