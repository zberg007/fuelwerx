using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Customers.Dto;
using FuelWerx.Invoices;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapFrom(new Type[] { typeof(InvoicePayment) })]
	public class InvoicePaymentListDto : CreationAuditedEntityDto<long>
	{
		public virtual CustomerDto Customer
		{
			get;
			set;
		}

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

		public virtual bool ExportedToReporting
		{
			get;
			set;
		}

		public virtual InvoiceEditDto Invoice
		{
			get;
			set;
		}

		public virtual long InvoiceId
		{
			get;
			set;
		}

		public virtual string P_Authorization_Num
		{
			get;
			set;
		}

		public virtual string P_Bank_Message
		{
			get;
			set;
		}

		public virtual string P_Bank_Resp_Code
		{
			get;
			set;
		}

		public virtual string P_Customer_Ref
		{
			get;
			set;
		}

		public virtual string P_Exact_Ctr
		{
			get;
			set;
		}

		public virtual string P_EXact_Message
		{
			get;
			set;
		}

		public virtual string P_ResponseObject
		{
			get;
			set;
		}

		public virtual DateTime TransactionDateTime
		{
			get;
			set;
		}

		public virtual string X_Auth_Code
		{
			get;
			set;
		}

		public virtual string X_Response_Code
		{
			get;
			set;
		}

		public virtual string X_Response_Reason_Code
		{
			get;
			set;
		}

		public virtual string X_Response_Reason_Text
		{
			get;
			set;
		}

		public virtual string X_Trans_Id
		{
			get;
			set;
		}

		public InvoicePaymentListDto()
		{
		}
	}
}