using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using Castle.Core.Logging;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Configuration.Tenants.Dto;
using FuelWerx.Customers;
using FuelWerx.Customers.Dto;
using FuelWerx.EntityFramework;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.Invoices;
using FuelWerx.MultiTenancy;
using FuelWerx.Pay.Payeezy;
using FuelWerx.Web;
using FuelWerx.Web.Models.Payments.Payeezy;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class PayController : FuelWerxControllerBase
	{
		private readonly TenantSettingsAppService _tenantSettingsAppService;

		private readonly TenantManager _tenantManager;

		private readonly IWebUrlService _webUrlService;

		private readonly IInvoiceAppService _invoiceAppService;

		private readonly IPayAppService _paymentAppService;

		private readonly ICustomerAppService _customerAppService;

		private readonly IGenericAppService _genericAppService;

		private readonly UserManager _userManager;

		private readonly IUnitOfWorkManager _unitOfWorkManager;

		public PayController(TenantSettingsAppService tenantSettingsAppService, IPayAppService paymentAppService, IInvoiceAppService invoiceAppService, ICustomerAppService customerAppService, IGenericAppService genericAppService, TenantManager tenantManager, IWebUrlService webUrlService, UserManager userManager, IUnitOfWorkManager unitOfWorkManager)
		{
			this._tenantSettingsAppService = tenantSettingsAppService;
			this._tenantManager = tenantManager;
			this._webUrlService = webUrlService;
			this._paymentAppService = paymentAppService;
			this._invoiceAppService = invoiceAppService;
			this._customerAppService = customerAppService;
			this._genericAppService = genericAppService;
			this._userManager = userManager;
			this._unitOfWorkManager = unitOfWorkManager;
		}

		private static string GeneratePayeezyHash(string x_transaction_key, string x_login, decimal x_amount, string x_fp_sequence = "", string x_fp_timestamp = "", string x_currency = "USD")
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (string.IsNullOrEmpty(x_fp_sequence))
			{
				int num = (new Random()).Next(0, 1000);
				x_fp_sequence = num.ToString();
			}
			if (string.IsNullOrEmpty(x_fp_timestamp))
			{
				TimeSpan utcNow = DateTime.UtcNow - new DateTime(1970, 1, 1);
				x_fp_timestamp = ((int)utcNow.TotalSeconds).ToString();
			}
			if (string.IsNullOrEmpty(x_currency))
			{
				x_currency = "USD";
			}
			stringBuilder.Append(x_login).Append("^").Append(x_fp_sequence).Append("^").Append(x_fp_timestamp).Append("^").Append(x_amount.ToString()).Append("^").Append("");
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			byte[] numArray = Encoding.UTF8.GetBytes(x_transaction_key);
			byte[] numArray1 = (new HMACMD5(numArray)).ComputeHash(bytes);
			return BitConverter.ToString(numArray1).Replace("-", "").ToLower();
		}

		private static string GetIPAddress(HttpRequestBase request)
		{
			string item;
			try
			{
				item = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
				if (string.IsNullOrEmpty(item))
				{
					item = request.UserHostAddress;
				}
				else if (item.IndexOf(",") > 0)
				{
					string[] strArrays = item.Split(new char[] { ',' });
					item = strArrays[(int)strArrays.Length - 1];
				}
			}
			catch
			{
				item = null;
			}
			return item;
		}

		[HttpPost]
		public async Task<ActionResult> Payeezy(FormCollection formFields)
		{
			int value;
			string str;
			Guid guid;
			string str1;
			bool flag;
			PayNowDto payNowDto = new PayNowDto();
			try
			{
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
				TenantSettingsEditDto allSettingsByTenantId = await this._tenantSettingsAppService.GetAllSettingsByTenantId(num);
				long num1 = long.Parse(formFields["invoiceId"]);
				decimal.Parse(formFields["invoiceAmount"]);
				decimal num2 = decimal.Parse(formFields["paymentAmount"]);
				Invoice invoice = await this._invoiceAppService.GetInvoice(num1);
				if (invoice == null || invoice.TenantId != num)
				{
					if (invoice != null)
					{
						throw new Exception("SecurityViolation");
					}
					throw new Exception("InvoiceIsNull");
				}
				payNowDto.x_invoice_num = invoice.Number;
				payNowDto.x_po_num = invoice.PONumber;
				payNowDto.x_reference_3 = invoice.Number;
				ICustomerAppService customerAppService = this._customerAppService;
				NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
				{
					Id = new long?(invoice.CustomerId)
				};
				GetCustomerForEditOutput customerForEdit = await customerAppService.GetCustomerForEdit(nullableIdInput);
				PayNowDto payNowDto1 = payNowDto;
				string[] strArrays = new string[7];
				long id = invoice.Id;
				strArrays[0] = id.ToString();
				strArrays[1] = "|";
				long? impersonatorUserId = customerForEdit.Customer.Id;
				id = impersonatorUserId.Value;
				strArrays[2] = id.ToString();
				strArrays[3] = "|";
				strArrays[4] = num.ToString();
				strArrays[5] = "|";
				impersonatorUserId = this.AbpSession.ImpersonatorUserId;
				if (impersonatorUserId.HasValue)
				{
					impersonatorUserId = this.AbpSession.ImpersonatorUserId;
					id = impersonatorUserId.Value;
					str = id.ToString();
				}
				else
				{
					impersonatorUserId = this.AbpSession.UserId;
					if (impersonatorUserId.HasValue)
					{
						impersonatorUserId = this.AbpSession.UserId;
						id = impersonatorUserId.Value;
						str = id.ToString();
					}
					else
					{
						impersonatorUserId = this.AbpSession.UserId;
						str = impersonatorUserId.ToString();
					}
				}
				strArrays[6] = str;
				payNowDto1.x_cust_id = string.Concat(strArrays);
				payNowDto.x_email = customerForEdit.Customer.Email;
				if (customerForEdit.Customer.BusinessName != null && customerForEdit.Customer.BusinessName.ToString().Length > 0)
				{
					payNowDto.x_company = customerForEdit.Customer.BusinessName;
				}
				if (customerForEdit.Customer.FirstName != null && customerForEdit.Customer.FirstName.ToString().Length > 0)
				{
					payNowDto.x_first_name = customerForEdit.Customer.FirstName.ToString();
				}
				if (customerForEdit.Customer.LastName != null && customerForEdit.Customer.LastName.ToString().Length > 0)
				{
					payNowDto.x_last_name = customerForEdit.Customer.LastName.ToString();
				}
				PayNowDto str2 = payNowDto;
				impersonatorUserId = customerForEdit.Customer.Id;
				id = impersonatorUserId.Value;
				str2.x_customer_tax_id = id.ToString();
				impersonatorUserId = invoice.CustomerAddressId;
				if (impersonatorUserId.HasValue)
				{
					impersonatorUserId = invoice.CustomerAddressId;
					if (impersonatorUserId.Value > (long)0)
					{
						IGenericAppService genericAppService = this._genericAppService;
						GetAddressesInput getAddressesInput = new GetAddressesInput();
						impersonatorUserId = customerForEdit.Customer.Id;
						getAddressesInput.OwnerId = new long?(impersonatorUserId.Value);
						getAddressesInput.OwnerType = "Customer";
						PagedResultOutput<AddressListDto> addresses = await genericAppService.GetAddresses(getAddressesInput);
						int num3 = 0;
						while (num3 < addresses.Items.Count)
						{
							long id1 = (long)addresses.Items[num3].Id;
							impersonatorUserId = invoice.CustomerAddressId;
							flag = (id1 == impersonatorUserId.GetValueOrDefault() ? impersonatorUserId.HasValue : false);
							if (!flag)
							{
								num3++;
							}
							else
							{
								payNowDto.x_address = addresses.Items[num3].PrimaryAddress;
								payNowDto.x_city = addresses.Items[num3].City;
								payNowDto.x_zip = addresses.Items[num3].PostalCode;
								impersonatorTenantId = addresses.Items[num3].CountryRegionId;
								if (!impersonatorTenantId.HasValue)
								{
									break;
								}
								IGenericAppService genericAppService1 = this._genericAppService;
								impersonatorTenantId = addresses.Items[num3].CountryRegionId;
								int? nullable = new int?(impersonatorTenantId.Value);
								impersonatorTenantId = null;
								ListResultOutput<CountryRegionInCountryListDto> countryRegions = genericAppService1.GetCountryRegions(nullable, impersonatorTenantId);
								if (countryRegions.Items.Count != 1)
								{
									break;
								}
								payNowDto.x_state = countryRegions.Items[0].Code;
								break;
							}
						}
					}
				}
				Tenant byIdAsync = await this._tenantManager.GetByIdAsync(num);
				string tenancyName = byIdAsync.TenancyName;
				string str3 = tenancyName;
				string str4 = tenancyName;
				str4 = str3;
				byIdAsync = await this._tenantManager.FindByTenancyNameAsync(str4);
				string siteRootAddress = this._webUrlService.GetSiteRootAddress(str4);
				PayNowDto payNowDto2 = payNowDto;
				object[] objArray = new object[] { siteRootAddress, "Mpa/Settings/GetLogoById?logoId=", null, null, null };
				guid = (allSettingsByTenantId.Logo.InvoiceImageId.HasValue ? allSettingsByTenantId.Logo.InvoiceImageId.Value : Guid.Empty);
				objArray[2] = guid;
				objArray[3] = "&logoType=header&viewContrast=light&t=";
				id = Clock.Now.Ticks;
				objArray[4] = id.ToString();
				payNowDto2.x_logo_url = string.Concat(objArray);
				payNowDto.x_receipt_link_url = string.Concat(siteRootAddress, "Pay/PayeezyResponse");
				payNowDto.x_receipt_link_method = "AUTO-POST";
				payNowDto.x_receipt_link_text = this.L("CompleteTransaction");
				if (allSettingsByTenantId.PaymentGatewaySettings.GatewaySettings.Length <= 3)
				{
					throw new Exception("PaymentGatewayError_PayEezySettingsMissing");
				}
				PayEezyJsonObject payEezyJsonObject = JsonConvert.DeserializeObject<PayEezyJsonObject>(allSettingsByTenantId.PaymentGatewaySettings.GatewaySettings);
				payNowDto.x_login = payEezyJsonObject.PayEezy_x_login;
				payNowDto.x_transaction_key = payEezyJsonObject.PayEezy_x_transaction_key;
				PayNowDto payNowDto3 = payNowDto;
				bool? payEezyXTestRequest = payEezyJsonObject.PayEezy_x_test_request;
				payNowDto3.x_test_request = bool.Parse(payEezyXTestRequest.ToString());
				PayNowDto payNowDto4 = payNowDto;
				payEezyXTestRequest = payEezyJsonObject.PayEezy_x_email_customer;
				payNowDto4.x_email_customer = bool.Parse(payEezyXTestRequest.ToString());
				payNowDto.x_gateway_id = payEezyJsonObject.PayEezy_x_gateway_id;
				PayNowDto payNowDto5 = payNowDto;
				string payEezyXDescription = payEezyJsonObject.PayEezy_x_description;
				str1 = (formFields["Description"] == null || formFields["Description"] != null && formFields["Description"].ToString().Length > 0 ? string.Concat(" ", formFields["Description"].ToString()) : "");
				payNowDto5.x_description = string.Concat(payEezyXDescription, str1);
				payNowDto.x_amount = num2;
				payNowDto.x_customer_ip = PayController.GetIPAddress(this.Request);
				Random random = new Random();
				PayNowDto str5 = payNowDto;
				int num4 = random.Next(0, 1000);
				str5.x_fp_sequence = num4.ToString();
				TimeSpan utcNow = DateTime.UtcNow - new DateTime(1970, 1, 1);
				payNowDto.x_fp_timestamp = ((int)utcNow.TotalSeconds).ToString();
				payNowDto.x_fp_hash = PayController.GeneratePayeezyHash(payNowDto.x_transaction_key, payNowDto.x_login, payNowDto.x_amount, payNowDto.x_fp_sequence, payNowDto.x_fp_timestamp, "USD");
				if (!payNowDto.x_test_request)
				{
					payNowDto.PostToUrl = "https://checkout.globalgatewaye4.firstdata.com/payment";
				}
				else
				{
					payNowDto.PostToUrl = "https://demo.globalgatewaye4.firstdata.com/payment";
				}
				allSettingsByTenantId = null;
				invoice = null;
				str4 = null;
			}
			catch (Exception)
			{
				payNowDto = new PayNowDto();
				((dynamic)this.ViewBag).Error_InvalidParameters = true;
			}
			return this.View(payNowDto);
		}

		[HttpGet]
		public async Task<ActionResult> PayeezyComplete(long newId)
		{
			return this.View();
		}

		[HttpPost]
		public async Task<ActionResult> PayeezyResponse(FormCollection formFields)
		{
			ActionResult actionResult;
			int? impersonatorTenantId;
			long? impersonatorUserId;
			int value;
			long num;
			bool item = formFields["isRecordPayment"] != null;
			int num1 = 0;
			int num2 = 0;
			int num3 = 0;
			long num4 = (long)0;
			if (!item)
			{
				string str = formFields["x_cust_id"].ToString();
				string[] strArrays = str.Split(new char[] { '|' });
				num1 = int.Parse(strArrays[0].ToString());
				num2 = int.Parse(strArrays[1].ToString());
				num3 = int.Parse(strArrays[2].ToString());
				num4 = long.Parse(strArrays[3].ToString());
			}
			else
			{
				num1 = int.Parse(formFields["invoiceId"].ToString());
				num2 = int.Parse(formFields["x_cust_id"].ToString());
				if (this.AbpSession.ImpersonatorTenantId.HasValue)
				{
					impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
					value = impersonatorTenantId.Value;
				}
				else
				{
					impersonatorTenantId = this.AbpSession.TenantId;
					value = impersonatorTenantId.Value;
				}
				num3 = value;
				if (this.AbpSession.ImpersonatorUserId.HasValue)
				{
					impersonatorUserId = this.AbpSession.ImpersonatorUserId;
					num = impersonatorUserId.Value;
				}
				else
				{
					impersonatorUserId = this.AbpSession.UserId;
					num = impersonatorUserId.Value;
				}
				num4 = num;
			}
			Dictionary<string, string> strs = new Dictionary<string, string>();
			foreach (object key in formFields.Keys)
			{
				strs.Add(key.ToString(), formFields[key.ToString()]);
			}
			InvoicePayment invoicePayment = new InvoicePayment()
			{
				Id = (long)0,
				TenantId = num3,
				InvoiceId = (long)num1,
				CustomerId = (long)num2,
				X_Response_Code = strs["x_response_code"].ToString(),
				X_Response_Reason_Code = strs["x_response_reason_code"].ToString(),
				X_Response_Reason_Text = strs["x_response_reason_text"].ToString(),
				X_Auth_Code = strs["x_auth_code"].ToString(),
				X_Trans_Id = strs["x_trans_id"].ToString(),
				X_Description = strs["x_description"].ToString(),
				DollarAmount = decimal.Parse(strs["x_amount"].ToString()),
				TransactionDateTime = DateTime.Now,
				P_Authorization_Num = strs["Authorization_Num"].ToString(),
				P_Bank_Resp_Code = strs["Bank_Resp_Code"].ToString(),
				P_Bank_Message = strs["Bank_Message"].ToString(),
				P_Customer_Ref = strs["Customer_Ref"].ToString(),
				P_Exact_Ctr = strs["exact_ctr"].ToString(),
				P_EXact_Message = strs["EXact_Message"].ToString(),
				P_ResponseObject = JsonConvert.SerializeObject(strs, Formatting.Indented)
			};
			InvoicePayment invoicePayment1 = invoicePayment;
			((dynamic)this.ViewBag).NewId = 0;
			decimal num5 = new decimal(0);
			using (CustomDbContext customDbContext = new CustomDbContext())
			{
				try
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("INSERT INTO [dbo].[FuelWerxInvoicePayments]");
					stringBuilder.AppendLine("           ([TenantId]");
					stringBuilder.AppendLine("           ,[InvoiceId]");
					stringBuilder.AppendLine("           ,[CustomerId]");
					stringBuilder.AppendLine("           ,[X_Response_Code]");
					stringBuilder.AppendLine("           ,[X_Response_Reason_Code]");
					stringBuilder.AppendLine("           ,[X_Response_Reason_Text]");
					stringBuilder.AppendLine("           ,[X_Auth_Code]");
					stringBuilder.AppendLine("           ,[X_Trans_Id]");
					stringBuilder.AppendLine("           ,[X_Description]");
					stringBuilder.AppendLine("           ,[P_Exact_Ctr]");
					stringBuilder.AppendLine("           ,[P_Authorization_Num]");
					stringBuilder.AppendLine("           ,[P_Customer_Ref]");
					stringBuilder.AppendLine("           ,[P_Bank_Resp_Code]");
					stringBuilder.AppendLine("           ,[P_Bank_Message]");
					stringBuilder.AppendLine("           ,[P_EXact_Message]");
					stringBuilder.AppendLine("           ,[P_ResponseObject]");
					stringBuilder.AppendLine("           ,[TransactionDateTime]");
					stringBuilder.AppendLine("           ,[DollarAmount]");
					stringBuilder.AppendLine("           ,[ExportedToReporting]");
					stringBuilder.AppendLine("           ,[IsDeleted]");
					stringBuilder.AppendLine("           ,[DeleterUserId]");
					stringBuilder.AppendLine("           ,[DeletionTime]");
					stringBuilder.AppendLine("           ,[LastModificationTime]");
					stringBuilder.AppendLine("           ,[LastModifierUserId]");
					stringBuilder.AppendLine("           ,[CreationTime]");
					stringBuilder.AppendLine("           ,[CreatorUserId])");
					stringBuilder.AppendLine("     VALUES");
					stringBuilder.AppendLine("           (@p0");
					stringBuilder.AppendLine("           ,@p1");
					stringBuilder.AppendLine("           ,@p2");
					stringBuilder.AppendLine("           ,@p3");
					stringBuilder.AppendLine("           ,@p4");
					stringBuilder.AppendLine("           ,@p5");
					stringBuilder.AppendLine("           ,@p6");
					stringBuilder.AppendLine("           ,@p7");
					stringBuilder.AppendLine("           ,@p8");
					stringBuilder.AppendLine("           ,@p9");
					stringBuilder.AppendLine("           ,@p10");
					stringBuilder.AppendLine("           ,@p11");
					stringBuilder.AppendLine("           ,@p12");
					stringBuilder.AppendLine("           ,@p13");
					stringBuilder.AppendLine("           ,@p14");
					stringBuilder.AppendLine("           ,@p15");
					stringBuilder.AppendLine("           ,@p16");
					stringBuilder.AppendLine("           ,@p17");
					stringBuilder.AppendLine("           ,0");
					stringBuilder.AppendLine("           ,0");
					stringBuilder.AppendLine("           ,null");
					stringBuilder.AppendLine("           ,null");
					stringBuilder.AppendLine("           ,null");
					stringBuilder.AppendLine("           ,null");
					stringBuilder.AppendLine("           ,GETDATE()");
					stringBuilder.AppendLine("           ,@p18);");
					stringBuilder.AppendLine("SELECT CAST(SCOPE_IDENTITY() AS BIGINT)[NewId];");
					Database database = customDbContext.Database;
					string str1 = stringBuilder.ToString();
					object[] tenantId = new object[] { invoicePayment1.TenantId, invoicePayment1.InvoiceId, invoicePayment1.CustomerId, invoicePayment1.X_Response_Code, invoicePayment1.X_Response_Reason_Code, invoicePayment1.X_Response_Reason_Text, invoicePayment1.X_Auth_Code, invoicePayment1.X_Trans_Id, invoicePayment1.X_Description, invoicePayment1.P_Exact_Ctr, invoicePayment1.P_Authorization_Num, invoicePayment1.P_Customer_Ref, invoicePayment1.P_Bank_Resp_Code, invoicePayment1.P_Bank_Message, invoicePayment1.P_EXact_Message, invoicePayment1.P_ResponseObject, invoicePayment1.TransactionDateTime, null, null };
					decimal dollarAmount = invoicePayment1.DollarAmount;
					tenantId[17] = decimal.Parse(dollarAmount.ToString());
					tenantId[18] = num4;
					long num6 = await database.SqlQuery<long>(str1, tenantId).SingleAsync();
					long num7 = num6;
					await customDbContext.SaveChangesAsync();
					((dynamic)this.ViewBag).NewId = num7;
					string str2 = "SELECT LineTotal FROM FuelWerxInvoices WHERE Id = @p0";
					Database database1 = customDbContext.Database;
					string str3 = str2.ToString();
					object[] invoiceId = new object[] { invoicePayment1.InvoiceId };
					dollarAmount = await database1.SqlQuery<decimal>(str3, invoiceId).SingleAsync();
					decimal num8 = dollarAmount;
					string str4 = "UPDATE FuelWerxInvoices SET PaidTotal = (SELECT SUM(DollarAmount) FROM FuelWerxInvoicePayments WHERE InvoiceId = @p0) WHERE Id = @p0; SELECT PaidTotal FROM FuelWerxInvoices WHERE Id = @p0;";
					Database database2 = customDbContext.Database;
					string str5 = str4.ToString();
					object[] objArray = new object[] { invoicePayment1.InvoiceId };
					dollarAmount = await database2.SqlQuery<decimal>(str5, objArray).SingleAsync();
					decimal num9 = dollarAmount;
					if (num8 < num9)
					{
						num5 = num9 - num8;
						this.Logger.Debug(string.Concat("CreateInvoiceForOverageAmount[] invoiceId:overage = ", num1.ToString(), ":", num5.ToString()));
						StringBuilder stringBuilder1 = new StringBuilder();
						stringBuilder1.AppendLine("DECLARE @newId As BIGINT = 0;");
						stringBuilder1.AppendLine("INSERT INTO [dbo].[FuelWerxInvoices]");
						stringBuilder1.AppendLine("           ([TenantId]");
						stringBuilder1.AppendLine("           ,[CustomerId]");
						stringBuilder1.AppendLine("           ,[Number]");
						stringBuilder1.AppendLine("           ,[Date]");
						stringBuilder1.AppendLine("           ,[PONumber]");
						stringBuilder1.AppendLine("           ,[Discount]");
						stringBuilder1.AppendLine("           ,[Rate]");
						stringBuilder1.AppendLine("           ,[Hours]");
						stringBuilder1.AppendLine("           ,[Tax]");
						stringBuilder1.AppendLine("           ,[LineTotal]");
						stringBuilder1.AppendLine("           ,[Terms]");
						stringBuilder1.AppendLine("           ,[CurrentStatus]");
						stringBuilder1.AppendLine("           ,[TimeEntryLog]");
						stringBuilder1.AppendLine("           ,[Description]");
						stringBuilder1.AppendLine("           ,[IsActive]");
						stringBuilder1.AppendLine("           ,[ProjectId]");
						stringBuilder1.AppendLine("           ,[CreationTime]");
						stringBuilder1.AppendLine("           ,[CreatorUserId]");
						stringBuilder1.AppendLine("           ,[CustomerAddressId]");
						stringBuilder1.AppendLine("           ,[Label]");
						stringBuilder1.AppendLine("           ,[BillingType]");
						stringBuilder1.AppendLine("           ,[DueDateDiscountExpirationDate]");
						stringBuilder1.AppendLine("           ,[DueDateDiscountTotal]");
						stringBuilder1.AppendLine("           ,[DueDate]");
						stringBuilder1.AppendLine("           ,[Upcharge]");
						stringBuilder1.AppendLine("           ,[GroupDiscount]");
						stringBuilder1.AppendLine("           ,[HoursActual]");
						stringBuilder1.AppendLine("           ,[TermType]");
						stringBuilder1.AppendLine("           ,[LogDataAndTasksVisibleToCustomer]");
						stringBuilder1.AppendLine("           ,[EmergencyDeliveryFee]");
						stringBuilder1.AppendLine("           ,[IsDeleted]");
						stringBuilder1.AppendLine("           ,[IncludeEmergencyDeliveryFee])");
						stringBuilder1.AppendLine("       SELECT [TenantId]");
						stringBuilder1.AppendLine("           ,[CustomerId]");
						stringBuilder1.AppendLine("           ,[Number]");
						stringBuilder1.AppendLine("           ,CONVERT(date, GETDATE())");
						stringBuilder1.AppendLine("           ,[PONumber]");
						stringBuilder1.AppendLine("           ,0");
						stringBuilder1.AppendLine("           ,[Rate]");
						stringBuilder1.AppendLine("           ,0");
						stringBuilder1.AppendLine("           ,0");
						stringBuilder1.AppendLine(string.Concat("           ,-", num5.ToString()));
						stringBuilder1.AppendLine("           ,[Terms]");
						stringBuilder1.AppendLine(string.Concat("           ,'", this.L("InvoiceOverageNewInvoiceDefaultStatus").Replace("'", "''"), "'"));
						stringBuilder1.AppendLine("           ,''");
						stringBuilder1.AppendLine("           ,[Description]");
						stringBuilder1.AppendLine("           ,1");
						stringBuilder1.AppendLine("           ,null");
						stringBuilder1.AppendLine("           ,GETDATE()");
						stringBuilder1.AppendLine("           ,[CreatorUserId]");
						stringBuilder1.AppendLine("           ,[CustomerAddressId]");
						stringBuilder1.AppendLine("           ,[Label]");
						stringBuilder1.AppendLine("           ,[BillingType]");
						stringBuilder1.AppendLine("           ,null");
						stringBuilder1.AppendLine("           ,null");
						stringBuilder1.AppendLine("           ,CONVERT(date, DATEADD(m, 1, GETDATE()))");
						stringBuilder1.AppendLine("           ,0");
						stringBuilder1.AppendLine("           ,0");
						stringBuilder1.AppendLine("           ,0");
						stringBuilder1.AppendLine("           ,[TermType]");
						stringBuilder1.AppendLine("           ,[LogDataAndTasksVisibleToCustomer]");
						stringBuilder1.AppendLine("           ,0");
						stringBuilder1.AppendLine("           ,0");
						stringBuilder1.AppendLine("           ,[IncludeEmergencyDeliveryFee]");
						stringBuilder1.AppendLine("       FROM [dbo].[FuelWerxInvoices]");
						stringBuilder1.AppendLine("       WHERE ([Id] = @p0);");
						stringBuilder1.AppendLine("SET @newId = (SELECT CAST(SCOPE_IDENTITY() AS BIGINT)[NewId]);");
						stringBuilder1.AppendLine("IF @newId > 0");
						stringBuilder1.AppendLine("BEGIN");
						stringBuilder1.AppendLine("        INSERT INTO [dbo].[FuelWerxInvoiceAdjustments]");
						stringBuilder1.AppendLine("           (");
						stringBuilder1.AppendLine("               [InvoiceId]");
						stringBuilder1.AppendLine("               ,[Name]");
						stringBuilder1.AppendLine("               ,[Cost]");
						stringBuilder1.AppendLine("               ,[RetailCost]");
						stringBuilder1.AppendLine("               ,[Description]");
						stringBuilder1.AppendLine("               ,[IsTaxable]");
						stringBuilder1.AppendLine("               ,[IsActive]");
						stringBuilder1.AppendLine("               ,[CreationTime]");
						stringBuilder1.AppendLine("               ,[CreatorUserId]");
						stringBuilder1.AppendLine("               ,[IsDeleted]");
						stringBuilder1.AppendLine("           )");
						stringBuilder1.AppendLine("           VALUES");
						stringBuilder1.AppendLine("           (");
						stringBuilder1.AppendLine("                @newId");
						stringBuilder1.AppendLine(string.Concat("               ,'", this.L("InvoiceOverageNewInvoiceDefaultAdjustmentName").Replace("'", "''"), "'"));
						stringBuilder1.AppendLine(string.Concat("               ,-", num5.ToString()));
						stringBuilder1.AppendLine(string.Concat("               ,-", num5.ToString()));
						stringBuilder1.AppendLine(string.Concat("               ,'", this.L("InvoiceOverageNewInvoiceDefaultAdjustmentDescription").Replace("'", "''"), "'"));
						stringBuilder1.AppendLine("               ,1");
						stringBuilder1.AppendLine("               ,1");
						stringBuilder1.AppendLine("               ,GETDATE()");
						stringBuilder1.AppendLine("               ,(SELECT TOP 1 [CreatorUserId] FROM [dbo].[FuelWerxInvoices] WHERE [Id] = @newId)");
						stringBuilder1.AppendLine("               ,0");
						stringBuilder1.AppendLine("           );");
						stringBuilder1.AppendLine("END");
						stringBuilder1.AppendLine("SELECT @newId;");
						Database database3 = customDbContext.Database;
						string str6 = stringBuilder1.ToString();
						object[] invoiceId1 = new object[] { invoicePayment1.InvoiceId };
						num6 = await database3.SqlQuery<long>(str6, invoiceId1).SingleAsync();
						long num10 = num6;
						this.Logger.Debug(string.Concat("CreateInvoiceForOverageAmount[] newInvoiceId = ", num10.ToString()));
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					((dynamic)this.ViewBag).NewId = 0;
					throw new UserFriendlyException(string.Concat(this.L("ManualPaymentCreationFailed"), exception.Message.ToString()));
				}
			}
			if (!item)
			{
				actionResult = this.View();
			}
			else
			{
				actionResult = this.Json(new MvcAjaxResponse());
			}
			return actionResult;
		}
	}
}