using FuelWerx.Web.Bundling;
using System;
using System.Web.Optimization;

namespace FuelWerx.Web.Areas.Mpa.Startup
{
	public static class MpaBundleConfig
	{
		private static void AddAppMetrinicCss(BundleCollection bundles, bool isRTL)
		{
			bundles.Add((new StyleBundle(string.Concat("~/Bundles/Mpa/metronic/css", (isRTL ? "RTL" : "")))).Include(string.Concat("~/metronic/assets/global/css/components-md", (isRTL ? "-rtl" : ""), ".css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include(string.Concat("~/metronic/assets/global/css/plugins-md", (isRTL ? "-rtl" : ""), ".css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include(string.Concat("~/metronic/assets/admin/layout4/css/layout", (isRTL ? "-rtl" : ""), ".css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include(string.Concat("~/metronic/assets/admin/layout4/css/themes/light", (isRTL ? "-rtl" : ""), ".css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).ForceOrdered());
		}

		private static void AddMpaCssLibs(BundleCollection bundles, bool isRTL)
		{
			bundles.Add((new StyleBundle(string.Concat("~/Bundles/Mpa/libs/css", (isRTL ? "RTL" : "")))).Include("~/libs/jquery-ui/jquery-ui.min.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/libs/jquery-jtable/themes/lightcolor/gray/jtable.min.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/Content/font-awesome.min.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/libs/simple-line-icons/simple-line-icons.min.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/Content/flags/famfamfam-flags.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include((isRTL ? "~/libs/bootstrap/css/bootstrap-rtl.min.css" : "~/libs/bootstrap/css/bootstrap.min.css"), new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/libs/jquery-uniform/css/uniform.default.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/libs/jstree/themes/default/style.css", new IItemTransform[] { new CssRewriteUrlWithVirtualDirectoryTransform() }).Include("~/libs/morris/morris.css", new IItemTransform[0]).Include("~/libs/sweetalert/sweet-alert.css", new IItemTransform[0]).Include("~/libs/toastr/toastr.min.css", new IItemTransform[0]).Include("~/libs/bootstrap-daterangepicker/daterangepicker.css", new IItemTransform[0]).Include("~/libs/bootstrap-switch/css/bootstrap-switch.min.css", new IItemTransform[0]).Include("~/libs/bootstrap-select/bootstrap-select.min.css", new IItemTransform[0]).ForceOrdered());
		}

		public static void RegisterBundles(BundleCollection bundles)
		{
			MpaBundleConfig.AddMpaCssLibs(bundles, false);
			MpaBundleConfig.AddMpaCssLibs(bundles, true);
			bundles.Add((new ScriptBundle("~/Bundles/Mpa/libs/js")).Include(new string[] { "~/libs/json2/json2.min.js", "~/libs/jquery/jquery.min.js", "~/libs/jquery/jquery-migrate.min.js", "~/libs/jquery-ui/jquery-ui.min.js", "~/libs/jquery-validation/js/jquery.validate.min.js", "~/libs/bootstrap/js/bootstrap.min.js", "~/libs/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js", "~/libs/jquery-slimscroll/jquery.slimscroll.min.js", "~/libs/jquery-blockui/jquery.blockui.min.js", "~/libs/jquery-cookie/jquery.cookie.min.js", "~/libs/jquery-uniform/jquery.uniform.min.js", "~/libs/jquery-ajax-form/jquery.form.js", "~/libs/jquery-jtable/jquery.jtable.min.js", "~/libs/morris/morris.min.js", "~/libs/morris/raphael-min.js", "~/libs/jquery-sparkline/jquery.sparkline.min.js", "~/libs/jstree/jstree.js", "~/libs/bootstrap-switch/js/bootstrap-switch.min.js", "~/libs/spinjs/spin.js", "~/libs/spinjs/jquery.spin.js", "~/libs/sweetalert/sweet-alert.min.js", "~/libs/toastr/toastr.min.js", "~/Scripts/moment-with-locales.min.js", "~/libs/bootstrap-daterangepicker/daterangepicker.js", "~/libs/bootstrap-select/bootstrap-select.min.js", "~/Scripts/underscore.min.js", "~/Abp/Framework/scripts/abp.js", "~/Abp/Framework/scripts/libs/abp.jquery.js", "~/Abp/Framework/scripts/libs/abp.toastr.js", "~/Abp/Framework/scripts/libs/abp.blockUI.js", "~/Abp/Framework/scripts/libs/abp.spin.js", "~/Abp/Framework/scripts/libs/abp.sweet-alert.js", "~/Abp/Framework/scripts/libs/abp.jtable.js", "~/lib/jquery-inputmask/inputmask.js", "~/lib/jquery-inputmask/jquery.inputmask.js", "~/lib/jquery-inputmask/inputmask.extensions.js", "~/lib/jquery-inputmask/inputmask.phone.extensions.js", "~/lib/jquery-inputmask/inputmask.numeric.extensions.js", "~/lib/jquery-inputmask/inputmask.date.extensions.js", "~/Scripts/jquery-jtable-footer.min.js" }).ForceOrdered());
			bundles.Add((new ScriptBundle("~/Bundles/Mpa/Common/js")).IncludeDirectory("~/Areas/Mpa/Common/Scripts", "*.js", true).Include("~/Areas/Mpa/Views/Common/Modals/_LookupModal.js", new IItemTransform[0]).ForceOrdered());
			MpaBundleConfig.AddAppMetrinicCss(bundles, false);
			MpaBundleConfig.AddAppMetrinicCss(bundles, true);
			bundles.Add((new ScriptBundle("~/Bundles/Mpa/metronic/js")).Include(new string[] { "~/metronic/assets/global/scripts/app.js", "~/metronic/assets/admin/layout4/scripts/layout.js" }).ForceOrdered());
		}
	}
}