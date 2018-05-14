using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Administrative;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Contacts.Dto
{
	[AutoMapTo(new Type[] { typeof(Contact) })]
	public class ContactEditDto : IValidate, IPassivable
	{
		public virtual string Description
		{
			get;
			set;
		}

		[Required]
		[StringLength(600)]
		public virtual string Email
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		public Guid? ImageId
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

		public virtual int? TenantId
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string Title
		{
			get;
			set;
		}

		public ContactEditDto()
		{
		}
	}
}