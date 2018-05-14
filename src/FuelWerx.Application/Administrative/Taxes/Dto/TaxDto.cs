using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Taxes.Dto
{
	[AutoMapTo(new Type[] { typeof(Tax) })]
	public class TaxDto
	{
		public virtual string Caption
		{
			get;
			set;
		}

		public long Id
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

		public virtual decimal Rate
		{
			get;
			set;
		}

		public virtual int? TenantId
		{
			get;
			set;
		}

		public TaxDto()
		{
		}
	}
}