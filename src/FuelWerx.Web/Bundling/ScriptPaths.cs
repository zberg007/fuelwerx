using Abp.Extensions;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;

namespace FuelWerx.Web.Bundling
{
	public static class ScriptPaths
	{
		public const string Json2 = "~/libs/json2/json2.min.js";

		public const string JQuery = "~/libs/jquery/jquery.min.js";

		public const string JQuery_Migrate = "~/libs/jquery/jquery-migrate.min.js";

		public const string JQuery_UI = "~/libs/jquery-ui/jquery-ui.min.js";

		public const string JQuery_Slimscroll = "~/libs/jquery-slimscroll/jquery.slimscroll.min.js";

		public const string JQuery_BlockUi = "~/libs/jquery-blockui/jquery.blockui.min.js";

		public const string JQuery_Cookie = "~/libs/jquery-cookie/jquery.cookie.min.js";

		public const string JQuery_Uniform = "~/libs/jquery-uniform/jquery.uniform.min.js";

		public const string JQuery_Ajax_Form = "~/libs/jquery-ajax-form/jquery.form.js";

		public const string JQuery_Sparkline = "~/libs/jquery-sparkline/jquery.sparkline.min.js";

		public const string JQuery_Validation = "~/libs/jquery-validation/js/jquery.validate.min.js";

		public const string JQuery_jTable = "~/libs/jquery-jtable/jquery.jtable.min.js";

		public const string JQuery_Multi_Select = "~/libs/jquery-multiselect/jquery.multi-select.js";

		public const string JQuery_InputMask = "~/lib/jquery-inputmask/inputmask.js";

		public const string JQuery_InputMask_jQuery = "~/lib/jquery-inputmask/jquery.inputmask.js";

		public const string JQuery_InputMask_Extensions = "~/lib/jquery-inputmask/inputmask.extensions.js";

		public const string JQuery_InputMask_Extensions_Date = "~/lib/jquery-inputmask/inputmask.date.extensions.js";

		public const string JQuery_InputMask_Extensions_Numeric = "~/lib/jquery-inputmask/inputmask.numeric.extensions.js";

		public const string JQuery_InputMask_Extensions_Phone = "~/lib/jquery-inputmask/inputmask.phone.extensions.js";

		public const string Bootstrap = "~/libs/bootstrap/js/bootstrap.min.js";

		public const string Bootstrap_Hover_Dropdown = "~/libs/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js";

		public const string Bootstrap_DateRangePicker = "~/libs/bootstrap-daterangepicker/daterangepicker.js";

		public const string Bootstrap_Select = "~/libs/bootstrap-select/bootstrap-select.min.js";

		public const string Bootstrap_Switch = "~/libs/bootstrap-switch/js/bootstrap-switch.min.js";

		public const string Morris = "~/libs/morris/morris.min.js";

		public const string Morris_Raphael = "~/libs/morris/raphael-min.js";

		public const string JsTree = "~/libs/jstree/jstree.js";

		public const string SpinJs = "~/libs/spinjs/spin.js";

		public const string SpinJs_JQuery = "~/libs/spinjs/jquery.spin.js";

		public const string SweetAlert = "~/libs/sweetalert/sweet-alert.min.js";

		public const string Toastr = "~/libs/toastr/toastr.min.js";

		public const string MomentJs = "~/Scripts/moment-with-locales.min.js";

		public const string Underscore = "~/Scripts/underscore.min.js";

		public const string Angular = "~/Scripts/angular.min.js";

		public const string Angular_Sanitize = "~/Scripts/angular-sanitize.min.js";

		public const string Angular_Touch = "~/Scripts/angular-touch.min.js";

		public const string Angular_Ui_Router = "~/Scripts/angular-ui-router.min.js";

		public const string Angular_Ui_Utils = "~/Scripts/angular-ui/ui-utils.min.js";

		public const string Angular_Ui_Bootstrap_Tpls = "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js";

		public const string Angular_Ui_Grid = "~/libs/angular-ui-grid/ui-grid.min.js";

		public const string Angular_OcLazyLoad = "~/libs/angular-ocLazyLoad/ocLazyLoad.min.js";

		public const string Angular_File_Upload = "~/libs/angular-file-upload/angular-file-upload.min.js";

		public const string Angular_DateRangePicker = "~/libs/angular-daterangepicker/angular-daterangepicker.min.js";

		public const string Abp = "~/Abp/Framework/scripts/abp.js";

		public const string Abp_JQuery = "~/Abp/Framework/scripts/libs/abp.jquery.js";

		public const string Abp_Toastr = "~/Abp/Framework/scripts/libs/abp.toastr.js";

