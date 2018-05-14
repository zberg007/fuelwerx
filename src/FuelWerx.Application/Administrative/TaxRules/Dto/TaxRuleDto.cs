using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Dto
{
	[AutoMapTo(new Type[] { typeof(TaxRule) })]
	public class TaxRuleDto
	{
		public virtual string Caption
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

		public virtual int TenantId
		{
			get;
			set;
		}

		public TaxRuleDto()
		{
		}
	}
}