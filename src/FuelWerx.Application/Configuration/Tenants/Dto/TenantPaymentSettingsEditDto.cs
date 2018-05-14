using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.Tenants;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	[AutoMapFrom(new Type[] { typeof(TenantPaymentSettings) })]
	public class TenantPaymentSettingsEditDto : IValidate
	{
		public virtual bool? AllowOnlinePayments
		{
			get;
			set;
		}

		public virtual string DefaultPaymentTerm
		{
			get;
			set;
		}

		public virtual bool? EnableAutoPay
		{
			get;
			set;
		}

		public virtual bool? EnableBudgetBilling
		{
			get;
			set;
		}

		public virtual bool? EnableCapping
		{
			get;
			set;
		}

		public virtual bool? EnableCOD
		{
			get;
			set;
		}

		public virtual string EnableCODMessage
		{
			get;
			set;
		}

		public virtual bool? EnableCODWarning
		{
			get;
			set;
		}

		public virtual string EnableCODWarningMessage
		{
			get;
			set;
		}

		public virtual bool? EnableDeliveryReceiptEmailed
		{
			get;
			set;
		}

		public virtual bool? EnableInstallmentBilling
		{
			get;
			set;
		}

		public virtual bool? EnableInvoiceEmailed
		{
			get;
			set;
		}

		public virtual bool? EnablePrePurchase
		{
			get;
			set;
		}

		public virtual bool? EnablePrintedDeliveryReceipt
		{
			get;
			set;
		}

		public virtual bool? EnablePrintedInvoice
		{
			get;
			set;
		}

		public virtual bool? EnablePrintedStatement
		{
			get;
			set;
		}

		public virtual bool? EnableReminderOne
		{
			get;
			set;
		}

		public virtual string EnableReminderOneMessage
		{
			get;
			set;
		}

		public virtual bool? EnableReminderThree
		{
			get;
			set;
		}

		public virtual string EnableReminderThreeMessage
		{
			get;
			set;
		}

		public virtual bool? EnableReminderTwo
		{
			get;
			set;
		}

		public virtual string EnableReminderTwoMessage
		{
			get;
			set;
		}

		public virtual bool? EnableStatementEmailed
		{
			get;
			set;
		}

		public virtual decimal FlatProjectAmount
		{
			get;
			set;
		}

		public virtual decimal HourlyProjectRate
		{
			get;
			set;
		}

		public virtual decimal HourlyStaffRate
		{
			get;
			set;
		}

		public virtual decimal HourlyTaskRate
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		public virtual string InvoiceNumber_Prefix
		{
			get;
			set;
		}

		public virtual long? InvoiceNumber_StartingNumber
		{
			get;
			set;
		}

		[Required]
		public virtual int TenantId
		{
			get;
			set;
		}

		public TenantPaymentSettingsEditDto()
		{
		}
	}
}