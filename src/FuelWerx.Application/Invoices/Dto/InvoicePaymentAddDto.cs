using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.Customers;
using FuelWerx.Invoices;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapTo(new Type[] { typeof(InvoicePayment) })]
	public class InvoicePaymentAddDto : IValidate
	{
		[ForeignKey("CustomerId")]
		public virtual FuelWerx.Customers.Customer Customer
		{
			get;
			set;
		}

		[Required]
		public virtual long CustomerId
		{
			get;
			set;
		}

		public virtual decimal DollarAmount
		{
			get;
			set;
		}

		[Required]
		public virtual bool ExportedToReporting
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		[ForeignKey("InvoiceId")]
		public virtual FuelWerx.Invoices.Invoice Invoice
		{
			get;
			set;
		}

		[Required]
		public virtual long InvoiceId
		{
			get;
			set;
		}

		[MaxLength(75)]
		public virtual string P_Authorization_Num
		{
			get;
			set;
		}

		[MaxLength(120)]
		public virtual string P_Bank_Message
		{
			get;
			set;
		}

		[MaxLength(5)]
		public virtual string P_Bank_Resp_Code
		{
			get;
			set;
		}

		[MaxLength(75)]
		public virtual string P_Customer_Ref
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string P_Exact_Ctr
		{
			get;
			set;
		}

		[MaxLength(150)]
		public virtual string P_EXact_Message
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string P_ResponseObject
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public virtual DateTime TransactionDateTime
		{
			get;
			set;
		}

		[MaxLength(40)]
		public virtual string X_Auth_Code
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string X_Description
		{
			get;
			set;
		}

		[MaxLength(12)]
		public virtual string X_Response_Code
		{
			get;
			set;
		}

		[MaxLength(12)]
		public virtual string X_Response_Reason_Code
		{
			get;
			set;
		}

		[MaxLength(70)]
		public virtual string X_Response_Reason_Text
		{
			get;
			set;
		}

		[MaxLength(75)]
		public virtual string X_Trans_Id
		{
			get;
			set;
		}

		public InvoicePaymentAddDto()
		{
		}
	}
}