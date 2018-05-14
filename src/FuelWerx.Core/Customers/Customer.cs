using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.Administrative;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Customers
{
	[Table("FuelWerxCustomers")]
	public class Customer : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxFirstNameLength = 255;

		public const int MaxLastNameLength = 255;

		public const int MaxBusinessNameLength = 255;

		public const int MaxEmailLength = 255;

		public const int MaxDeliveryTypeLength = 40;

		public const int MaxPaymentTypeLength = 40;

		[Required]
		public virtual bool AllowBillPay
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string BusinessName
		{
			get;
			set;
		}

		[MaxLength(40)]
		[Required]
		public virtual string DeliveryType
		{
			get;
			set;
		}

		[Required]
		public virtual bool DoNotDeliver
		{
			get;
			set;
		}

		[EmailAddress]
		[MaxLength(255)]
		[Required]
		public virtual string Email
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string FirstName
		{
			get;
			set;
		}

		[NotMapped]
		public string FullName
		{
			get
			{
				return string.Concat(this.FirstName, (this.LastName.Length > 0 ? string.Concat(" ", this.LastName) : string.Empty));
			}
		}

		[Required]
		public virtual bool IsActive
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string LastName
		{
			get;
			set;
		}

		[Required]
		public virtual bool PaymentAssistanceParticipant
		{
			get;
			set;
		}

		[MaxLength(40)]
		[Required]
		public virtual string PaymentType
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		[ForeignKey("TitleId")]
		public virtual FuelWerx.Administrative.Title Title
		{
			get;
			set;
		}

		public virtual long? TitleId
		{
			get;
			set;
		}

		public virtual long? UserId
		{
			get;
			set;
		}

		public Customer()
		{
		}
	}
}