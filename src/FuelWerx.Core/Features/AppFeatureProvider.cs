using Abp.Application.Features;
using Abp.Localization;
using Abp.UI.Inputs;
using System;

namespace FuelWerx.Features
{
	public class AppFeatureProvider : FeatureProvider
	{
		public AppFeatureProvider()
		{
		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, "FuelWerx");
		}

		public override void SetFeatures(IFeatureDefinitionContext context)
		{
			context.Create("MyApplication.PaymentGatewayBooleanFeature", "Payeezy", AppFeatureProvider.L("AvailablePaymentGateways"), null, FeatureScopes.All, new ComboboxInputType(new StaticLocalizableComboboxItemSource(new ILocalizableComboboxItem[] { new LocalizableComboboxItem("Payeezy", AppFeatureProvider.L("Payeezy")) })));
		}
	}
}