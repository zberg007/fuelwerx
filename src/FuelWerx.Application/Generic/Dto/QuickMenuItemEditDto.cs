using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapTo(new Type[] { typeof(QuickMenuItem) })]
	public class QuickMenuItemEditDto : IValidate
	{
		[StringLength(400)]
		public virtual string FaviconUrl
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		[MaxLength(120)]
		[Required]
		public virtual string Label
		{
			get;
			set;
		}

		[Required]
		public virtual bool LoadInNewWindow
		{
			get;
			set;
		}

		[Required]
		public virtual long OwnerId
		{
			get;
			set;
		}

		[Required]
		[StringLength(400)]
		public virtual string Url
		{
			get;
			set;
		}

		public QuickMenuItemEditDto()
		{
		}
	}
}