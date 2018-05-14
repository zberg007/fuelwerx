using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.Customers;
using FuelWerx.Generic;
using FuelWerx.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices
{
	[Table("FuelWerxInvoices")]
	public class Invoice : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxInternalLabelLength = 255;

		public const int MaxBillingTypeLength = 155;

		public const int MaxNumberLength = 38;

		public const int MaxPONumberLength = 99;

		public const int MaxCurrentStatusLength = 50;

		public const int MaxCurrentTermTypeLength = 255;

		public ICollection<InvoiceAdhocProduct> AdhocProducts
		{
			get;
			set;
		}

		public ICollection<InvoiceAdjustment> Adjustments
		{
			get;
			set;
		}

		[MaxLength(155)]
		[Required]
		public virtual string BillingType
		{
			get;
			set;
		}

		[MaxLength(50)]
		public virtual string CurrentStatus
		{
			get;
			set;
		}

		[ForeignKey("CustomerId")]
		public virtual FuelWerx.Customers.Customer Customer
		{
			get;
			set;
		}

		[ForeignKey("CustomerAddressId")]
		public virtual Address CustomerAddress
		{
			get;
			set;
		}

		public virtual long? CustomerAddressId
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

		[Column(TypeName="nvarchar(MAX)")]
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

		public virtual DateTime? DueDateDiscountExpirationDate
		{
			get;
			set;
		}

		public virtual decimal? DueDateDiscountTotal
		{
			get;
			set;
		}

		public virtual decimal? EmergencyDeliveryFee
		{
			get;
			set;
		}

		public virtual decimal? GroupDiscount
		{
			get;
			set;
		}

		public virtual decimal Hours
		{
			get;
			set;
		}

		public virtual decimal HoursActual
		{
			get;
			set;
		}

		public virtual bool? IncludeEmergencyDeliveryFee
		{
			get;
			set;
		}

		[Required]
		public virtual bool IsActive
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
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

		[MaxLength(38)]
		[Required]
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

		[MaxLength(99)]
		public virtual string PONumber
		{
			get;
			set;
		}

		public ICollection<InvoiceProduct> Products
		{
			get;
			set;
		}

		[ForeignKey("ProjectId")]
		public virtual FuelWerx.Projects.Project Project
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

		public ICollection<InvoiceResource> Resources
		{
			get;
			set;
		}

		public ICollection<InvoiceTask> Tasks
		{
			get;
			set;
		}

		[NotMapped]
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

		public ICollection<InvoiceTeamMember> TeamMembers
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public virtual string Terms
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string TermType
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string TimeEntryLog
		{
			get;
			set;
		}

		public virtual decimal? Upcharge
		{
			get;
			set;
		}

		public Invoice()
		{
		}
	}
}