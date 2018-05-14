using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Titles.Dto
{
	[AutoMapTo(new System.Type[] { typeof(Title) })]
	public class TitleDto
	{
		public long Id
		{
			get;
			set;
		}

		public Guid? ImageId
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

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

		public virtual string Type
		{
			get;
			set;
		}

		public TitleDto()
		{
		}
	}
}