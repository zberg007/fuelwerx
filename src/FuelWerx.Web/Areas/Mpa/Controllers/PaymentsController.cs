using Abp.Web.Mvc.Authorization;
using FuelWerx.Administrative.Titles;
using FuelWerx.Customers;
using FuelWerx.Generic;
using FuelWerx.Invoices.Dto;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Tenant.InvoicePayments" })]
	public class PaymentsController : FuelWerxControllerBase
	{
		private readonly ICustomerAppService _customerAppService;

		private readonly IGenericAppService _genericAppService;

		private readonly ITitleAppService _titleAppService;

		public PaymentsController(ICustomerAppService customerAppService, IGenericAppService genericAppService, ITitleAppService titleAppService)
		{
			this._customerAppService = customerAppService;
			this._genericAppService = genericAppService;
			this._titleAppService = titleAppService;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.InvoicePayments.Create" })]
		public async Task<PartialViewResult> CreateOrViewPaymentModal(long? id)
		{
			return this.PartialView();
		}

		public ActionResult Index(GetInvoicePaymentsInput input)
		{
			return base.View();
		}
	}
}