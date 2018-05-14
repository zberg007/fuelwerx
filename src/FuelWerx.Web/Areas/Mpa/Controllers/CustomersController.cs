using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Administrative.Titles;
using FuelWerx.Administrative.Titles.Dto;
using FuelWerx.Customers;
using FuelWerx.Customers.Dto;
using FuelWerx.Generic;
using FuelWerx.Invoices;
using FuelWerx.Web.Areas.Mpa.Models.Customers;
using FuelWerx.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(new string[] { "Pages.Tenant.Customers" })]
    public class CustomersController : FuelWerxControllerBase
    {
        private readonly ICustomerAppService _customerAppService;

        private readonly IGenericAppService _genericAppService;

        private readonly ITitleAppService _titleAppService;

        private readonly IRepository<Invoice, long> _invoiceRepository;

        private readonly IRepository<InvoicePayment, long> _invoicePaymentRepository;

        public CustomersController(ICustomerAppService customerAppService, IGenericAppService genericAppService, ITitleAppService titleAppService, IRepository<Invoice, long> invoiceRepository, IRepository<InvoicePayment, long> invoicePaymentRepository)
        {
            this._customerAppService = customerAppService;
            this._genericAppService = genericAppService;
            this._titleAppService = titleAppService;
            this._invoiceRepository = invoiceRepository;
            this._invoicePaymentRepository = invoicePaymentRepository;
        }

        [AbpMvcAuthorize(new string[] { "Pages.Tenant.Customers.Create" })]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            var theId = id;
            NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
            {
                Id = id
            };
            var getCustomerForEditOutput = await _customerAppService.GetCustomerForEdit(nullableIdInput);
            if (id.HasValue)
            {
                decimal totalOwed = 0;
                // Retrieve active invoices for the customer where they haven't paid at all or haven't paid in full.
                var invoices = await _invoiceRepository.GetAllListAsync(m => m.CustomerId == id
                    && (!m.PaidTotal.HasValue || m.PaidTotal.HasValue && m.PaidTotal < m.LineTotal)
                    && m.IsActive);
                foreach (var invoice in invoices)
                {
                    if (invoice.PaidTotal.HasValue)
                    {
                        totalOwed += invoice.PaidTotal.Value - invoice.LineTotal;
                    }
                    else if (invoice.LineTotal > 0)
                    {
                        totalOwed += invoice.LineTotal;
                    }
                }
                getCustomerForEditOutput.CustomerOwesTotal = totalOwed;
            }
            else
            {
                getCustomerForEditOutput.CustomerOwesTotal = 0;
            }
            var createOrEditCustomerModalViewModel = new CreateOrEditCustomerModalViewModel(getCustomerForEditOutput);
            var list = new List<SelectListItem>();
            var titles = await _titleAppService.GetTitlesForTenant(AbpSession.ImpersonatorTenantId.HasValue ? AbpSession.ImpersonatorTenantId.Value : AbpSession.TenantId.Value);
            foreach (var title in titles)
            {
                list.Add(new SelectListItem()
                {
                    Text = title.Name,
                    Value = title.Id.ToString(),
                    Disabled = false,
                    Selected = false
                });
            }
            var firstItem = new SelectListItem()
            {
                Text = String.Empty,
                Value = String.Empty,
                Disabled = false
            };
            list.Insert(0, firstItem);
            ViewData["Titles"] = list;
            return PartialView("_CreateOrEditModal", createOrEditCustomerModalViewModel);
        }

        public ActionResult Index(GetCustomersInput input)
        {
            return base.View();
        }

        public ActionResult Register()
        {
            return base.View();
        }
    }
}