		public const string Abp_BlockUi = "~/Abp/Framework/scripts/libs/abp.blockUI.js";

		public const string Abp_SpinJs = "~/Abp/Framework/scripts/libs/abp.spin.js";

		public const string Abp_SweetAlert = "~/Abp/Framework/scripts/libs/abp.sweet-alert.js";

		public const string Abp_Angular = "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js";

		public const string Abp_jTable = "~/Abp/Framework/scripts/libs/abp.jtable.js";

		public const string JQuery_JTable_Ext_Footer = "~/Scripts/jquery-jtable-footer.min.js";

		public static string Angular_Localization
		{
			get
			{
				return ScriptPaths.GetLocalizationFileForjAngularOrNull(Thread.CurrentThread.CurrentUICulture.Name.ToLower()) ?? (ScriptPaths.GetLocalizationFileForjAngularOrNull(Thread.CurrentThread.CurrentUICulture.Name.Left(2).ToLower()) ?? "~/Scripts/i18n/angular-locale_en-us.js");
			}
		}

		public static string Bootstrap_Select_Localization
		{
			get
			{
				return ScriptPaths.GetLocalizationFileForBootstrapSelect(Thread.CurrentThread.CurrentUICulture.Name.ToLower()) ?? (ScriptPaths.GetLocalizationFileForBootstrapSelect(Thread.CurrentThread.CurrentUICulture.Name.Left(2).ToLower()) ?? "~/libs/bootstrap-select/i18n/defaults-en_US.js");
			}
		}

		public static string JQuery_JTable_Localization
		{
			get
			{
				return ScriptPaths.GetLocalizationFileForJTableOrNull(Thread.CurrentThread.CurrentUICulture.Name.ToLower()) ?? (ScriptPaths.GetLocalizationFileForJTableOrNull(Thread.CurrentThread.CurrentUICulture.Name.Left(2).ToLower()) ?? "~/libs/jquery-jtable/localization/_jquery.jtable.empty.js");
			}
		}

		public static string JQuery_Validation_Localization
		{
			get
			{
				return ScriptPaths.GetLocalizationFileForjQueryValidationOrNull(Thread.CurrentThread.CurrentUICulture.Name.ToLower().Replace("-", "_")) ?? (ScriptPaths.GetLocalizationFileForjQueryValidationOrNull(Thread.CurrentThread.CurrentUICulture.Name.Left(2).ToLower()) ?? "~/libs/jquery-validation/js/localization/_messages_empty.js");
			}
		}

		private static string GetLocalizationFileForBootstrapSelect(string cultureCode)
		{
			string[] strArrays = new string[] { "ar_AR", "bg_BG", "cs_CZ", "da_DK", "de_DE", "en_US", "es_CL", "eu", "fa_IR", "fi_FI", "fr_FR", "hu_HU", "id_ID", "it_IT", "ko_KR", "nb_NO", "nl_NL", "pl_PL", "pt_BR", "pt_PT", "ro_RO", "ru_RU", "sk_SK", "sl_SL", "sv_SE", "tr_TR", "ua_UA", "zh_CN", "zh_TW" };
			try
			{
				cultureCode = cultureCode.Replace("-", "_");
				string[] strArrays1 = strArrays;
				int num = 0;
				while (num < (int)strArrays1.Length)
				{
					string str = strArrays1[num];
					if (!str.StartsWith(cultureCode))
					{
						num++;
					}
					else
					{
						return string.Concat("~/libs/bootstrap-select/i18n/defaults-", str, ".js");
					}
				}
			}
			catch
			{
			}
			return null;
		}

		private static string GetLocalizationFileForjAngularOrNull(string cultureCode)
		{
			try
			{
				string str = string.Concat("~/Scripts/i18n/angular-locale_", cultureCode, ".js");
				if (File.Exists(HttpContext.Current.Server.MapPath(str)))
				{
					return str;
				}
			}
			catch
			{
			}
			return null;
		}

		private static string GetLocalizationFileForjQueryValidationOrNull(string cultureCode)
		{
			try
			{
				string str = string.Concat("~/libs/jquery-validation/js/localization/messages_", cultureCode, ".min.js");
				if (File.Exists(HttpContext.Current.Server.MapPath(str)))
				{
					return str;
				}
			}
			catch
			{
			}
			return null;
		}

		private static string GetLocalizationFileForJTableOrNull(string cultureCode)
		{
			try
			{
				string str = string.Concat("~/libs/jquery-jtable/localization/jquery.jtable.", cultureCode, ".js");
				if (File.Exists(HttpContext.Current.Server.MapPath(str)))
				{
					return str;
				}
			}
			catch
			{
			}
			return null;
		}
	}
}