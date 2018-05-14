using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new Type[] { typeof(QuickMenuItem) })]
	public class QuickMenuItemListDto
	{
		public virtual string FaviconUrl
		{
			get;
			set;
		}

		public virtual long? Id
		{
			get;
			set;
		}

		public virtual string Label
		{
			get;
			set;
		}

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

		public virtual string Url
		{
			get;
			set;
		}

		public QuickMenuItemListDto()
		{
		}
	}
}