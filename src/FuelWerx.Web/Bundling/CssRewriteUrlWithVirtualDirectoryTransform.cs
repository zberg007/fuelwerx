using Abp.Extensions;
using System;
using System.Web;
using System.Web.Optimization;

namespace FuelWerx.Web.Bundling
{
	public class CssRewriteUrlWithVirtualDirectoryTransform : IItemTransform
	{
		private readonly CssRewriteUrlTransform _rewriteUrlTransform;

		public CssRewriteUrlWithVirtualDirectoryTransform()
		{
			this._rewriteUrlTransform = new CssRewriteUrlTransform();
		}

		public string Process(string includedVirtualPath, string input)
		{
			string str = this._rewriteUrlTransform.Process(includedVirtualPath, input);
			if (!HttpRuntime.AppDomainAppVirtualPath.IsNullOrEmpty() && HttpRuntime.AppDomainAppVirtualPath != "/")
			{
				str = str.Replace("url(/", string.Concat("url(", HttpRuntime.AppDomainAppVirtualPath, "/"));
			}
			return str;
		}
	}
}