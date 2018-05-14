using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Dto
{
	[AutoMapFrom(new Type[] { typeof(TaxRule) })]
	public class TaxRuleListDto : FullAuditedEntityDto
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

		public TaxRuleListDto()
		{
		}
	}
}