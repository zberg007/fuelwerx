using Abp.Application.Navigation;
using Abp.Localization;
using System;
using System.Collections.Generic;

namespace FuelWerx.Web.Navigation
{
	public class FrontEndNavigationProvider : NavigationProvider
	{
		public const string MenuName = "Frontend";

		public FrontEndNavigationProvider()
		{
		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, "FuelWerx");
		}

		public override void SetNavigation(INavigationProviderContext context)
		{
			MenuDefinition menuDefinition = new MenuDefinition("Frontend", new FixedLocalizableString("Frontend menu"), null);
			context.Manager.Menus["Frontend"] = menuDefinition;
			menuDefinition.AddItem(new MenuItemDefinition("Frontend.Home", FrontEndNavigationProvider.L("HomePage"), null, "", false, null, 0, null, null)).AddItem(new MenuItemDefinition("Frontend.About", FrontEndNavigationProvider.L("AboutUs"), null, "About", false, null, 0, null, null));
		}
	}
}