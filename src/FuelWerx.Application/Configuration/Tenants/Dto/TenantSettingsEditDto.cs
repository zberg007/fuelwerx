using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using FuelWerx.Configuration.Host.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	public class TenantSettingsEditDto : IDoubleWayDto, IInputDto, IDto, IValidate, IOutputDto
	{
		public TenantCustomerServiceEditDto CustomerService
		{
			get;
			set;
		}

		public TenantDateTimeSettingsEditDto DateTimeSettings
		{
			get;
			set;
		}

		public TenantDetailEditDto Details
		{
			get;
			set;
		}

		public EmailSettingsEditDto Email
		{
			get;
			set;
		}

		public GeneralSettingsEditDto General
		{
			get;
			set;
		}

		public TenantHoursEditDto Hours
		{
			get;
			set;
		}

		public LdapSettingsEditDto Ldap
		{
			get;
			set;
		}

		public TenantLogosEditDto Logo
		{
			get;
			set;
		}

		public TenantNotificationsEditDto Notifications
		{
			get;
			set;
		}

		public TenantPaymentGatewaySettingsEditDto PaymentGatewaySettings
		{
			get;
			set;
		}

		public TenantPaymentSettingsEditDto PaymentSettings
		{
			get;
			set;
		}

		[Required]
		public TenantUserManagementSettingsEditDto UserManagement
		{
			get;
			set;
		}

		public TenantSettingsEditDto()
		{
		}

		private bool IsValidEmail(string email)
		{
			bool address;
			try
			{
				address = (new MailAddress(email)).Address == email;
			}
			catch
			{
				address = false;
			}
			return address;
		}

		public void ValidateEmailLists()
		{
			string[] strArrays;
			int i;
			List<ValidationResult> validationResults = new List<ValidationResult>();
			char[] chrArray = new char[] { ',', ';' };
			if (this.Notifications.NewOrderEmails.Length > 0)
			{
				string str = this.Notifications.NewOrderEmails.ToString().Replace(" ", "");
				if (!this.IsValidEmail(str))
				{
					if (str.Contains(",") || str.Contains(";"))
					{
						string[] strArrays1 = str.Split(chrArray, StringSplitOptions.RemoveEmptyEntries);
						if (strArrays1.Length != 0)
						{
							strArrays = strArrays1;
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								if (!this.IsValidEmail(strArrays[i]))
								{
									validationResults.Add(new ValidationResult("Notification (New Order) Email has an invalid value", new string[] { "NewOrderEmails" }));
								}
							}
						}
					}
					else
					{
						validationResults.Add(new ValidationResult("Notification (New Order) Email value is invalid", new string[] { "NewOrderEmails" }));
					}
				}
			}
			if (this.Notifications.NewCustomerEmails.Length > 0)
			{
				string str1 = this.Notifications.NewCustomerEmails.ToString().Replace(" ", "");
				if (!this.IsValidEmail(str1))
				{
					if (str1.Contains(",") || str1.Contains(";"))
					{
						string[] strArrays2 = str1.Split(chrArray, StringSplitOptions.RemoveEmptyEntries);
						if (strArrays2.Length != 0)
						{
							strArrays = strArrays2;
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								if (!this.IsValidEmail(strArrays[i]))
								{
									validationResults.Add(new ValidationResult("Notification (New Customer) Email has an invalid value", new string[] { "NewCustomerEmails" }));
								}
							}
						}
					}
					else
					{
						validationResults.Add(new ValidationResult("Notification (New Customer) Email value is invalid", new string[] { "NewCustomerEmails" }));
					}
				}
			}
			if (this.Notifications.NewMessageEmails.Length > 0)
			{
				string str2 = this.Notifications.NewMessageEmails.ToString().Replace(" ", "");
				if (!this.IsValidEmail(str2))
				{
					if (str2.Contains(",") || str2.Contains(";"))
					{
						string[] strArrays3 = str2.Split(chrArray, StringSplitOptions.RemoveEmptyEntries);
						if (strArrays3.Length != 0)
						{
							strArrays = strArrays3;
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								if (!this.IsValidEmail(strArrays[i]))
								{
									validationResults.Add(new ValidationResult("Notification (New Message) Email has an invalid value", new string[] { "NewMessageEmails" }));
								}
							}
						}
					}
					else
					{
						validationResults.Add(new ValidationResult("Notification (New Message) Email value is invalid", new string[] { "NewMessageEmails" }));
					}
				}
			}
			if (this.Notifications.LowPercentageEmails.Length > 0)
			{
				string str3 = this.Notifications.LowPercentageEmails.ToString().Replace(" ", "");
				if (!this.IsValidEmail(str3))
				{
					if (str3.Contains(",") || str3.Contains(";"))
					{
						string[] strArrays4 = str3.Split(chrArray, StringSplitOptions.RemoveEmptyEntries);
						if (strArrays4.Length != 0)
						{
							strArrays = strArrays4;
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								if (!this.IsValidEmail(strArrays[i]))
								{
									validationResults.Add(new ValidationResult("Notification (Low Percentage) Email has an invalid value", new string[] { "LowPercentageEmails" }));
								}
							}
						}
					}
					else
					{
						validationResults.Add(new ValidationResult("Notification (Low Percentage) Email value is invalid", new string[] { "LowPercentageEmails" }));
					}
				}
			}
			if (validationResults.Count > 0)
			{
				throw new AbpValidationException("Data validation failed! See ValidationErrors for details.", validationResults);
			}
		}

		public void ValidateHostSettings()
		{
			List<ValidationResult> validationResults = new List<ValidationResult>();
			if (this.General == null)
			{
				validationResults.Add(new ValidationResult("General settings can not be null", new string[] { "General" }));
			}
			else if (this.General.WebSiteRootAddress.IsNullOrEmpty())
			{
				validationResults.Add(new ValidationResult("General.WebSiteRootAddress can not be null or empty", new string[] { "WebSiteRootAddress" }));
			}
			if (this.Email == null)
			{
				validationResults.Add(new ValidationResult("Email settings can not be null", new string[] { "Email" }));
			}
			if (validationResults.Count > 0)
			{
				throw new AbpValidationException("Method arguments are not valid! See ValidationErrors for details.", validationResults);
			}
		}
	}
}