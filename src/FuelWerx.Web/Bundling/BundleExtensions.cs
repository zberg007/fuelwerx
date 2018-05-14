using System;
using System.Runtime.CompilerServices;
using System.Web.Optimization;

namespace FuelWerx.Web.Bundling
{
	public static class BundleExtensions
	{
		public static Bundle ForceOrdered(this Bundle bundle)
		{
			bundle.Orderer = new AsIsBundleOrderer();
			return bundle;
		}
	}
}