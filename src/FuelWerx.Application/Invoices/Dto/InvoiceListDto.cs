using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Customers.Dto;
using FuelWerx.Invoices;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapFrom(new Type[] { typeof(Invoice) })]
	public class InvoiceListDto : FullAuditedEntityDto
	{
		public virtual string CurrentStatus
		{
			get;
			set;
		}

		[ForeignKey("CustomerId")]
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

		public virtual DateTime? Date
		{
			get;
			set;
		}

		public virtual string Description
		{
			get;
			set;
		}

		public virtual decimal Discount
		{
			get;
			set;
		}

		public virtual DateTime? DueDate
		{
			get;
			set;
		}

		public virtual decimal Hours
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual string Label
		{
			get;
			set;
		}

		public virtual decimal LineTotal
		{
			get;
			set;
		}

		public virtual bool? LogDataAndTasksVisibleToCustomer
		{
			get;
			set;
		}

		public virtual string Number
		{
			get;
			set;
		}

		public virtual decimal? PaidTotal
		{
			get;
			set;
		}

		public virtual string PONumber
		{
			get;
			set;
		}

		public virtual DateTime? ProjectAcceptedTime
		{
			get;
			set;
		}

		public virtual long? ProjectId
		{
			get;
			set;
		}

		public virtual decimal Rate
		{
			get;
			set;
		}

		[NotMapped]
		public virtual int RelatedPaymentRecordsCount
		{
			get;
			set;
		}

		[NotMapped]
		public virtual decimal? RelatedPaymentRecordsTotalPaid
		{
			get;
			set;
		}

		public virtual int TaskTotal
		{
			get;
			set;
		}

		public virtual decimal Tax
		{
			get;
			set;
		}

		public virtual string Terms
		{
			get;
			set;
		}

		public virtual string TimeEntryLog
		{
			get;
			set;
		}

		public InvoiceListDto()
		{
		}
	}
}