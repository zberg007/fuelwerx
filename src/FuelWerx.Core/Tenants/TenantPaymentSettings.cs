using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Tenants
{
	[Table("FuelWerxTenantPaymentSettings")]
	public class TenantPaymentSettings : FullAuditedEntity<long>, IMustHaveTenant
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

		[Column(TypeName="nvarchar(MAX)")]
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

		[Column(TypeName="nvarchar(MAX)")]
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

		[Column(TypeName="nvarchar(MAX)")]
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

		[Column(TypeName="nvarchar(MAX)")]
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

		[Column(TypeName="nvarchar(MAX)")]
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

		[MaxLength(10)]
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

		public virtual int TenantId
		{
			get;
			set;
		}

		public TenantPaymentSettings()
		{
		}
	}
}