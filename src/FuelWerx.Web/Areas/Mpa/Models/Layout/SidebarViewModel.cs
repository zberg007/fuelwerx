using Abp.Application.Navigation;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Layout
{
	public class SidebarViewModel
	{
		public string CurrentPageName
		{
			get;
			set;
		}

		public UserMenu Menu
		{
			get;
			set;
		}

		public SidebarViewModel()
		{
		}
	}
}