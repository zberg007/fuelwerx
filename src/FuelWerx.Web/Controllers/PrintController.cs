using Abp.Web.Mvc.Controllers;
using FuelWerx.Print;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class PrintController : FuelWerxControllerBase
	{
		private readonly IPrintAppService _printAppService;

		public PrintController(IPrintAppService printAppService)
		{
			this._printAppService = printAppService;
		}

		[AllowAnonymous]
		public async Task<ActionResult> EstimatePDF(long id)
		{
			try
			{
				string str = await this._printAppService.ViewEstimateForPDF(id);
				this.ViewData["PDFHtml"] = str;
			}
			catch (Exception)
			{
				ViewDataDictionary viewData = this.ViewData;
				string[] strArrays = new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" };
				viewData["PDFHtml"] = string.Concat(strArrays);
			}
			return this.View();
		}

		public async Task<ActionResult> EstimatePrint(long id)
		{
			try
			{
				string estimateForPrint = await this._printAppService.GetEstimateForPrint(id);
				this.ViewData["PrintHtml"] = estimateForPrint;
			}
			catch (Exception)
			{
				ViewDataDictionary viewData = this.ViewData;
				string[] strArrays = new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" };
				viewData["PrintHtml"] = string.Concat(strArrays);
			}
			return this.View();
		}

		public async Task<ActionResult> EstimateSend(long id)
		{
			try
			{
				await this._printAppService.SendEstimate(id);
				this.ViewData["PrintHtml"] = string.Concat("<html><head><title>", this.L("Message_Sent"), "</title><script>setTimeout('window.close();', 10 * 1000);</script></head><body></body></html>");
			}
			catch (Exception)
			{
				ViewDataDictionary viewData = this.ViewData;
				string[] strArrays = new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" };
				viewData["PrintHtml"] = string.Concat(strArrays);
			}
			return this.View();
		}

		public ActionResult Index(long id)
		{
			base.ViewData["PrintHtml"] = string.Concat(new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" });
			return base.View();
		}

		[AllowAnonymous]
		public async Task<ActionResult> InvoicePDF(long id)
		{
			try
			{
				string str = await this._printAppService.ViewInvoiceForPDF(id);
				this.ViewData["PDFHtml"] = str;
			}
			catch (Exception)
			{
				ViewDataDictionary viewData = this.ViewData;
				string[] strArrays = new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" };
				viewData["PDFHtml"] = string.Concat(strArrays);
			}
			return this.View();
		}

		public async Task<ActionResult> InvoicePrint(long id)
		{
			try
			{
				string invoiceForPrint = await this._printAppService.GetInvoiceForPrint(id);
				this.ViewData["PrintHtml"] = invoiceForPrint;
			}
			catch (Exception)
			{
				ViewDataDictionary viewData = this.ViewData;
				string[] strArrays = new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" };
				viewData["PrintHtml"] = string.Concat(strArrays);
			}
			return this.View();
		}

		public async Task<ActionResult> InvoiceSend(long id)
		{
			try
			{
				await this._printAppService.SendInvoice(id);
				this.ViewData["PrintHtml"] = string.Concat("<html><head><title>", this.L("Message_Sent"), "</title><script>setTimeout('window.close();', 10 * 1000);</script></head><body></body></html>");
			}
			catch (Exception)
			{
				ViewDataDictionary viewData = this.ViewData;
				string[] strArrays = new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" };
				viewData["PrintHtml"] = string.Concat(strArrays);
			}
			return this.View();
		}

		[AllowAnonymous]
		public async Task<ActionResult> ProjectPDF(long id)
		{
			try
			{
				string str = await this._printAppService.ViewProjectForPDF(id);
				this.ViewData["PDFHtml"] = str;
			}
			catch (Exception)
			{
				ViewDataDictionary viewData = this.ViewData;
				string[] strArrays = new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" };
				viewData["PDFHtml"] = string.Concat(strArrays);
			}
			return this.View();
		}

		public async Task<ActionResult> ProjectPrint(long id)
		{
			try
			{
				string projectForPrint = await this._printAppService.GetProjectForPrint(id);
				this.ViewData["PrintHtml"] = projectForPrint;
			}
			catch (Exception)
			{
				ViewDataDictionary viewData = this.ViewData;
				string[] strArrays = new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" };
				viewData["PrintHtml"] = string.Concat(strArrays);
			}
			return this.View();
		}

		public async Task<ActionResult> ProjectSend(long id)
		{
			try
			{
				await this._printAppService.SendProject(id);
				this.ViewData["PrintHtml"] = string.Concat("<html><head><title>", this.L("Message_Sent"), "</title><script>setTimeout('window.close();', 10 * 1000);</script></head><body></body></html>");
			}
			catch (Exception)
			{
				ViewDataDictionary viewData = this.ViewData;
				string[] strArrays = new string[] { "<html><head><title>", this.L("Print_DefaultNotFound_PageTitle"), "</title></head><body>", this.L("Print_DefaultNotFound_Body"), "</body></html>" };
				viewData["PrintHtml"] = string.Concat(strArrays);
			}
			return this.View();
		}
	}
}