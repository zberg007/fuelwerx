using System;
using System.Collections.Generic;
using System.Web.Optimization;

namespace FuelWerx.Web.Bundling
{
	public class AsIsBundleOrderer : IBundleOrderer
	{
		public AsIsBundleOrderer()
		{
		}

		public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
		{
			return files;
		}
	}
}