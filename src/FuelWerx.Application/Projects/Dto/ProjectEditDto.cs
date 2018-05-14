using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Customers.Dto;
using FuelWerx.Generic.Dto;
using FuelWerx.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapTo(new Type[] { typeof(Project) })]
	public class ProjectEditDto : IValidate, IPassivable
	{
		public ICollection<ProjectAdhocProduct> AdhocProducts
		{
			get;
			set;
		}

		public ICollection<ProjectAdjustment> Adjustments
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
		public virtual CustomerDto Customer
		{
			get;
			set;
		}

		[ForeignKey("CustomerAddressId")]
		public virtual AddressDto CustomerAddress
		{
			get;
			set;
		}

		public virtual long? CustomerAddressId
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string CustomerEmail
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string CustomerFirstName
		{
			get;
			set;
		}

		public virtual long? CustomerId
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string CustomerLastName
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

		public long? Id
		{
			get;
			set;
		}

		public virtual long? InvoiceId
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

		[MaxLength(99)]
		public virtual string PONumber
		{
			get;
			set;
		}

		public ICollection<ProjectProductDto> Products
		{
			get;
			set;
		}

		public virtual decimal Rate
		{
			get;
			set;
		}

		public ICollection<ProjectTask> Tasks
		{
			get;
			set;
		}

		public virtual decimal Tax
		{
			get;
			set;
		}

		public virtual int? TenantId
		{
			get;
			set;
		}

		public virtual string Terms
		{
			get;
			set;
		}

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

		public ProjectEditDto()
		{
		}
	}
}