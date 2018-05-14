using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Customers.Dto;
using FuelWerx.Estimates;
using FuelWerx.Generic.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	[AutoMapTo(new Type[] { typeof(Estimate) })]
	public class EstimateEditDto : IValidate, IPassivable
	{
		public ICollection<EstimateAdhocProduct> AdhocProducts
		{
			get;
			set;
		}

		public ICollection<EstimateAdjustment> Adjustments
		{
			get;
			set;
		}

		[MaxLength(255)]
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

		public long? Id
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

		public ICollection<EstimateProductDto> Products
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

		public ICollection<EstimateTask> Tasks
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

		public EstimateEditDto()
		{
		}
	}
}