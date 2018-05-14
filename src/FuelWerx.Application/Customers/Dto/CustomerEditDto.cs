using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Administrative.Titles.Dto;
using FuelWerx.Customers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Customers.Dto
{
	[AutoMapTo(new Type[] { typeof(Customer) })]
	public class CustomerEditDto : IValidate, IPassivable
	{
		[Required]
		public virtual bool AllowBillPay
		{
			get;
			set;
		}

		[StringLength(255)]
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

		[RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage="Format must be valid email address (user@domain.tld).")]
		[StringLength(255)]
		public virtual string Email
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string FirstName
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

		[Required]
		[StringLength(255)]
		public string LastName
		{
			get;
			set;
		}

		public Guid? LogoId
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

		[ForeignKey("TitleId")]
		public virtual TitleEditDto Title
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

		public CustomerEditDto()
		{
		}
	}
}