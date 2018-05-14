using Abp;
using Abp.Application.Services;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.Timing;
using FuelWerx;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Configuration.Tenants.Dto;
using FuelWerx.Customers;
using FuelWerx.Emailing;
using FuelWerx.Estimates;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.Invoices;
using FuelWerx.MultiTenancy;
using FuelWerx.Products;
using FuelWerx.Projects;
using FuelWerx.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FuelWerx.Print
{
    public class PrintAppService : FuelWerxAppServiceBase, IPrintAppService, IApplicationService, ITransientDependency
    {
        private readonly TenantSettingsAppService _tenantSettingsAppService;

        private readonly FuelWerx.MultiTenancy.TenantManager _tenantManager;

        private readonly IWebUrlService _webUrlService;

        private readonly IEmailTemplateProvider _emailTemplateProvider;

        private readonly IEmailSender _emailSender;

        private readonly IRepository<Estimate, long> _estimateRepository;

        private readonly IRepository<EstimateTask, long> _estimateTaskRepository;

        private readonly IRepository<EstimateAdhocProduct, long> _estimateAdhocProductRepository;

        private readonly IRepository<EstimateProduct, long> _estimateProductRepository;

        private readonly IRepository<ProductOption, long> _productOptionRepository;

        private readonly IRepository<EstimateAdjustment, long> _estimateAdjustmentRepository;

        private readonly IRepository<EstimateProductLineItem, long> _estimateProductLineItemRepository;

        private readonly IRepository<Project, long> _projectRepository;

        private readonly IRepository<ProjectTask, long> _projectTaskRepository;

        private readonly IRepository<ProjectTeamMember, long> _projectTeamMemberRepository;

        private readonly IRepository<ProjectAdhocProduct, long> _projectAdhocProductRepository;

        private readonly IRepository<ProjectProduct, long> _projectProductRepository;

        private readonly IRepository<ProjectAdjustment, long> _projectAdjustmentRepository;

        private readonly IRepository<ProjectProductLineItem, long> _projectProductLineItemRepository;

        private readonly IRepository<Invoice, long> _invoiceRepository;

        private readonly IRepository<InvoiceTask, long> _invoiceTaskRepository;

        private readonly IRepository<InvoiceTeamMember, long> _invoiceTeamMemberRepository;

        private readonly IRepository<InvoiceAdhocProduct, long> _invoiceAdhocProductRepository;

        private readonly IRepository<InvoiceProduct, long> _invoiceProductRepository;

        private readonly IRepository<InvoiceAdjustment, long> _invoiceAdjustmentRepository;

        private readonly IRepository<InvoiceProductLineItem, long> _invoiceProductLineItemRepository;

        private readonly IRepository<InvoicePayment, long> _invoicePaymentRepository;

        private readonly IRepository<Customer, long> _customerRepository;

        private readonly IRepository<Address, long> _addressRepository;

        private readonly IRepository<CountryRegion> _countryRegionRepository;

        public PrintAppService(TenantSettingsAppService tenantSettingsAppService, FuelWerx.MultiTenancy.TenantManager tenantManager, IWebUrlService webUrlService, IRepository<Estimate, long> estimateRepository, IRepository<EstimateTask, long> estimateTaskRepository, IRepository<EstimateAdhocProduct, long> estimateAdhocProductRepository, IRepository<EstimateProduct, long> estimateProductRepository, IRepository<EstimateAdjustment, long> estimateAdjustmentRepository, IRepository<EstimateProductLineItem, long> estimateProductLineItemRepository, IRepository<ProductOption, long> productOptionRepository, IRepository<Project, long> projectRepository, IRepository<ProjectTeamMember, long> projectTeamMemberRepository, IRepository<ProjectTask, long> projectTaskRepository, IRepository<ProjectAdhocProduct, long> projectAdhocProductRepository, IRepository<ProjectProduct, long> projectProductRepository, IRepository<ProjectAdjustment, long> projectAdjustmentRepository, IRepository<ProjectProductLineItem, long> projectProductLineItemRepository, IRepository<Invoice, long> invoiceRepository, IRepository<InvoiceTeamMember, long> invoiceTeamMemberRepository, IRepository<InvoiceTask, long> invoiceTaskRepository, IRepository<InvoiceAdhocProduct, long> invoiceAdhocProductRepository, IRepository<InvoiceProduct, long> invoiceProductRepository, IRepository<InvoiceAdjustment, long> invoiceAdjustmentRepository, IRepository<InvoiceProductLineItem, long> invoiceProductLineItemRepository, IRepository<Customer, long> customerRepository, IRepository<Address, long> addressRepository, IRepository<CountryRegion> countryRegionRepository, IRepository<InvoicePayment, long> invoicePaymentRepository, IEmailTemplateProvider emailTemplateProvider, IEmailSender emailSender)
        {
            this._tenantSettingsAppService = tenantSettingsAppService;
            this._tenantManager = tenantManager;
            this._webUrlService = webUrlService;
            this._estimateRepository = estimateRepository;
            this._estimateTaskRepository = estimateTaskRepository;
            this._estimateAdhocProductRepository = estimateAdhocProductRepository;
            this._estimateProductRepository = estimateProductRepository;
            this._estimateAdjustmentRepository = estimateAdjustmentRepository;
            this._estimateProductLineItemRepository = estimateProductLineItemRepository;
            this._productOptionRepository = productOptionRepository;
            this._projectRepository = projectRepository;
            this._projectTaskRepository = projectTaskRepository;
            this._projectTeamMemberRepository = projectTeamMemberRepository;
            this._projectAdhocProductRepository = projectAdhocProductRepository;
            this._projectProductRepository = projectProductRepository;
            this._projectAdjustmentRepository = projectAdjustmentRepository;
            this._projectProductLineItemRepository = projectProductLineItemRepository;
            this._invoiceRepository = invoiceRepository;
            this._invoiceTaskRepository = invoiceTaskRepository;
            this._invoiceTeamMemberRepository = invoiceTeamMemberRepository;
            this._invoiceAdhocProductRepository = invoiceAdhocProductRepository;
            this._invoiceProductRepository = invoiceProductRepository;
            this._invoiceAdjustmentRepository = invoiceAdjustmentRepository;
            this._invoiceProductLineItemRepository = invoiceProductLineItemRepository;
            this._customerRepository = customerRepository;
            this._addressRepository = addressRepository;
            this._countryRegionRepository = countryRegionRepository;
            this._invoicePaymentRepository = invoicePaymentRepository;
            this._emailTemplateProvider = emailTemplateProvider;
            this._emailSender = emailSender;
        }

        private async Task<string> GenerateEstimateHtml(Tenant tenant, string tenantUrl, TenantSettingsEditDto tenantSettings, Customer customer, Address customerAddress, Estimate estimate, List<EstimateTask> estimateTasks, List<EstimateAdhocProduct> estimateAdhocProducts, List<EstimateProduct> estimateProducts, List<EstimateAdjustment> estimateAdjustments)
        {
            StringBuilder stringBuilder = new StringBuilder(this._emailTemplateProvider.GetDefaultTemplate());
            stringBuilder.Replace("{TENANT_NAME}", tenant.Name);
            stringBuilder.Replace("{CURRENT_YEAR}", DateTime.Now.Year.ToString());
            stringBuilder.Replace("{EMAIL_TITLE}", this.L("PrintEmail_CustomerEstimate"));
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.AppendLine("      <div style=\"Margin:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;min-width:100%;padding:0;text-align:left;width:100%!important\">");
            stringBuilder2.AppendLine("        <table style=\"Margin:0;background:#f3f3f3;border-collapse:collapse;border-spacing:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;height:100%;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("          <tbody>");
            stringBuilder2.AppendLine("            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("              <td align=\"center\" valign=\"top\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
            stringBuilder2.AppendLine("                  <tbody>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <td style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                        <table align=\"center\" style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
            stringBuilder2.AppendLine("                          <tbody>");
            stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                              <td style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                                <table style=\"border-collapse:collapse;border-spacing:0;display:table;padding:0;position:relative;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("                                  <tbody>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <th colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <h5 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:center;word-wrap:normal\">");
            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
            stringBuilder2.AppendLine(this.L("PrintEmail_CustomerEstimate"));
            stringBuilder2.AppendLine("                                          </span>");
            stringBuilder2.AppendLine("                                        </h5>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                    </tr>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <table style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("                                          <tbody>");
            stringBuilder2.AppendLine("                                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                              <th style=\"Margin:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(string.Concat(new object[]
            {
        "<img src=",
        tenantUrl,
        "Mpa/Settings/GetLogoById?logoId=",
        tenantSettings.Logo.InvoiceImageId.HasValue ? tenantSettings.Logo.InvoiceImageId.Value : Guid.Empty,
        "&logoType=header&viewContrast=light&t=",
        Clock.Now.Ticks.ToString(),
        "width=\"168\" height=\"47\" alt=\"",
        tenant.Name,
        " - Intelligent Propane Software\" title=\"",
        tenant.Name,
        " - Intelligent Propane Software\" border=\"0\" style=\"display: block;\">"
            }));
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("<span style=\"font-weight:bold\">" + tenant.Name + " </span>");
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.AppendLine(tenantSettings.Details.Address);
            if (tenantSettings.Details.SecondaryAddress.Length > 0)
            {
                stringBuilder2.AppendLine("<br />" + tenantSettings.Details.SecondaryAddress);
            }
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(tenantSettings.Details.City);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append((tenantSettings.Details.CountryRegionId.HasValue ? tenantSettings.Details.CountryRegion.Code : "") + ((tenantSettings.Details.CountryRegionId.HasValue && tenantSettings.Details.PostalCode.Length > 0) ? ", " : "") + tenantSettings.Details.PostalCode);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                              </th>");
            stringBuilder2.AppendLine("                                            </tr>");
            stringBuilder2.AppendLine("                                          </tbody>");
            stringBuilder2.AppendLine("                                        </table>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <table style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("                                          <tbody>");
            stringBuilder2.AppendLine("                                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                              <th style=\"Margin:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_Summary"));
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_Status") + " " + estimate.CurrentStatus);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_EstimateReferenceNumber") + " " + estimate.Number);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_EstimateDate") + " " + (estimate.Date.HasValue ? DateTime.Parse(estimate.Date.ToString()).ToShortDateString() : ""));
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_EstimateTotal") + " $" + estimate.LineTotal);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                              </th>");
            stringBuilder2.AppendLine("                                            </tr>");
            stringBuilder2.AppendLine("                                          </tbody>");
            stringBuilder2.AppendLine("                                        </table>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                    </tr>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:center;word-wrap:normal\">");
            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
            stringBuilder2.AppendLine(this.L("PrintEmail_EstimateInfo"));
            stringBuilder2.AppendLine("                                          </span>");
            stringBuilder2.AppendLine("                                        </h6>");
            stringBuilder2.AppendLine("                                      </td>");
            stringBuilder2.AppendLine("                                    </tr>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
            stringBuilder2.Append(this.L("PrintEmail_CustomerInformation"));
            stringBuilder2.AppendLine("                                          </span>");
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append((customer.BusinessName != null && customer.BusinessName.Length > 0) ? customer.BusinessName : (customer.FirstName + " " + customer.LastName));
            stringBuilder2.AppendLine("                                        </p>");
            if (customerAddress != null && customerAddress.Id > 0L)
            {
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.Append(customerAddress.PrimaryAddress);
                if (customerAddress.SecondaryAddress.Length > 0)
                {
                    stringBuilder2.Append("<br />" + customerAddress.SecondaryAddress);
                }
                stringBuilder2.AppendLine("                                        </p>");
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.Append(string.Concat(new string[]
                {
            customerAddress.City,
            " ",
            (customerAddress.City.Length > 0 && (customerAddress.CountryRegionId.HasValue || customerAddress.CountryId > 0)) ? ", " : " ",
            customerAddress.CountryRegionId.HasValue ? (customerAddress.CountryRegion.Code + " ") : " ",
            (customerAddress.CountryId > 0) ? customerAddress.Country.Code : ""
                }));
                stringBuilder2.AppendLine("                                        </p>");
            }
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_EstimateName") + " " + estimate.Label);
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(estimate.BillingType + ": $" + estimate.Rate);
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_DueDate") + " " + (estimate.DueDate.HasValue ? DateTime.Parse(estimate.DueDate.ToString()).ToShortDateString() : ""));
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_PONumber") + " " + estimate.PONumber);
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                    </tr>");
            if (estimate.Description.Length > 0)
            {
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.Append(this.L("PrintEmail_Description"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </p>");
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.Append(estimate.Description);
                stringBuilder2.AppendLine("                                        </p>");
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
            }
            int num = 0;
            if (estimateTasks != null && estimateTasks.Count > 0)
            {
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.Append(this.L("PrintEmail_TasksIncluded"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </h6>");

                foreach (var current in estimateTasks)
                {
                    stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                    stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskName"));
                    stringBuilder2.AppendLine("                                          </span> " + current.Name);
                    stringBuilder2.AppendLine("                                        </p>");
                    stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                    stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskNote"));
                    stringBuilder2.AppendLine("                                          </span> " + current.Comment);
                    stringBuilder2.AppendLine("                                        </p>");
                    stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                    stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskRetail"));
                    stringBuilder2.AppendLine("                                          </span> $" + current.Retail);
                    stringBuilder2.AppendLine("                                        </p>");
                    if (current.Discount.HasValue && current.Discount.Value > decimal.Zero)
                    {
                        stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                        stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskDiscount") + "</span>");
                        if (current.Discount.HasValue && current.Discount.Value > new decimal(0.999))
                        {
                            stringBuilder2.AppendLine(" $" + current.Discount.Value);
                        }
                        else if (current.Discount.HasValue && current.Discount.Value <= new decimal(0.999))
                        {
                            stringBuilder2.AppendLine(" " + current.Discount.Value + "%");
                        }
                        stringBuilder2.AppendLine("                                        </p>");
                    }
                    if (num < estimateTasks.Count)
                    {
                        stringBuilder2.AppendLine("                                        <hr />");
                    }
                    num++;
                }
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
            }
            int num3 = 0;
            if (estimateProducts != null && estimateProducts.Count > 0)
            {
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_ProductsIncluded"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </h6>");
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
                stringBuilder2.AppendLine("                                  </tbody>");
                stringBuilder2.AppendLine("                                </table>");
                stringBuilder2.AppendLine("                                <table width=\"100%\" border=\"1\" style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                  <tbody>");

                foreach (var estimateProduct in estimateProducts)
                {
                    EstimateProductLineItem estimateProductLineItem = await this._estimateProductLineItemRepository.GetAsync(estimateProduct.LineItemId.Value);
                    EstimateProductLineItem estimateProductLineItem2 = estimateProductLineItem;
                    string text = "";
                    foreach (var epli in estimateProductLineItem2.Options)
                    {
                        text = text + (await this._productOptionRepository.GetAsync(epli.ProductOptionId)).Name + ", ";
                    }
                    stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                    stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:32.33333px\">");
                    stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductQuantity"));
                    stringBuilder2.AppendLine("                                        </span>");
                    stringBuilder2.AppendLine("                                        <br /> " + estimateProductLineItem2.Quantity.ToString());
                    stringBuilder2.AppendLine("                                      </th>");
                    stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:177.33333px\">");
                    stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductImage"));
                    stringBuilder2.AppendLine("                                        </span>");
                    stringBuilder2.AppendLine("                                        <br /> ");
                    if (estimateProduct.Product.ImageId.HasValue && estimateProduct.Product.ImageId.Value != Guid.Empty)
                    {
                        stringBuilder2.AppendLine(string.Concat(new object[]
                        {
                        "                                        <img src=\"",
                        tenantUrl,
                        "Mpa/Products/GetProductImageById?productImageId=",
                        estimateProduct.Product.ImageId,
                        "&ticks=",
                        Clock.Now.Ticks.ToString(),
                        "\" alt=\"",
                        estimateProduct.Product.Name,
                        "\" title=\"",
                        estimateProduct.Product.Name,
                        "\" style=\"clear:both;display:block;max-width:100%;outline:none;text-decoration:none;width:auto\">"
                        }));
                    }
                    stringBuilder2.AppendLine("                                      </th>");
                    stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:225.66667px\">");
                    stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductDescription"));
                    stringBuilder2.AppendLine("                                        </span>");
                    stringBuilder2.AppendLine("                                        <br /> " + estimateProduct.Product.Name);
                    stringBuilder2.AppendLine("                                        <p>" + estimateProduct.Product.Description + ((text.Length > 0) ? string.Concat(new string[]
                    {
                    "</p><p><font style=\"font-weight: bold;\">",
                    this.L("PrintEmail_ProductLineItemOptions"),
                    "</font><br />",
                    text.TrimEnd(new char[0]).TrimEnd(new char[]
                    {
                        ','
                    }),
                    "</p>"
                    }) : ""));
                    stringBuilder2.AppendLine("                                      </th>");
                    stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:80.66667px\">");
                    stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductRetail"));
                    stringBuilder2.AppendLine("                                        </span>");
                    stringBuilder2.AppendLine("                                        <br /> $" + estimateProduct.Product.FinalPrice);
                    stringBuilder2.AppendLine("                                      </th>");
                    stringBuilder2.AppendLine("                                    </tr>");
                    if (num3 < estimateProducts.Count)
                    {
                        stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                                      <td colspan=\"4\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                        stringBuilder2.AppendLine("                                        <hr />");
                        stringBuilder2.AppendLine("                                      </td>");
                        stringBuilder2.AppendLine("                                    </tr>");
                    }
                    int num4 = num3;
                    num3 = num4 + 1;
                    estimateProductLineItem2 = null;
                    text = null;
                }
                stringBuilder2.AppendLine("                                  </tbody>");
                stringBuilder2.AppendLine("                                </table>");
            }
            int num5 = 0;
            if (estimateAdhocProducts != null && estimateAdhocProducts.Count > 0)
            {
                stringBuilder2.AppendLine("                                <table width=\"100%\" border=\"1\" style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                  <tbody>");
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"3\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_MiscellaneousItemsIncluded"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </h6>");
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");

                foreach (var current in estimateAdhocProducts)
                {
                    stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                    stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:225.66667px\">");
                    stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdhocProductName"));
                    stringBuilder2.AppendLine("                                        </span>");
                    stringBuilder2.AppendLine("                                        <br />" + current.Name);
                    stringBuilder2.AppendLine("                                      </th>");
                    stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:225.66667px\">");
                    stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdhocProductDescription"));
                    stringBuilder2.AppendLine("                                        </span>");
                    stringBuilder2.AppendLine("                                        <br />" + current.Description);
                    stringBuilder2.AppendLine("                                      </th>");
                    stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:80.66667px\">");
                    stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdhocProductRetail"));
                    stringBuilder2.AppendLine("                                        </span>");
                    stringBuilder2.AppendLine("                                        <br /> $" + current.RetailCost);
                    stringBuilder2.AppendLine("                                      </th>");
                    stringBuilder2.AppendLine("                                    </tr>");
                    if (num5 < estimateAdhocProducts.Count)
                    {
                        stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                                      <td colspan=\"3\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                        stringBuilder2.AppendLine("                                        <hr />");
                        stringBuilder2.AppendLine("                                      </td>");
                        stringBuilder2.AppendLine("                                    </tr>");
                    }
                }
                num5++;
                stringBuilder2.AppendLine("                                  </tbody>");
                stringBuilder2.AppendLine("                                </table>");
            }
            stringBuilder2.AppendLine("                              </td>");
            stringBuilder2.AppendLine("                            </tr>");
            stringBuilder2.AppendLine("                          </tbody>");
            stringBuilder2.AppendLine("                        </table>");
            int num6 = 0;
            if (estimateAdjustments != null && estimateAdjustments.Count > 0)
            {
                stringBuilder2.AppendLine("                        <table width=\"100%\" border=\"1\" style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                          <tbody>");
                stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                              <td colspan=\"3\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                  <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_CreditsAndReturns"));
                stringBuilder2.AppendLine("                                  </span>");
                stringBuilder2.AppendLine("                                </h6>");
                stringBuilder2.AppendLine("                              </td>");
                stringBuilder2.AppendLine("                            </tr>");
                foreach (var current in estimateAdjustments)
                {
                    stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                    stringBuilder2.AppendLine("                              <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:225.66667px\">");
                    stringBuilder2.AppendLine("                                <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdjustmentName"));
                    stringBuilder2.AppendLine("                                </span>");
                    stringBuilder2.AppendLine("                                <br />" + current.Name);
                    stringBuilder2.AppendLine("                              </th>");
                    stringBuilder2.AppendLine("                              <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:225.66667px\">");
                    stringBuilder2.AppendLine("                                <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdjustmentDescription"));
                    stringBuilder2.AppendLine("                                </span>");
                    stringBuilder2.AppendLine("                                <br />" + current.Description);
                    stringBuilder2.AppendLine("                              </th>");
                    stringBuilder2.AppendLine("                              <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:80.66667px\">");
                    stringBuilder2.AppendLine("                                <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdjustmentRefund"));
                    stringBuilder2.AppendLine("                                </span>");
                    stringBuilder2.AppendLine("                                <br />");
                    stringBuilder2.AppendLine("                                <span style=\"color:red\"> $" + current.RetailCost);
                    stringBuilder2.AppendLine("                                </span>");
                    stringBuilder2.AppendLine("                              </th>");
                    stringBuilder2.AppendLine("                            </tr>");
                    if (num6 < estimateAdjustments.Count)
                    {
                        stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                              <td colspan=\"3\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                        stringBuilder2.AppendLine("                                <hr />");
                        stringBuilder2.AppendLine("                              </td>");
                        stringBuilder2.AppendLine("                            </tr>");
                    }
                    num6++;
                }
                stringBuilder2.AppendLine("                          </tbody>");
                stringBuilder2.AppendLine("                        </table>");
            }
            stringBuilder2.AppendLine("                      </td>");
            stringBuilder2.AppendLine("                    </tr>");
            stringBuilder2.AppendLine("                  </tbody>");
            stringBuilder2.AppendLine("                </table>");
            if (estimate.LogDataAndTasksVisibleToCustomer.HasValue && bool.Parse(estimate.LogDataAndTasksVisibleToCustomer.ToString()))
            {
                stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
                stringBuilder2.AppendLine("                  <tbody>");
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <td style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:564px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_TimeEntryLog"));
                stringBuilder2.AppendLine("                          </span>");
                stringBuilder2.AppendLine("                        </h6>");
                stringBuilder2.AppendLine("                        <p>");
                stringBuilder2.AppendLine(estimate.TimeEntryLog);
                stringBuilder2.AppendLine("                        </p>");
                stringBuilder2.AppendLine("                      </td>");
                stringBuilder2.AppendLine("                    </tr>");
                stringBuilder2.AppendLine("                  </tbody>");
                stringBuilder2.AppendLine("                </table>");
                if (estimate.TimeEntryLog.Length > 0 && estimate.Terms.Length > 0)
                {
                    stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                    stringBuilder2.AppendLine("                              <td colspan=\"3\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                    stringBuilder2.AppendLine("                                <hr />");
                    stringBuilder2.AppendLine("                              </td>");
                    stringBuilder2.AppendLine("                            </tr>");
                }
                stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
                stringBuilder2.AppendLine("                  <tbody>");
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <td style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:564px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_Terms"));
                stringBuilder2.AppendLine("                          </span>");
                stringBuilder2.AppendLine("                        </h6>");
                stringBuilder2.AppendLine("                        <p>");
                stringBuilder2.AppendLine(estimate.Terms);
                stringBuilder2.AppendLine("                        </p>");
                stringBuilder2.AppendLine("                      </td>");
                stringBuilder2.AppendLine("                    </tr>");
                stringBuilder2.AppendLine("                  </tbody>");
                stringBuilder2.AppendLine("                </table>");
            }
            stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
            stringBuilder2.AppendLine("                  <tbody>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <td style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:564px;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
            stringBuilder2.AppendLine("                          <span style=\"font-weight:bold\">");
            stringBuilder2.AppendLine(this.L("PrintEmail_Totals"));
            stringBuilder2.AppendLine("                          </span>");
            stringBuilder2.AppendLine("                        </h6>");
            stringBuilder2.AppendLine("                      </td>");
            stringBuilder2.AppendLine("                    </tr>");
            if (estimate.Hours > decimal.Zero)
            {
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
                stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_EstimateHours"));
                stringBuilder2.AppendLine("                        </span>");
                stringBuilder2.AppendLine("                        <br />" + estimate.Hours.ToString());
                stringBuilder2.AppendLine("                      </th>");
                stringBuilder2.AppendLine("                    </tr>");
            }
            if (estimate.Discount > decimal.Zero)
            {
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
                stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_EstimateDiscount") + "</span>");
                if (estimate.Discount > new decimal(0.999))
                {
                    stringBuilder2.AppendLine("                    <br />$" + estimate.Discount);
                }
                else
                {
                    stringBuilder2.AppendLine("                    <br />" + estimate.Discount + "%");
                }
                stringBuilder2.AppendLine("                      </th>");
                stringBuilder2.AppendLine("                    </tr>");
            }
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
            stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_EstimateTax"));
            stringBuilder2.AppendLine("                        </span>");
            stringBuilder2.AppendLine("                        <br />$" + estimate.Tax);
            stringBuilder2.AppendLine("                      </th>");
            stringBuilder2.AppendLine("                    </tr>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
            stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_EstimateTotal"));
            stringBuilder2.AppendLine("                        </span>");
            stringBuilder2.AppendLine("                        <br />$" + estimate.LineTotal);
            stringBuilder2.AppendLine("                      </th>");
            stringBuilder2.AppendLine("                    </tr>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:right;width:564px\">");
            stringBuilder2.AppendLine("                        <span>");
            stringBuilder2.AppendLine("                          <a href=\"javascript: alert('todo');\" style=\"Margin:0;background:#3adb76;border:0px solid #3adb76;border-radius:3px;color:#fefefe;display:inline-block;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:bold;line-height:1.3;margin:16px 0 0px 0;padding:8px 16px 8px 16px;text-align:center;text-decoration:none;vertical-align:middle;width:auto\">");
            stringBuilder2.Append(this.L("PrintEmail_AcceptEstimate"));
            stringBuilder2.AppendLine("                          </a>");
            stringBuilder2.AppendLine("                        </span>");
            stringBuilder2.AppendLine("                      </th>");
            stringBuilder2.AppendLine("                    </tr>");
            stringBuilder2.AppendLine("                  </tbody>");
            stringBuilder2.AppendLine("                </table>");
            stringBuilder2.AppendLine("              </td>");
            stringBuilder2.AppendLine("            </tr>");
            stringBuilder2.AppendLine("          </tbody>");
            stringBuilder2.AppendLine("        </table>");
            stringBuilder2.AppendLine("      </div>");
            stringBuilder.Replace("{EMAIL_BODY}", stringBuilder2.ToString());
            return stringBuilder.ToString();
        }


        private async Task<string> GenerateInvoiceHtml(Tenant tenant, string tenantUrl, TenantSettingsEditDto tenantSettings, Customer customer, Address customerAddress, Invoice invoice, List<InvoiceTask> invoiceTasks, List<InvoiceAdhocProduct> invoiceAdhocProducts, List<InvoiceProduct> invoiceProducts, List<InvoiceAdjustment> invoiceAdjustments, List<InvoicePayment> invoicePayments)
        {
            StringBuilder stringBuilder = new StringBuilder(this._emailTemplateProvider.GetDefaultTemplate());
            stringBuilder.Replace("{TENANT_NAME}", tenant.Name);
            stringBuilder.Replace("{CURRENT_YEAR}", DateTime.Now.Year.ToString());
            stringBuilder.Replace("{EMAIL_TITLE}", this.L("PrintEmail_CustomerInvoice"));
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.AppendLine("      <div style=\"Margin:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;min-width:100%;padding:0;text-align:left;width:100%!important\">");
            stringBuilder2.AppendLine("        <table style=\"Margin:0;background:#f3f3f3;border-collapse:collapse;border-spacing:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;height:100%;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("          <tbody>");
            stringBuilder2.AppendLine("            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("              <td align=\"center\" valign=\"top\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
            stringBuilder2.AppendLine("                  <tbody>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <td style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                        <table align=\"center\" style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
            stringBuilder2.AppendLine("                          <tbody>");
            stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                              <td style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                                <table style=\"border-collapse:collapse;border-spacing:0;display:table;padding:0;position:relative;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("                                  <tbody>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <th colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <h5 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:center;word-wrap:normal\">");
            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
            stringBuilder2.AppendLine(this.L("PrintEmail_CustomerInvoice"));
            stringBuilder2.AppendLine("                                          </span>");
            stringBuilder2.AppendLine("                                        </h5>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                    </tr>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <table style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("                                          <tbody>");
            stringBuilder2.AppendLine("                                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                              <th style=\"Margin:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(string.Concat(new object[]
            {
        "<img src=",
        tenantUrl,
        "Mpa/Settings/GetLogoById?logoId=",
        tenantSettings.Logo.InvoiceImageId.HasValue ? tenantSettings.Logo.InvoiceImageId.Value : Guid.Empty,
        "&logoType=header&viewContrast=light&t=",
        Clock.Now.Ticks.ToString(),
        "width=\"168\" height=\"47\" alt=\"",
        tenant.Name,
        " - Intelligent Propane Software\" title=\"",
        tenant.Name,
        " - Intelligent Propane Software\" border=\"0\" style=\"display: block;\">"
            }));
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("<span style=\"font-weight:bold\">" + tenant.Name + " </span>");
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.AppendLine(tenantSettings.Details.Address);
            if (tenantSettings.Details.SecondaryAddress.Length > 0)
            {
                stringBuilder2.AppendLine("<br />" + tenantSettings.Details.SecondaryAddress);
            }
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(tenantSettings.Details.City);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append((tenantSettings.Details.CountryRegionId.HasValue ? tenantSettings.Details.CountryRegion.Code : "") + ((tenantSettings.Details.CountryRegionId.HasValue && tenantSettings.Details.PostalCode.Length > 0) ? ", " : "") + tenantSettings.Details.PostalCode);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                              </th>");
            stringBuilder2.AppendLine("                                            </tr>");
            stringBuilder2.AppendLine("                                          </tbody>");
            stringBuilder2.AppendLine("                                        </table>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <table style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("                                          <tbody>");
            stringBuilder2.AppendLine("                                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                              <th style=\"Margin:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_Summary"));
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_Status") + " " + invoice.CurrentStatus);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_InvoiceReferenceNumber") + " " + invoice.Number);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_InvoiceDate") + " " + (invoice.Date.HasValue ? DateTime.Parse(invoice.Date.ToString()).ToShortDateString() : ""));
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_InvoiceTotal") + " $" + invoice.LineTotal);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                              </th>");
            stringBuilder2.AppendLine("                                            </tr>");
            stringBuilder2.AppendLine("                                          </tbody>");
            stringBuilder2.AppendLine("                                        </table>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                    </tr>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:center;word-wrap:normal\">");
            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
            stringBuilder2.AppendLine(this.L("PrintEmail_InvoiceInfo"));
            stringBuilder2.AppendLine("                                          </span>");
            stringBuilder2.AppendLine("                                        </h6>");
            stringBuilder2.AppendLine("                                      </td>");
            stringBuilder2.AppendLine("                                    </tr>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
            stringBuilder2.Append(this.L("PrintEmail_CustomerInformation"));
            stringBuilder2.AppendLine("                                          </span>");
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append((customer.BusinessName != null && customer.BusinessName.Length > 0) ? customer.BusinessName : (customer.FirstName + " " + customer.LastName));
            stringBuilder2.AppendLine("                                        </p>");
            if (customerAddress != null && customerAddress.Id > 0L)
            {
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.Append(customerAddress.PrimaryAddress);
                if (customerAddress.SecondaryAddress.Length > 0)
                {
                    stringBuilder2.Append("<br />" + customerAddress.SecondaryAddress);
                }
                stringBuilder2.AppendLine("                                        </p>");
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.Append(string.Concat(new string[]
                {
            customerAddress.City,
            " ",
            (customerAddress.City.Length > 0 && (customerAddress.CountryRegionId.HasValue || customerAddress.CountryId > 0)) ? ", " : " ",
            customerAddress.CountryRegionId.HasValue ? (customerAddress.CountryRegion.Code + " ") : " ",
            (customerAddress.CountryId > 0) ? customerAddress.Country.Code : ""
                }));
                stringBuilder2.AppendLine("                                        </p>");
            }
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_InvoiceName") + " " + invoice.Label);
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(invoice.BillingType + ": $" + invoice.Rate);
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_DueDate") + " " + (invoice.DueDate.HasValue ? DateTime.Parse(invoice.DueDate.ToString()).ToShortDateString() : ""));
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_PONumber") + " " + invoice.PONumber);
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                    </tr>");
            if (invoice.Description.Length > 0)
            {
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.Append(this.L("PrintEmail_Description"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </p>");
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.Append(invoice.Description);
                stringBuilder2.AppendLine("                                        </p>");
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
            }
            int num = 0;
            if (invoiceTasks != null && invoiceTasks.Count > 0)
            {
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.Append(this.L("PrintEmail_TasksIncluded"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </h6>");
                List<InvoiceTask>.Enumerator enumerator = invoiceTasks.GetEnumerator();
                    foreach (var current in invoiceTasks)
                    {
                        stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                        stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskName"));
                        stringBuilder2.AppendLine("                                          </span> " + current.Name);
                        stringBuilder2.AppendLine("                                        </p>");
                        stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                        stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskNote"));
                        stringBuilder2.AppendLine("                                          </span> " + current.Comment);
                        stringBuilder2.AppendLine("                                        </p>");
                        stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                        stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskRetail"));
                        stringBuilder2.AppendLine("                                          </span> $" + current.Retail);
                        stringBuilder2.AppendLine("                                        </p>");
                        if (current.Discount.HasValue && current.Discount.Value > decimal.Zero)
                        {
                            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskDiscount") + "</span>");
                            if (current.Discount.HasValue && current.Discount.Value > new decimal(0.999))
                            {
                                stringBuilder2.AppendLine(" $" + current.Discount.Value);
                            }
                            else if (current.Discount.HasValue && current.Discount.Value <= new decimal(0.999))
                            {
                                stringBuilder2.AppendLine(" " + current.Discount.Value + "%");
                            }
                            stringBuilder2.AppendLine("                                        </p>");
                        }
                        if (num < invoiceTasks.Count)
                        {
                            stringBuilder2.AppendLine("                                        <hr />");
                        }
                        num++;
                    }
                
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
            }
            int num3 = 0;
            if (invoiceProducts != null && invoiceProducts.Count > 0)
            {
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_ProductsIncluded"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </h6>");
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
                stringBuilder2.AppendLine("                                  </tbody>");
                stringBuilder2.AppendLine("                                </table>");
                stringBuilder2.AppendLine("                                <table width=\"100%\" border=\"1\" style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                  <tbody>");
                    foreach (var invoiceProduct in invoiceProducts)
                    {
                        InvoiceProductLineItem invoiceProductLineItem = await this._invoiceProductLineItemRepository.GetAsync(invoiceProduct.LineItemId.Value);
                        InvoiceProductLineItem invoiceProductLineItem2 = invoiceProductLineItem;
                        string text = "";
                        foreach(var current in invoiceProductLineItem2.Options)
                        {
                            text = text + (await this._productOptionRepository.GetAsync(current.ProductOptionId)).Name + ", ";
                        }
                        stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:32.33333px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductQuantity"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> " + invoiceProductLineItem2.Quantity.ToString());
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:177.33333px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductImage"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> ");
                        if (invoiceProduct.Product.ImageId.HasValue && invoiceProduct.Product.ImageId.Value != Guid.Empty)
                        {
                            stringBuilder2.AppendLine(string.Concat(new object[]
                            {
                        "                                        <img src=\"",
                        tenantUrl,
                        "Mpa/Products/GetProductImageById?productImageId=",
                        invoiceProduct.Product.ImageId,
                        "&ticks=",
                        Clock.Now.Ticks.ToString(),
                        "\" alt=\"",
                        invoiceProduct.Product.Name,
                        "\" title=\"",
                        invoiceProduct.Product.Name,
                        "\" style=\"clear:both;display:block;max-width:100%;outline:none;text-decoration:none;width:auto\">"
                            }));
                        }
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductDescription"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> " + invoiceProduct.Product.Name);
                        stringBuilder2.AppendLine("                                        <p>" + invoiceProduct.Product.Description + ((text.Length > 0) ? string.Concat(new string[]
                        {
                    "</p><p><font style=\"font-weight: bold;\">",
                    this.L("PrintEmail_ProductLineItemOptions"),
                    "</font><br />",
                    text.TrimEnd(new char[0]).TrimEnd(new char[]
                    {
                        ','
                    }),
                    "</p>"
                        }) : ""));
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:80.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductRetail"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> $" + invoiceProduct.Product.FinalPrice);
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                    </tr>");
                        if (num3 < invoiceProducts.Count)
                        {
                            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                            stringBuilder2.AppendLine("                                      <td colspan=\"4\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                            stringBuilder2.AppendLine("                                        <hr />");
                            stringBuilder2.AppendLine("                                      </td>");
                            stringBuilder2.AppendLine("                                    </tr>");
                        }
                        int num4 = num3;
                        num3 = num4 + 1;
                        invoiceProductLineItem2 = null;
                        text = null;
                    }
                stringBuilder2.AppendLine("                                  </tbody>");
                stringBuilder2.AppendLine("                                </table>");
            }
            int num5 = 0;
            if (invoiceAdhocProducts != null && invoiceAdhocProducts.Count > 0)
            {
                stringBuilder2.AppendLine("                                <table width=\"100%\" border=\"1\" style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                  <tbody>");
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"3\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_MiscellaneousItemsIncluded"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </h6>");
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");

                    foreach (var var_21_19F3 in invoiceAdhocProducts)
                    {
                        
                        stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdhocProductName"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br />" + var_21_19F3.Name);
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdhocProductDescription"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br />" + var_21_19F3.Description);
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:80.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdhocProductRetail"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> $" + var_21_19F3.RetailCost);
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                    </tr>");
                        if (num5 < invoiceAdhocProducts.Count)
                        {
                            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                            stringBuilder2.AppendLine("                                      <td colspan=\"3\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                            stringBuilder2.AppendLine("                                        <hr />");
                            stringBuilder2.AppendLine("                                      </td>");
                            stringBuilder2.AppendLine("                                    </tr>");
                        }
                    }
                num5++;
                stringBuilder2.AppendLine("                                  </tbody>");
                stringBuilder2.AppendLine("                                </table>");
            }
            stringBuilder2.AppendLine("                              </td>");
            stringBuilder2.AppendLine("                            </tr>");
            stringBuilder2.AppendLine("                          </tbody>");
            stringBuilder2.AppendLine("                        </table>");
            int num6 = 0;
            if (invoiceAdjustments != null && invoiceAdjustments.Count > 0)
            {
                stringBuilder2.AppendLine("                        <table width=\"100%\" border=\"1\" style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                          <tbody>");
                stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                              <td colspan=\"3\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                  <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_CreditsAndReturns"));
                stringBuilder2.AppendLine("                                  </span>");
                stringBuilder2.AppendLine("                                </h6>");
                stringBuilder2.AppendLine("                              </td>");
                stringBuilder2.AppendLine("                            </tr>");

                    foreach (var var_23_1D69 in invoiceAdjustments)
                    {
                        stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                              <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdjustmentName"));
                        stringBuilder2.AppendLine("                                </span>");
                        stringBuilder2.AppendLine("                                <br />" + var_23_1D69.Name);
                        stringBuilder2.AppendLine("                              </th>");
                        stringBuilder2.AppendLine("                              <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdjustmentDescription"));
                        stringBuilder2.AppendLine("                                </span>");
                        stringBuilder2.AppendLine("                                <br />" + var_23_1D69.Description);
                        stringBuilder2.AppendLine("                              </th>");
                        stringBuilder2.AppendLine("                              <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:80.66667px\">");
                        stringBuilder2.AppendLine("                                <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdjustmentRefund"));
                        stringBuilder2.AppendLine("                                </span>");
                        stringBuilder2.AppendLine("                                <br />");
                        stringBuilder2.AppendLine("                                <span style=\"color:red\"> $" + var_23_1D69.RetailCost);
                        stringBuilder2.AppendLine("                                </span>");
                        stringBuilder2.AppendLine("                              </th>");
                        stringBuilder2.AppendLine("                            </tr>");
                        if (num6 < invoiceAdjustments.Count)
                        {
                            stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                            stringBuilder2.AppendLine("                              <td colspan=\"3\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                            stringBuilder2.AppendLine("                                <hr />");
                            stringBuilder2.AppendLine("                              </td>");
                            stringBuilder2.AppendLine("                            </tr>");
                        }
                        num6++;
                    }
                stringBuilder2.AppendLine("                          </tbody>");
                stringBuilder2.AppendLine("                        </table>");
            }
            stringBuilder2.AppendLine("                      </td>");
            stringBuilder2.AppendLine("                    </tr>");
            stringBuilder2.AppendLine("                  </tbody>");
            stringBuilder2.AppendLine("                </table>");
            if (invoice.LogDataAndTasksVisibleToCustomer.HasValue && bool.Parse(invoice.LogDataAndTasksVisibleToCustomer.ToString()))
            {
                stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
                stringBuilder2.AppendLine("                  <tbody>");
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <td style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:564px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_TimeEntryLog"));
                stringBuilder2.AppendLine("                          </span>");
                stringBuilder2.AppendLine("                        </h6>");
                stringBuilder2.AppendLine("                        <p>");
                stringBuilder2.AppendLine(invoice.TimeEntryLog);
                stringBuilder2.AppendLine("                        </p>");
                stringBuilder2.AppendLine("                      </td>");
                stringBuilder2.AppendLine("                    </tr>");
                stringBuilder2.AppendLine("                  </tbody>");
                stringBuilder2.AppendLine("                </table>");
                if (invoice.TimeEntryLog.Length > 0 && invoice.Terms.Length > 0)
                {
                    stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                    stringBuilder2.AppendLine("                              <td colspan=\"3\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                    stringBuilder2.AppendLine("                                <hr />");
                    stringBuilder2.AppendLine("                              </td>");
                    stringBuilder2.AppendLine("                            </tr>");
                }
                stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
                stringBuilder2.AppendLine("                  <tbody>");
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <td style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:564px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_Terms"));
                stringBuilder2.AppendLine("                          </span>");
                stringBuilder2.AppendLine("                        </h6>");
                stringBuilder2.AppendLine("                        <p>");
                stringBuilder2.AppendLine(invoice.Terms);
                stringBuilder2.AppendLine("                        </p>");
                stringBuilder2.AppendLine("                      </td>");
                stringBuilder2.AppendLine("                    </tr>");
                stringBuilder2.AppendLine("                  </tbody>");
                stringBuilder2.AppendLine("                </table>");
            }
            stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
            stringBuilder2.AppendLine("                  <tbody>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <td style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:564px;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
            stringBuilder2.AppendLine("                          <span style=\"font-weight:bold\">");
            stringBuilder2.AppendLine(this.L("PrintEmail_Totals"));
            stringBuilder2.AppendLine("                          </span>");
            stringBuilder2.AppendLine("                        </h6>");
            stringBuilder2.AppendLine("                      </td>");
            stringBuilder2.AppendLine("                    </tr>");
            if (invoice.Hours > decimal.Zero)
            {
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
                stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_InvoiceHours"));
                stringBuilder2.AppendLine("                        </span>");
                stringBuilder2.AppendLine("                        <br />" + invoice.Hours.ToString());
                stringBuilder2.AppendLine("                      </th>");
                stringBuilder2.AppendLine("                    </tr>");
            }
            if (invoice.Discount > decimal.Zero)
            {
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
                stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_InvoiceDiscount") + "</span>");
                if (invoice.Discount > new decimal(0.999))
                {
                    stringBuilder2.AppendLine("                    <br />$" + invoice.Discount);
                }
                else
                {
                    stringBuilder2.AppendLine("                    <br />" + invoice.Discount + "%");
                }
                stringBuilder2.AppendLine("                      </th>");
                stringBuilder2.AppendLine("                    </tr>");
            }
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
            stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_InvoiceTax"));
            stringBuilder2.AppendLine("                        </span>");
            stringBuilder2.AppendLine("                        <br />$" + invoice.Tax);
            stringBuilder2.AppendLine("                      </th>");
            stringBuilder2.AppendLine("                    </tr>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
            stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_InvoiceTotal"));
            stringBuilder2.AppendLine("                        </span>");
            stringBuilder2.AppendLine("                        <br />$" + invoice.LineTotal);
            stringBuilder2.AppendLine("                      </th>");
            stringBuilder2.AppendLine("                    </tr>");
            if (invoicePayments != null && invoicePayments.Count > 0)
            {
                decimal var_26_26EA = invoice.LineTotal;

                    foreach (var current in invoicePayments)
                    {
                        
                        var_26_26EA -= current.DollarAmount;
                        stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
                        stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + current.TransactionDateTime.ToShortDateString() + " - " + this.L("PrintEmail_InvoicePayment"));
                        stringBuilder2.AppendLine("                        </span>");
                        stringBuilder2.AppendLine("                        <br /><span style=\"color: red;\">$-" + current.DollarAmount + "</span>");
                        stringBuilder2.AppendLine("                      </th>");
                        stringBuilder2.AppendLine("                    </tr>");
                        stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
                        stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_InvoiceBalance"));
                        stringBuilder2.AppendLine("                        </span>");
                        stringBuilder2.AppendLine("                        <br />$" + var_26_26EA.ToString());
                        stringBuilder2.AppendLine("                      </th>");
                        stringBuilder2.AppendLine("                    </tr>");
                    }
            }
            stringBuilder2.AppendLine("                  </tbody>");
            stringBuilder2.AppendLine("                </table>");
            stringBuilder2.AppendLine("              </td>");
            stringBuilder2.AppendLine("            </tr>");
            stringBuilder2.AppendLine("          </tbody>");
            stringBuilder2.AppendLine("        </table>");
            stringBuilder2.AppendLine("      </div>");
            stringBuilder.Replace("{EMAIL_BODY}", stringBuilder2.ToString());
            return stringBuilder.ToString();
        }


        private async Task<string> GenerateProjectHtml(Tenant tenant, string tenantUrl, TenantSettingsEditDto tenantSettings, Customer customer, Address customerAddress, Project project, List<ProjectTask> projectTasks, List<ProjectAdhocProduct> projectAdhocProducts, List<ProjectProduct> projectProducts, List<ProjectAdjustment> projectAdjustments)
        {
            StringBuilder stringBuilder = new StringBuilder(this._emailTemplateProvider.GetDefaultTemplate());
            stringBuilder.Replace("{TENANT_NAME}", tenant.Name);
            stringBuilder.Replace("{CURRENT_YEAR}", DateTime.Now.Year.ToString());
            stringBuilder.Replace("{EMAIL_TITLE}", this.L("PrintEmail_CustomerProject"));
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.AppendLine("      <div style=\"Margin:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;min-width:100%;padding:0;text-align:left;width:100%!important\">");
            stringBuilder2.AppendLine("        <table style=\"Margin:0;background:#f3f3f3;border-collapse:collapse;border-spacing:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;height:100%;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("          <tbody>");
            stringBuilder2.AppendLine("            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("              <td align=\"center\" valign=\"top\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
            stringBuilder2.AppendLine("                  <tbody>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <td style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                        <table align=\"center\" style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
            stringBuilder2.AppendLine("                          <tbody>");
            stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                              <td style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                                <table style=\"border-collapse:collapse;border-spacing:0;display:table;padding:0;position:relative;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("                                  <tbody>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <th colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <h5 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:center;word-wrap:normal\">");
            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
            stringBuilder2.AppendLine(this.L("PrintEmail_CustomerProject"));
            stringBuilder2.AppendLine("                                          </span>");
            stringBuilder2.AppendLine("                                        </h5>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                    </tr>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <table style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("                                          <tbody>");
            stringBuilder2.AppendLine("                                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                              <th style=\"Margin:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(string.Concat(new object[]
            {
        "<img src=",
        tenantUrl,
        "Mpa/Settings/GetLogoById?logoId=",
        tenantSettings.Logo.InvoiceImageId.HasValue ? tenantSettings.Logo.InvoiceImageId.Value : Guid.Empty,
        "&logoType=header&viewContrast=light&t=",
        Clock.Now.Ticks.ToString(),
        "width=\"168\" height=\"47\" alt=\"",
        tenant.Name,
        " - Intelligent Propane Software\" title=\"",
        tenant.Name,
        " - Intelligent Propane Software\" border=\"0\" style=\"display: block;\">"
            }));
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("<span style=\"font-weight:bold\">" + tenant.Name + " </span>");
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.AppendLine(tenantSettings.Details.Address);
            if (tenantSettings.Details.SecondaryAddress.Length > 0)
            {
                stringBuilder2.AppendLine("<br />" + tenantSettings.Details.SecondaryAddress);
            }
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(tenantSettings.Details.City);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append((tenantSettings.Details.CountryRegionId.HasValue ? tenantSettings.Details.CountryRegion.Code : "") + ((tenantSettings.Details.CountryRegionId.HasValue && tenantSettings.Details.PostalCode.Length > 0) ? ", " : "") + tenantSettings.Details.PostalCode);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                              </th>");
            stringBuilder2.AppendLine("                                            </tr>");
            stringBuilder2.AppendLine("                                          </tbody>");
            stringBuilder2.AppendLine("                                        </table>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <table style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top;width:100%\">");
            stringBuilder2.AppendLine("                                          <tbody>");
            stringBuilder2.AppendLine("                                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                              <th style=\"Margin:0;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_Summary"));
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_Status") + " " + project.CurrentStatus);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_ProjectReferenceNumber") + " " + project.Number);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_ProjectDate") + " " + (project.Date.HasValue ? DateTime.Parse(project.Date.ToString()).ToShortDateString() : ""));
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                                <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_ProjectTotal") + " $" + project.LineTotal);
            stringBuilder2.AppendLine("                                                </p>");
            stringBuilder2.AppendLine("                                              </th>");
            stringBuilder2.AppendLine("                                            </tr>");
            stringBuilder2.AppendLine("                                          </tbody>");
            stringBuilder2.AppendLine("                                        </table>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                    </tr>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:center;word-wrap:normal\">");
            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
            stringBuilder2.AppendLine(this.L("PrintEmail_ProjectInfo"));
            stringBuilder2.AppendLine("                                          </span>");
            stringBuilder2.AppendLine("                                        </h6>");
            stringBuilder2.AppendLine("                                      </td>");
            stringBuilder2.AppendLine("                                    </tr>");
            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
            stringBuilder2.Append(this.L("PrintEmail_CustomerInformation"));
            stringBuilder2.AppendLine("                                          </span>");
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append((customer.BusinessName != null && customer.BusinessName.Length > 0) ? customer.BusinessName : (customer.FirstName + " " + customer.LastName));
            stringBuilder2.AppendLine("                                        </p>");
            if (customerAddress != null && customerAddress.Id > 0L)
            {
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.Append(customerAddress.PrimaryAddress);
                if (customerAddress.SecondaryAddress.Length > 0)
                {
                    stringBuilder2.Append("<br />" + customerAddress.SecondaryAddress);
                }
                stringBuilder2.AppendLine("                                        </p>");
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.Append(string.Concat(new string[]
                {
            customerAddress.City,
            " ",
            (customerAddress.City.Length > 0 && (customerAddress.CountryRegionId.HasValue || customerAddress.CountryId > 0)) ? ", " : " ",
            customerAddress.CountryRegionId.HasValue ? (customerAddress.CountryRegion.Code + " ") : " ",
            (customerAddress.CountryId > 0) ? customerAddress.Country.Code : ""
                }));
                stringBuilder2.AppendLine("                                        </p>");
            }
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:274px\">");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_ProjectName") + " " + project.Label);
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(project.BillingType + ": $" + project.Rate);
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_DueDate") + " " + (project.DueDate.HasValue ? DateTime.Parse(project.DueDate.ToString()).ToShortDateString() : ""));
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
            stringBuilder2.Append(this.L("PrintEmail_PONumber") + " " + project.PONumber);
            stringBuilder2.AppendLine("                                        </p>");
            stringBuilder2.AppendLine("                                      </th>");
            stringBuilder2.AppendLine("                                    </tr>");
            if (project.Description.Length > 0)
            {
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.Append(this.L("PrintEmail_Description"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </p>");
                stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                stringBuilder2.Append(project.Description);
                stringBuilder2.AppendLine("                                        </p>");
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
            }
            int num = 0;
            if (projectTasks != null && projectTasks.Count > 0)
            {
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.Append(this.L("PrintEmail_TasksIncluded"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </h6>");

                    foreach (var current in projectTasks)
                    {
                        stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                        stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskName"));
                        stringBuilder2.AppendLine("                                          </span> " + current.Name);
                        stringBuilder2.AppendLine("                                        </p>");
                        stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                        stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskNote"));
                        stringBuilder2.AppendLine("                                          </span> " + current.Comment);
                        stringBuilder2.AppendLine("                                        </p>");
                        stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                        stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskRetail"));
                        stringBuilder2.AppendLine("                                          </span> $" + current.Retail);
                        stringBuilder2.AppendLine("                                        </p>");
                        if (current.Discount.HasValue && current.Discount.Value > decimal.Zero)
                        {
                            stringBuilder2.AppendLine("                                        <p style=\"Margin:0;Margin-bottom:10px;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left\">");
                            stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">" + this.L("PrintEmail_TaskDiscount") + "</span>");
                            if (current.Discount.HasValue && current.Discount.Value > new decimal(0.999))
                            {
                                stringBuilder2.AppendLine(" $" + current.Discount.Value);
                            }
                            else if (current.Discount.HasValue && current.Discount.Value <= new decimal(0.999))
                            {
                                stringBuilder2.AppendLine(" " + current.Discount.Value + "%");
                            }
                            stringBuilder2.AppendLine("                                        </p>");
                        }
                        if (num < projectTasks.Count)
                        {
                            stringBuilder2.AppendLine("                                        <hr />");
                        }
                        num++;
                    }
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
            }
            int num3 = 0;
            if (projectProducts != null && projectProducts.Count > 0)
            {
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"2\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_ProductsIncluded"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </h6>");
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
                stringBuilder2.AppendLine("                                  </tbody>");
                stringBuilder2.AppendLine("                                </table>");
                stringBuilder2.AppendLine("                                <table width=\"100%\" border=\"1\" style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                  <tbody>");

                    foreach (var projectProduct in projectProducts)
                    {
                        ProjectProductLineItem projectProductLineItem = await this._projectProductLineItemRepository.GetAsync(projectProduct.LineItemId.Value);
                        ProjectProductLineItem projectProductLineItem2 = projectProductLineItem;
                        string text = "";
                        foreach (var var_17_13B0 in projectProductLineItem2.Options)
                        {
                            text = text + (await this._productOptionRepository.GetAsync(var_17_13B0.ProductOptionId)).Name + ", ";
                        }
                        stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:32.33333px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductQuantity"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> " + projectProductLineItem2.Quantity.ToString());
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:177.33333px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductImage"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> ");
                        if (projectProduct.Product.ImageId.HasValue && projectProduct.Product.ImageId.Value != Guid.Empty)
                        {
                            stringBuilder2.AppendLine(string.Concat(new object[]
                            {
                        "                                        <img src=\"",
                        tenantUrl,
                        "Mpa/Products/GetProductImageById?productImageId=",
                        projectProduct.Product.ImageId,
                        "&ticks=",
                        Clock.Now.Ticks.ToString(),
                        "\" alt=\"",
                        projectProduct.Product.Name,
                        "\" title=\"",
                        projectProduct.Product.Name,
                        "\" style=\"clear:both;display:block;max-width:100%;outline:none;text-decoration:none;width:auto\">"
                            }));
                        }
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductDescription"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> " + projectProduct.Product.Name);
                        stringBuilder2.AppendLine("                                        <p>" + projectProduct.Product.Description + ((text.Length > 0) ? string.Concat(new string[]
                        {
                    "</p><p><font style=\"font-weight: bold;\">",
                    this.L("PrintEmail_ProductLineItemOptions"),
                    "</font><br />",
                    text.TrimEnd(new char[0]).TrimEnd(new char[]
                    {
                        ','
                    }),
                    "</p>"
                        }) : ""));
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:80.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProductRetail"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> $" + projectProduct.Product.FinalPrice);
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                    </tr>");
                        if (num3 < projectProducts.Count)
                        {
                            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                            stringBuilder2.AppendLine("                                      <td colspan=\"4\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                            stringBuilder2.AppendLine("                                        <hr />");
                            stringBuilder2.AppendLine("                                      </td>");
                            stringBuilder2.AppendLine("                                    </tr>");
                        }
                        int num4 = num3;
                        num3 = num4 + 1;
                        projectProductLineItem2 = null;
                        text = null;
                    }
                stringBuilder2.AppendLine("                                  </tbody>");
                stringBuilder2.AppendLine("                                </table>");
            }
            int num5 = 0;
            if (projectAdhocProducts != null && projectAdhocProducts.Count > 0)
            {
                stringBuilder2.AppendLine("                                <table width=\"100%\" border=\"1\" style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                  <tbody>");
                stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                                      <td colspan=\"3\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_MiscellaneousItemsIncluded"));
                stringBuilder2.AppendLine("                                          </span>");
                stringBuilder2.AppendLine("                                        </h6>");
                stringBuilder2.AppendLine("                                      </td>");
                stringBuilder2.AppendLine("                                    </tr>");
                    foreach (var var_21_19F3 in projectAdhocProducts)
                    {
                        stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdhocProductName"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br />" + var_21_19F3.Name);
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdhocProductDescription"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br />" + var_21_19F3.Description);
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:80.66667px\">");
                        stringBuilder2.AppendLine("                                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdhocProductRetail"));
                        stringBuilder2.AppendLine("                                        </span>");
                        stringBuilder2.AppendLine("                                        <br /> $" + var_21_19F3.RetailCost);
                        stringBuilder2.AppendLine("                                      </th>");
                        stringBuilder2.AppendLine("                                    </tr>");
                        if (num5 < projectAdhocProducts.Count)
                        {
                            stringBuilder2.AppendLine("                                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                            stringBuilder2.AppendLine("                                      <td colspan=\"3\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                            stringBuilder2.AppendLine("                                        <hr />");
                            stringBuilder2.AppendLine("                                      </td>");
                            stringBuilder2.AppendLine("                                    </tr>");
                        }
                    }
                num5++;
                stringBuilder2.AppendLine("                                  </tbody>");
                stringBuilder2.AppendLine("                                </table>");
            }
            stringBuilder2.AppendLine("                              </td>");
            stringBuilder2.AppendLine("                            </tr>");
            stringBuilder2.AppendLine("                          </tbody>");
            stringBuilder2.AppendLine("                        </table>");
            int num6 = 0;
            if (projectAdjustments != null && projectAdjustments.Count > 0)
            {
                stringBuilder2.AppendLine("                        <table width=\"100%\" border=\"1\" style=\"border-collapse:collapse;border-spacing:0;padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                          <tbody>");
                stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                              <td colspan=\"3\" style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:274px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                                <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                                  <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_CreditsAndReturns"));
                stringBuilder2.AppendLine("                                  </span>");
                stringBuilder2.AppendLine("                                </h6>");
                stringBuilder2.AppendLine("                              </td>");
                stringBuilder2.AppendLine("                            </tr>");

                    foreach (var current in projectAdjustments)
                    {
                        stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                        stringBuilder2.AppendLine("                              <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdjustmentName"));
                        stringBuilder2.AppendLine("                                </span>");
                        stringBuilder2.AppendLine("                                <br />" + current.Name);
                        stringBuilder2.AppendLine("                              </th>");
                        stringBuilder2.AppendLine("                              <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:8px;text-align:left;width:225.66667px\">");
                        stringBuilder2.AppendLine("                                <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdjustmentDescription"));
                        stringBuilder2.AppendLine("                                </span>");
                        stringBuilder2.AppendLine("                                <br />" + current.Description);
                        stringBuilder2.AppendLine("                              </th>");
                        stringBuilder2.AppendLine("                              <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:8px;padding-right:16px;text-align:left;width:80.66667px\">");
                        stringBuilder2.AppendLine("                                <span style=\"font-weight:bold\">" + this.L("PrintEmail_AdjustmentRefund"));
                        stringBuilder2.AppendLine("                                </span>");
                        stringBuilder2.AppendLine("                                <br />");
                        stringBuilder2.AppendLine("                                <span style=\"color:red\"> $" + current.RetailCost);
                        stringBuilder2.AppendLine("                                </span>");
                        stringBuilder2.AppendLine("                              </th>");
                        stringBuilder2.AppendLine("                            </tr>");
                        if (num6 < projectAdjustments.Count)
                        {
                            stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                            stringBuilder2.AppendLine("                              <td colspan=\"3\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                            stringBuilder2.AppendLine("                                <hr />");
                            stringBuilder2.AppendLine("                              </td>");
                            stringBuilder2.AppendLine("                            </tr>");
                        }
                        num6++;
                    }
                stringBuilder2.AppendLine("                          </tbody>");
                stringBuilder2.AppendLine("                        </table>");
            }
            stringBuilder2.AppendLine("                      </td>");
            stringBuilder2.AppendLine("                    </tr>");
            stringBuilder2.AppendLine("                  </tbody>");
            stringBuilder2.AppendLine("                </table>");
            if (project.LogDataAndTasksVisibleToCustomer.HasValue && bool.Parse(project.LogDataAndTasksVisibleToCustomer.ToString()))
            {
                stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
                stringBuilder2.AppendLine("                  <tbody>");
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <td style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:564px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_TimeEntryLog"));
                stringBuilder2.AppendLine("                          </span>");
                stringBuilder2.AppendLine("                        </h6>");
                stringBuilder2.AppendLine("                        <p>");
                stringBuilder2.AppendLine(project.TimeEntryLog);
                stringBuilder2.AppendLine("                        </p>");
                stringBuilder2.AppendLine("                      </td>");
                stringBuilder2.AppendLine("                    </tr>");
                stringBuilder2.AppendLine("                  </tbody>");
                stringBuilder2.AppendLine("                </table>");
                if (project.TimeEntryLog.Length > 0 && project.Terms.Length > 0)
                {
                    stringBuilder2.AppendLine("                            <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                    stringBuilder2.AppendLine("                              <td colspan=\"3\" style=\"Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word\">");
                    stringBuilder2.AppendLine("                                <hr />");
                    stringBuilder2.AppendLine("                              </td>");
                    stringBuilder2.AppendLine("                            </tr>");
                }
                stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
                stringBuilder2.AppendLine("                  <tbody>");
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <td style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:564px;word-wrap:break-word\">");
                stringBuilder2.AppendLine("                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
                stringBuilder2.AppendLine("                          <span style=\"font-weight:bold\">");
                stringBuilder2.AppendLine(this.L("PrintEmail_Terms"));
                stringBuilder2.AppendLine("                          </span>");
                stringBuilder2.AppendLine("                        </h6>");
                stringBuilder2.AppendLine("                        <p>");
                stringBuilder2.AppendLine(project.Terms);
                stringBuilder2.AppendLine("                        </p>");
                stringBuilder2.AppendLine("                      </td>");
                stringBuilder2.AppendLine("                    </tr>");
                stringBuilder2.AppendLine("                  </tbody>");
                stringBuilder2.AppendLine("                </table>");
            }
            stringBuilder2.AppendLine("                <table style=\"Margin:0 auto;background:#fefefe;border-collapse:collapse;border-spacing:0;margin:0 auto;padding:0;text-align:inherit;vertical-align:top;width:580px\">");
            stringBuilder2.AppendLine("                  <tbody>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <td style=\"Margin:0 auto;border:1px solid #333;border-collapse:collapse!important;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:left;vertical-align:top;width:564px;word-wrap:break-word\">");
            stringBuilder2.AppendLine("                        <h6 style=\"Margin:0;Margin-bottom:10px;color:inherit;font-family:Helvetica,Arial,sans-serif;font-size:18px;font-weight:normal;line-height:1.3;margin:0;margin-bottom:10px;padding:0;text-align:left;word-wrap:normal\">");
            stringBuilder2.AppendLine("                          <span style=\"font-weight:bold\">");
            stringBuilder2.AppendLine(this.L("PrintEmail_Totals"));
            stringBuilder2.AppendLine("                          </span>");
            stringBuilder2.AppendLine("                        </h6>");
            stringBuilder2.AppendLine("                      </td>");
            stringBuilder2.AppendLine("                    </tr>");
            if (project.Hours > decimal.Zero)
            {
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
                stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProjectHours"));
                stringBuilder2.AppendLine("                        </span>");
                stringBuilder2.AppendLine("                        <br />" + project.Hours.ToString());
                stringBuilder2.AppendLine("                      </th>");
                stringBuilder2.AppendLine("                    </tr>");
            }
            if (project.Discount > decimal.Zero)
            {
                stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
                stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
                stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProjectDiscount") + "</span>");
                if (project.Discount > new decimal(0.999))
                {
                    stringBuilder2.AppendLine("                    <br />$" + project.Discount);
                }
                else
                {
                    stringBuilder2.AppendLine("                    <br />" + project.Discount + "%");
                }
                stringBuilder2.AppendLine("                      </th>");
                stringBuilder2.AppendLine("                    </tr>");
            }
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
            stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProjectTax"));
            stringBuilder2.AppendLine("                        </span>");
            stringBuilder2.AppendLine("                        <br />$" + project.Tax);
            stringBuilder2.AppendLine("                      </th>");
            stringBuilder2.AppendLine("                    </tr>");
            stringBuilder2.AppendLine("                    <tr style=\"padding:0;text-align:left;vertical-align:top\">");
            stringBuilder2.AppendLine("                      <th style=\"Margin:0 auto;border:1px solid #333;color:#0a0a0a;font-family:Helvetica,Arial,sans-serif;font-size:16px;font-weight:normal;line-height:1.3;margin:0 auto;padding:0;padding-bottom:16px;padding-left:16px;padding-right:8px;text-align:right;width:564px\">");
            stringBuilder2.AppendLine("                        <span style=\"font-weight:bold\">" + this.L("PrintEmail_ProjectTotal"));
            stringBuilder2.AppendLine("                        </span>");
            stringBuilder2.AppendLine("                        <br />$" + project.LineTotal);
            stringBuilder2.AppendLine("                      </th>");
            stringBuilder2.AppendLine("                    </tr>");
            stringBuilder2.AppendLine("                  </tbody>");
            stringBuilder2.AppendLine("                </table>");
            stringBuilder2.AppendLine("              </td>");
            stringBuilder2.AppendLine("            </tr>");
            stringBuilder2.AppendLine("          </tbody>");
            stringBuilder2.AppendLine("        </table>");
            stringBuilder2.AppendLine("      </div>");
            stringBuilder.Replace("{EMAIL_BODY}", stringBuilder2.ToString());
            return stringBuilder.ToString();
        }


        public async Task<string> GetEstimateForPrint(long input)
        {
            int value;
            int? impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
            if (impersonatorTenantId.HasValue)
            {
                impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
                value = impersonatorTenantId.Value;
            }
            else
            {
                value = this.AbpSession.GetTenantId();
            }
            int num = value;
            Estimate async = await this._estimateRepository.GetAsync(input);
            if (async == null || async.TenantId != num)
            {
                if (async != null)
                {
                    throw new Exception("SecurityViolation");
                }
                throw new Exception("EstimateIsNull");
            }
            string tenancyName = (await this._tenantManager.GetByIdAsync(num)).TenancyName;
            string str = tenancyName;
            string str1 = tenancyName;
            str1 = str;
            Tenant tenant = await this._tenantManager.FindByTenancyNameAsync(str1);
            string siteRootAddress = this._webUrlService.GetSiteRootAddress(str1);
            impersonatorTenantId = null;
            TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(impersonatorTenantId);
            Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
            Address address = new Address();
            if (async.CustomerAddressId.HasValue)
            {
                IRepository<Address, long> repository = this._addressRepository;
                address = await repository.GetAsync(async.CustomerAddressId.Value);
                impersonatorTenantId = address.CountryRegionId;
                if (impersonatorTenantId.HasValue)
                {
                    Address async1 = address;
                    IRepository<CountryRegion> repository1 = this._countryRegionRepository;
                    impersonatorTenantId = async.CustomerAddress.CountryRegionId;
                    async1.CountryRegion = await repository1.GetAsync(impersonatorTenantId.Value);
                    async1 = null;
                }
            }
            IRepository<EstimateTask, long> repository2 = this._estimateTaskRepository;
            List<EstimateTask> allListAsync = await repository2.GetAllListAsync((EstimateTask m) => m.EstimateId == async.Id);
            List<EstimateTask> estimateTasks = allListAsync;
            IRepository<EstimateAdhocProduct, long> repository3 = this._estimateAdhocProductRepository;
            List<EstimateAdhocProduct> estimateAdhocProducts = await repository3.GetAllListAsync((EstimateAdhocProduct m) => m.EstimateId == async.Id);
            List<EstimateAdhocProduct> estimateAdhocProducts1 = estimateAdhocProducts;
            IRepository<EstimateProduct, long> repository4 = this._estimateProductRepository;
            List<EstimateProduct> estimateProducts = await repository4.GetAllListAsync((EstimateProduct m) => m.EstimateId == async.Id);
            List<EstimateProduct> estimateProducts1 = estimateProducts;
            IRepository<EstimateAdjustment, long> repository5 = this._estimateAdjustmentRepository;
            List<EstimateAdjustment> estimateAdjustments = await repository5.GetAllListAsync((EstimateAdjustment m) => m.EstimateId == async.Id);
            List<EstimateAdjustment> estimateAdjustments1 = estimateAdjustments;
            string str2 = await this.GenerateEstimateHtml(tenant, siteRootAddress, allSettings, customer, address, async, estimateTasks, estimateAdhocProducts1, estimateProducts1, estimateAdjustments1);
            return str2;
        }

        public async Task<string> GetInvoiceForPrint(long input)
        {
            int value;
            int? impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
            if (impersonatorTenantId.HasValue)
            {
                impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
                value = impersonatorTenantId.Value;
            }
            else
            {
                value = this.AbpSession.GetTenantId();
            }
            int num = value;
            Invoice async = await this._invoiceRepository.GetAsync(input);
            if (async == null || async.TenantId != num)
            {
                if (async != null)
                {
                    throw new Exception("SecurityViolation");
                }
                throw new Exception("InvoiceIsNull");
            }
            string tenancyName = (await this._tenantManager.GetByIdAsync(num)).TenancyName;
            string str = tenancyName;
            string str1 = tenancyName;
            str1 = str;
            Tenant tenant = await this._tenantManager.FindByTenancyNameAsync(str1);
            string siteRootAddress = this._webUrlService.GetSiteRootAddress(str1);
            impersonatorTenantId = null;
            TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(impersonatorTenantId);
            Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
            Address address = new Address();
            if (async.CustomerAddressId.HasValue)
            {
                IRepository<Address, long> repository = this._addressRepository;
                address = await repository.GetAsync(async.CustomerAddressId.Value);
                impersonatorTenantId = address.CountryRegionId;
                if (impersonatorTenantId.HasValue)
                {
                    Address async1 = address;
                    IRepository<CountryRegion> repository1 = this._countryRegionRepository;
                    impersonatorTenantId = async.CustomerAddress.CountryRegionId;
                    async1.CountryRegion = await repository1.GetAsync(impersonatorTenantId.Value);
                    async1 = null;
                }
            }
            IRepository<InvoiceTask, long> repository2 = this._invoiceTaskRepository;
            List<InvoiceTask> allListAsync = await repository2.GetAllListAsync((InvoiceTask m) => m.InvoiceId == async.Id);
            List<InvoiceTask> invoiceTasks = allListAsync;
            IRepository<InvoiceAdhocProduct, long> repository3 = this._invoiceAdhocProductRepository;
            List<InvoiceAdhocProduct> invoiceAdhocProducts = await repository3.GetAllListAsync((InvoiceAdhocProduct m) => m.InvoiceId == async.Id);
            List<InvoiceAdhocProduct> invoiceAdhocProducts1 = invoiceAdhocProducts;
            IRepository<InvoiceProduct, long> repository4 = this._invoiceProductRepository;
            List<InvoiceProduct> invoiceProducts = await repository4.GetAllListAsync((InvoiceProduct m) => m.InvoiceId == async.Id);
            List<InvoiceProduct> invoiceProducts1 = invoiceProducts;
            IRepository<InvoiceAdjustment, long> repository5 = this._invoiceAdjustmentRepository;
            List<InvoiceAdjustment> invoiceAdjustments = await repository5.GetAllListAsync((InvoiceAdjustment m) => m.InvoiceId == async.Id);
            List<InvoiceAdjustment> invoiceAdjustments1 = invoiceAdjustments;
            IRepository<InvoicePayment, long> repository6 = this._invoicePaymentRepository;
            List<InvoicePayment> invoicePayments = await repository6.GetAllListAsync((InvoicePayment m) => m.InvoiceId == async.Id);
            List<InvoicePayment> invoicePayments1 = invoicePayments;
            string str2 = await this.GenerateInvoiceHtml(tenant, siteRootAddress, allSettings, customer, address, async, invoiceTasks, invoiceAdhocProducts1, invoiceProducts1, invoiceAdjustments1, invoicePayments1);
            return str2;
        }

        public async Task<string> GetProjectForPrint(long input)
        {
            int value;
            int? impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
            if (impersonatorTenantId.HasValue)
            {
                impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
                value = impersonatorTenantId.Value;
            }
            else
            {
                value = this.AbpSession.GetTenantId();
            }
            int num = value;
            Project async = await this._projectRepository.GetAsync(input);
            if (async == null || async.TenantId != num)
            {
                if (async != null)
                {
                    throw new Exception("SecurityViolation");
                }
                throw new Exception("ProjectIsNull");
            }
            string tenancyName = (await this._tenantManager.GetByIdAsync(num)).TenancyName;
            string str = tenancyName;
            string str1 = tenancyName;
            str1 = str;
            Tenant tenant = await this._tenantManager.FindByTenancyNameAsync(str1);
            string siteRootAddress = this._webUrlService.GetSiteRootAddress(str1);
            impersonatorTenantId = null;
            TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(impersonatorTenantId);
            Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
            Address address = new Address();
            if (async.CustomerAddressId.HasValue)
            {
                IRepository<Address, long> repository = this._addressRepository;
                address = await repository.GetAsync(async.CustomerAddressId.Value);
                impersonatorTenantId = address.CountryRegionId;
                if (impersonatorTenantId.HasValue)
                {
                    Address async1 = address;
                    IRepository<CountryRegion> repository1 = this._countryRegionRepository;
                    impersonatorTenantId = async.CustomerAddress.CountryRegionId;
                    async1.CountryRegion = await repository1.GetAsync(impersonatorTenantId.Value);
                    async1 = null;
                }
            }
            IRepository<ProjectTask, long> repository2 = this._projectTaskRepository;
            List<ProjectTask> allListAsync = await repository2.GetAllListAsync((ProjectTask m) => m.ProjectId == async.Id);
            List<ProjectTask> projectTasks = allListAsync;
            IRepository<ProjectAdhocProduct, long> repository3 = this._projectAdhocProductRepository;
            List<ProjectAdhocProduct> projectAdhocProducts = await repository3.GetAllListAsync((ProjectAdhocProduct m) => m.ProjectId == async.Id);
            List<ProjectAdhocProduct> projectAdhocProducts1 = projectAdhocProducts;
            IRepository<ProjectProduct, long> repository4 = this._projectProductRepository;
            List<ProjectProduct> projectProducts = await repository4.GetAllListAsync((ProjectProduct m) => m.ProjectId == async.Id);
            List<ProjectProduct> projectProducts1 = projectProducts;
            IRepository<ProjectAdjustment, long> repository5 = this._projectAdjustmentRepository;
            List<ProjectAdjustment> projectAdjustments = await repository5.GetAllListAsync((ProjectAdjustment m) => m.ProjectId == async.Id);
            List<ProjectAdjustment> projectAdjustments1 = projectAdjustments;
            string str2 = await this.GenerateProjectHtml(tenant, siteRootAddress, allSettings, customer, address, async, projectTasks, projectAdhocProducts1, projectProducts1, projectAdjustments1);
            return str2;
        }

        public async Task<bool> SendEstimate(long input)
        {
            int value;
            int? impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
            if (impersonatorTenantId.HasValue)
            {
                impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
                value = impersonatorTenantId.Value;
            }
            else
            {
                impersonatorTenantId = this.AbpSession.TenantId;
                value = impersonatorTenantId.Value;
            }
            int num = value;
            Estimate async = await this._estimateRepository.GetAsync(input);
            if (async == null || async.TenantId != num)
            {
                if (async != null)
                {
                    throw new Exception("SecurityViolation");
                }
                throw new Exception("EstimateIsNull");
            }
            Tenant byIdAsync = await this._tenantManager.GetByIdAsync(num);
            string tenancyName = byIdAsync.TenancyName;
            string siteRootAddress = this._webUrlService.GetSiteRootAddress(tenancyName);
            impersonatorTenantId = null;
            TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(impersonatorTenantId);
            Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
            Address address = new Address();
            if (async.CustomerAddressId.HasValue)
            {
                IRepository<Address, long> repository = this._addressRepository;
                address = await repository.GetAsync(async.CustomerAddressId.Value);
                impersonatorTenantId = address.CountryRegionId;
                if (impersonatorTenantId.HasValue)
                {
                    Address async1 = address;
                    IRepository<CountryRegion> repository1 = this._countryRegionRepository;
                    impersonatorTenantId = async.CustomerAddress.CountryRegionId;
                    async1.CountryRegion = await repository1.GetAsync(impersonatorTenantId.Value);
                    async1 = null;
                }
            }
            IRepository<EstimateTask, long> repository2 = this._estimateTaskRepository;
            List<EstimateTask> allListAsync = await repository2.GetAllListAsync((EstimateTask m) => m.EstimateId == async.Id);
            List<EstimateTask> estimateTasks = allListAsync;
            IRepository<EstimateAdhocProduct, long> repository3 = this._estimateAdhocProductRepository;
            List<EstimateAdhocProduct> estimateAdhocProducts = await repository3.GetAllListAsync((EstimateAdhocProduct m) => m.EstimateId == async.Id);
            List<EstimateAdhocProduct> estimateAdhocProducts1 = estimateAdhocProducts;
            IRepository<EstimateProduct, long> repository4 = this._estimateProductRepository;
            List<EstimateProduct> estimateProducts = await repository4.GetAllListAsync((EstimateProduct m) => m.EstimateId == async.Id);
            List<EstimateProduct> estimateProducts1 = estimateProducts;
            IRepository<EstimateAdjustment, long> repository5 = this._estimateAdjustmentRepository;
            List<EstimateAdjustment> estimateAdjustments = await repository5.GetAllListAsync((EstimateAdjustment m) => m.EstimateId == async.Id);
            List<EstimateAdjustment> estimateAdjustments1 = estimateAdjustments;
            string str = await this.GenerateEstimateHtml(byIdAsync, siteRootAddress, allSettings, customer, address, async, estimateTasks, estimateAdhocProducts1, estimateProducts1, estimateAdjustments1);
            string str1 = str;
            await this._emailSender.SendAsync(customer.Email, this.L("EstimateView_SendSubject"), str1, true);
            return true;
        }

        public async Task<bool> SendInvoice(long input)
        {
            int value;
            int? impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
            if (impersonatorTenantId.HasValue)
            {
                impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
                value = impersonatorTenantId.Value;
            }
            else
            {
                impersonatorTenantId = this.AbpSession.TenantId;
                value = impersonatorTenantId.Value;
            }
            int num = value;
            Invoice async = await this._invoiceRepository.GetAsync(input);
            if (async == null || async.TenantId != num)
            {
                if (async != null)
                {
                    throw new Exception("SecurityViolation");
                }
                throw new Exception("InvoiceIsNull");
            }
            Tenant byIdAsync = await this._tenantManager.GetByIdAsync(num);
            string tenancyName = byIdAsync.TenancyName;
            string siteRootAddress = this._webUrlService.GetSiteRootAddress(tenancyName);
            impersonatorTenantId = null;
            TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(impersonatorTenantId);
            Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
            Address address = new Address();
            if (async.CustomerAddressId.HasValue)
            {
                IRepository<Address, long> repository = this._addressRepository;
                address = await repository.GetAsync(async.CustomerAddressId.Value);
                impersonatorTenantId = address.CountryRegionId;
                if (impersonatorTenantId.HasValue)
                {
                    Address async1 = address;
                    IRepository<CountryRegion> repository1 = this._countryRegionRepository;
                    impersonatorTenantId = async.CustomerAddress.CountryRegionId;
                    async1.CountryRegion = await repository1.GetAsync(impersonatorTenantId.Value);
                    async1 = null;
                }
            }
            IRepository<InvoiceTask, long> repository2 = this._invoiceTaskRepository;
            List<InvoiceTask> allListAsync = await repository2.GetAllListAsync((InvoiceTask m) => m.InvoiceId == async.Id);
            List<InvoiceTask> invoiceTasks = allListAsync;
            IRepository<InvoiceAdhocProduct, long> repository3 = this._invoiceAdhocProductRepository;
            List<InvoiceAdhocProduct> invoiceAdhocProducts = await repository3.GetAllListAsync((InvoiceAdhocProduct m) => m.InvoiceId == async.Id);
            List<InvoiceAdhocProduct> invoiceAdhocProducts1 = invoiceAdhocProducts;
            IRepository<InvoiceProduct, long> repository4 = this._invoiceProductRepository;
            List<InvoiceProduct> invoiceProducts = await repository4.GetAllListAsync((InvoiceProduct m) => m.InvoiceId == async.Id);
            List<InvoiceProduct> invoiceProducts1 = invoiceProducts;
            IRepository<InvoiceAdjustment, long> repository5 = this._invoiceAdjustmentRepository;
            List<InvoiceAdjustment> invoiceAdjustments = await repository5.GetAllListAsync((InvoiceAdjustment m) => m.InvoiceId == async.Id);
            List<InvoiceAdjustment> invoiceAdjustments1 = invoiceAdjustments;
            IRepository<InvoicePayment, long> repository6 = this._invoicePaymentRepository;
            List<InvoicePayment> invoicePayments = await repository6.GetAllListAsync((InvoicePayment m) => m.InvoiceId == async.Id);
            List<InvoicePayment> invoicePayments1 = invoicePayments;
            string email = customer.Email;
            string str = this.L("InvoiceView_SendSubject");
            string str1 = await this.GenerateInvoiceHtml(byIdAsync, siteRootAddress, allSettings, customer, address, async, invoiceTasks, invoiceAdhocProducts1, invoiceProducts1, invoiceAdjustments1, invoicePayments1);
            await this._emailSender.SendAsync(email, str, str1, true);
            email = null;
            str = null;
            return true;
        }

        public async Task<bool> SendProject(long input)
        {
            int value;
            int? impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
            if (impersonatorTenantId.HasValue)
            {
                impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
                value = impersonatorTenantId.Value;
            }
            else
            {
                impersonatorTenantId = this.AbpSession.TenantId;
                value = impersonatorTenantId.Value;
            }
            int num = value;
            Project async = await this._projectRepository.GetAsync(input);
            if (async == null || async.TenantId != num)
            {
                if (async != null)
                {
                    throw new Exception("SecurityViolation");
                }
                throw new Exception("ProjectIsNull");
            }
            Tenant byIdAsync = await this._tenantManager.GetByIdAsync(num);
            string tenancyName = byIdAsync.TenancyName;
            string siteRootAddress = this._webUrlService.GetSiteRootAddress(tenancyName);
            impersonatorTenantId = null;
            TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(impersonatorTenantId);
            Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
            Address address = new Address();
            if (async.CustomerAddressId.HasValue)
            {
                IRepository<Address, long> repository = this._addressRepository;
                address = await repository.GetAsync(async.CustomerAddressId.Value);
                impersonatorTenantId = address.CountryRegionId;
                if (impersonatorTenantId.HasValue)
                {
                    Address async1 = address;
                    IRepository<CountryRegion> repository1 = this._countryRegionRepository;
                    impersonatorTenantId = async.CustomerAddress.CountryRegionId;
                    async1.CountryRegion = await repository1.GetAsync(impersonatorTenantId.Value);
                    async1 = null;
                }
            }
            IRepository<ProjectTask, long> repository2 = this._projectTaskRepository;
            List<ProjectTask> allListAsync = await repository2.GetAllListAsync((ProjectTask m) => m.ProjectId == async.Id);
            List<ProjectTask> projectTasks = allListAsync;
            IRepository<ProjectAdhocProduct, long> repository3 = this._projectAdhocProductRepository;
            List<ProjectAdhocProduct> projectAdhocProducts = await repository3.GetAllListAsync((ProjectAdhocProduct m) => m.ProjectId == async.Id);
            List<ProjectAdhocProduct> projectAdhocProducts1 = projectAdhocProducts;
            IRepository<ProjectProduct, long> repository4 = this._projectProductRepository;
            List<ProjectProduct> projectProducts = await repository4.GetAllListAsync((ProjectProduct m) => m.ProjectId == async.Id);
            List<ProjectProduct> projectProducts1 = projectProducts;
            IRepository<ProjectAdjustment, long> repository5 = this._projectAdjustmentRepository;
            List<ProjectAdjustment> projectAdjustments = await repository5.GetAllListAsync((ProjectAdjustment m) => m.ProjectId == async.Id);
            List<ProjectAdjustment> projectAdjustments1 = projectAdjustments;
            string email = customer.Email;
            string str = this.L("ProjectView_SendSubject");
            string str1 = await this.GenerateProjectHtml(byIdAsync, siteRootAddress, allSettings, customer, address, async, projectTasks, projectAdhocProducts1, projectProducts1, projectAdjustments1);
            await this._emailSender.SendAsync(email, str, str1, true);
            email = null;
            str = null;
            return true;
        }

        public async Task<string> ViewEstimateForPDF(long input)
        {
            Estimate async = await this._estimateRepository.GetAsync(input);
            if (async == null)
            {
                throw new Exception("EstimateIsNull");
            }
            int tenantId = async.TenantId;
            string tenancyName = (await this._tenantManager.GetByIdAsync(tenantId)).TenancyName;
            string str = tenancyName;
            string str1 = tenancyName;
            str1 = str;
            Tenant tenant = await this._tenantManager.FindByTenancyNameAsync(str1);
            string siteRootAddress = this._webUrlService.GetSiteRootAddress(str1);
            TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(new int?(tenantId));
            TenantSettingsEditDto tenantSettingsEditDto = allSettings;
            Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
            Address address = new Address();
            if (async.CustomerAddressId.HasValue)
            {
                IRepository<Address, long> repository = this._addressRepository;
                address = await repository.GetAsync(async.CustomerAddressId.Value);
                if (address.CountryRegionId.HasValue)
                {
                    Address async1 = address;
                    IRepository<CountryRegion> repository1 = this._countryRegionRepository;
                    int? countryRegionId = async.CustomerAddress.CountryRegionId;
                    async1.CountryRegion = await repository1.GetAsync(countryRegionId.Value);
                    async1 = null;
                }
            }
            IRepository<EstimateTask, long> repository2 = this._estimateTaskRepository;
            List<EstimateTask> allListAsync = await repository2.GetAllListAsync((EstimateTask m) => m.EstimateId == async.Id);
            List<EstimateTask> estimateTasks = allListAsync;
            IRepository<EstimateAdhocProduct, long> repository3 = this._estimateAdhocProductRepository;
            List<EstimateAdhocProduct> estimateAdhocProducts = await repository3.GetAllListAsync((EstimateAdhocProduct m) => m.EstimateId == async.Id);
            List<EstimateAdhocProduct> estimateAdhocProducts1 = estimateAdhocProducts;
            IRepository<EstimateProduct, long> repository4 = this._estimateProductRepository;
            List<EstimateProduct> estimateProducts = await repository4.GetAllListAsync((EstimateProduct m) => m.EstimateId == async.Id);
            List<EstimateProduct> estimateProducts1 = estimateProducts;
            IRepository<EstimateAdjustment, long> repository5 = this._estimateAdjustmentRepository;
            List<EstimateAdjustment> estimateAdjustments = await repository5.GetAllListAsync((EstimateAdjustment m) => m.EstimateId == async.Id);
            List<EstimateAdjustment> estimateAdjustments1 = estimateAdjustments;
            string str2 = await this.GenerateEstimateHtml(tenant, siteRootAddress, tenantSettingsEditDto, customer, address, async, estimateTasks, estimateAdhocProducts1, estimateProducts1, estimateAdjustments1);
            return str2;
        }

        public async Task<string> ViewInvoiceForPDF(long input)
        {
            Invoice async = await this._invoiceRepository.GetAsync(input);
            if (async == null)
            {
                throw new Exception("InvoiceIsNull");
            }
            int tenantId = async.TenantId;
            string tenancyName = (await this._tenantManager.GetByIdAsync(tenantId)).TenancyName;
            string str = tenancyName;
            string str1 = tenancyName;
            str1 = str;
            Tenant tenant = await this._tenantManager.FindByTenancyNameAsync(str1);
            string siteRootAddress = this._webUrlService.GetSiteRootAddress(str1);
            TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(new int?(tenantId));
            TenantSettingsEditDto tenantSettingsEditDto = allSettings;
            Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
            Address address = new Address();
            if (async.CustomerAddressId.HasValue)
            {
                IRepository<Address, long> repository = this._addressRepository;
                address = await repository.GetAsync(async.CustomerAddressId.Value);
                if (address.CountryRegionId.HasValue)
                {
                    Address async1 = address;
                    IRepository<CountryRegion> repository1 = this._countryRegionRepository;
                    int? countryRegionId = async.CustomerAddress.CountryRegionId;
                    async1.CountryRegion = await repository1.GetAsync(countryRegionId.Value);
                    async1 = null;
                }
            }
            IRepository<InvoiceTask, long> repository2 = this._invoiceTaskRepository;
            List<InvoiceTask> allListAsync = await repository2.GetAllListAsync((InvoiceTask m) => m.InvoiceId == async.Id);
            List<InvoiceTask> invoiceTasks = allListAsync;
            IRepository<InvoiceAdhocProduct, long> repository3 = this._invoiceAdhocProductRepository;
            List<InvoiceAdhocProduct> invoiceAdhocProducts = await repository3.GetAllListAsync((InvoiceAdhocProduct m) => m.InvoiceId == async.Id);
            List<InvoiceAdhocProduct> invoiceAdhocProducts1 = invoiceAdhocProducts;
            IRepository<InvoiceProduct, long> repository4 = this._invoiceProductRepository;
            List<InvoiceProduct> invoiceProducts = await repository4.GetAllListAsync((InvoiceProduct m) => m.InvoiceId == async.Id);
            List<InvoiceProduct> invoiceProducts1 = invoiceProducts;
            IRepository<InvoiceAdjustment, long> repository5 = this._invoiceAdjustmentRepository;
            List<InvoiceAdjustment> invoiceAdjustments = await repository5.GetAllListAsync((InvoiceAdjustment m) => m.InvoiceId == async.Id);
            List<InvoiceAdjustment> invoiceAdjustments1 = invoiceAdjustments;
            IRepository<InvoicePayment, long> repository6 = this._invoicePaymentRepository;
            List<InvoicePayment> invoicePayments = await repository6.GetAllListAsync((InvoicePayment m) => m.InvoiceId == async.Id);
            List<InvoicePayment> invoicePayments1 = invoicePayments;
            string str2 = await this.GenerateInvoiceHtml(tenant, siteRootAddress, tenantSettingsEditDto, customer, address, async, invoiceTasks, invoiceAdhocProducts1, invoiceProducts1, invoiceAdjustments1, invoicePayments1);
            return str2;
        }

        public async Task<string> ViewProjectForPDF(long input)
        {
            Project async = await this._projectRepository.GetAsync(input);
            if (async == null)
            {
                throw new Exception("ProjectIsNull");
            }
            int tenantId = async.TenantId;
            string tenancyName = (await this._tenantManager.GetByIdAsync(tenantId)).TenancyName;
            string str = tenancyName;
            string str1 = tenancyName;
            str1 = str;
            Tenant tenant = await this._tenantManager.FindByTenancyNameAsync(str1);
            string siteRootAddress = this._webUrlService.GetSiteRootAddress(str1);
            TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(new int?(tenantId));
            TenantSettingsEditDto tenantSettingsEditDto = allSettings;
            Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
            Address address = new Address();
            if (async.CustomerAddressId.HasValue)
            {
                IRepository<Address, long> repository = this._addressRepository;
                address = await repository.GetAsync(async.CustomerAddressId.Value);
                if (address.CountryRegionId.HasValue)
                {
                    Address async1 = address;
                    IRepository<CountryRegion> repository1 = this._countryRegionRepository;
                    int? countryRegionId = async.CustomerAddress.CountryRegionId;
                    async1.CountryRegion = await repository1.GetAsync(countryRegionId.Value);
                    async1 = null;
                }
            }
            IRepository<ProjectTask, long> repository2 = this._projectTaskRepository;
            List<ProjectTask> allListAsync = await repository2.GetAllListAsync((ProjectTask m) => m.ProjectId == async.Id);
            List<ProjectTask> projectTasks = allListAsync;
            IRepository<ProjectAdhocProduct, long> repository3 = this._projectAdhocProductRepository;
            List<ProjectAdhocProduct> projectAdhocProducts = await repository3.GetAllListAsync((ProjectAdhocProduct m) => m.ProjectId == async.Id);
            List<ProjectAdhocProduct> projectAdhocProducts1 = projectAdhocProducts;
            IRepository<ProjectProduct, long> repository4 = this._projectProductRepository;
            List<ProjectProduct> projectProducts = await repository4.GetAllListAsync((ProjectProduct m) => m.ProjectId == async.Id);
            List<ProjectProduct> projectProducts1 = projectProducts;
            IRepository<ProjectAdjustment, long> repository5 = this._projectAdjustmentRepository;
            List<ProjectAdjustment> projectAdjustments = await repository5.GetAllListAsync((ProjectAdjustment m) => m.ProjectId == async.Id);
            List<ProjectAdjustment> projectAdjustments1 = projectAdjustments;
            string str2 = await this.GenerateProjectHtml(tenant, siteRootAddress, tenantSettingsEditDto, customer, address, async, projectTasks, projectAdhocProducts1, projectProducts1, projectAdjustments1);
            return str2;
        }
    }
}