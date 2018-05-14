using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Administrative;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Titles.Dto
{
	[AutoMapTo(new System.Type[] { typeof(Title) })]
	public class TitleEditDto : IValidate, IPassivable
	{
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

		[Required]
		[StringLength(255)]
		public virtual string Name
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
		public virtual string Type
		{
			get;
			set;
		}

		public TitleEditDto()
		{
		}
	}
}