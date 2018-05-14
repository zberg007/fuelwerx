using System;
using System.Web.Optimization;

namespace FuelWerx.Web.Bundling
{
	public static class FrontEndBundleConfig
	{
		private static void AddFrontendCssLibs(BundleCollection bundles, bool isRTL)
		{
			bundles.Add((new StyleBundle(string.Concat("~/Bundles/Frontend/libs/css", (isRTL ? "RTL" : "")))).Include("~/libs/simple-line-icons/simple-line-icons.min.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/Content/font-awesome.min.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/Content/flags/famfamfam-flags.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include((isRTL ? "~/libs/bootstrap/css/bootstrap-rtl.min.css" : "~/libs/bootstrap/css/bootstrap.min.css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/libs/sweetalert/sweet-alert.css", new IItemTransform[0]).Include("~/libs/toastr/toastr.min.css", new IItemTransform[0]).ForceOrdered());
		}

		private static void AddFrontendCssMetronic(BundleCollection bundles, bool isRTL)
		{
			bundles.Add((new StyleBundle(string.Concat("~/Bundles/Frontend/metronic/css", (isRTL ? "RTL" : "")))).Include(string.Concat("~/metronic/assets/global/css/components", (isRTL ? "-rtl" : ""), ".css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include(string.Concat("~/metronic/assets/frontend/layout/css/style", (isRTL ? "-rtl" : ""), ".css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/metronic/assets/frontend/pages/css/style-revolution-slider.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include(string.Concat("~/metronic/assets/frontend/layout/css/style-responsive", (isRTL ? "-rtl" : ""), ".css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include(string.Concat("~/metronic/assets/frontend/layout/css/themes/red", (isRTL ? "-rtl" : ""), ".css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).ForceOrdered());
		}

		public static void RegisterBundles(BundleCollection bundles)
		{
			FrontEndBundleConfig.AddFrontendCssLibs(bundles, false);
			FrontEndBundleConfig.AddFrontendCssLibs(bundles, true);
			bundles.Add((new ScriptBundle("~/Bundles/Frontend/libs/js")).Include(new string[] { "~/libs/json2/json2.min.js", "~/libs/jquery/jquery.min.js", "~/libs/jquery/jquery-migrate.min.js", "~/libs/bootstrap/js/bootstrap.min.js", "~/libs/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js", "~/libs/jquery-slimscroll/jquery.slimscroll.min.js", "~/libs/jquery-blockui/jquery.blockui.min.js", "~/libs/jquery-cookie/jquery.cookie.min.js", "~/libs/spinjs/spin.js", "~/libs/spinjs/jquery.spin.js", "~/libs/sweetalert/sweet-alert.min.js", "~/libs/toastr/toastr.min.js", "~/Scripts/moment-with-locales.min.js", "~/Abp/Framework/scripts/abp.js", "~/Abp/Framework/scripts/libs/abp.jquery.js", "~/Abp/Framework/scripts/libs/abp.toastr.js", "~/Abp/Framework/scripts/libs/abp.blockUI.js", "~/Abp/Framework/scripts/libs/abp.spin.js", "~/Abp/Framework/scripts/libs/abp.sweet-alert.js" }).ForceOrdered());
			FrontEndBundleConfig.AddFrontendCssMetronic(bundles, false);
			FrontEndBundleConfig.AddFrontendCssMetronic(bundles, true);
			bundles.Add((new ScriptBundle("~/Bundles/Frontend/metronic/js")).Include(new string[] { "~/metronic/assets/frontend/layout/scripts/back-to-top.js", "~/metronic/assets/frontend/layout/scripts/layout.js" }).ForceOrdered());
		}
	}
}