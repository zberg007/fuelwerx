using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices
{
	[Table("FuelWerxInvoicePayments")]
	public class InvoicePayment : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxXResponseCode = 12;

		public const int MaxXResponseReasonCode = 12;

		public const int MaxXResponseReasonText = 70;

		public const int MaxXAuthCode = 40;

		public const int MaxXTransId = 75;

		public const int MaxPAuthorization_Num = 75;

		public const int MaxPCustomerRef = 75;

		public const int MaxPBankRespCode = 5;

		public const int MaxPBankMessage = 120;

		public const int MaxPEXactMessage = 150;

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

		public InvoicePayment()
		{
		}
	}
}