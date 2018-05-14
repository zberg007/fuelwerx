using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Dto
{
	[AutoMapFrom(new Type[] { typeof(TaxRuleRule) })]
	public class TaxRuleRuleListDto : FullAuditedEntityDto
	{
		public virtual string Behavior
		{
			get;
			set;
		}

		public virtual string Caption
		{
			get;
			set;
		}

		public virtual int CountryId
		{
			get;
			set;
		}

		public virtual int? CountryRegionId
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual string PostalCodeRange
		{
			get;
			set;
		}

		public virtual FuelWerx.Administrative.Tax Tax
		{
			get;
			set;
		}

		public virtual int TaxId
		{
			get;
			set;
		}

		public virtual FuelWerx.Administrative.TaxRule TaxRule
		{
			get;
			set;
		}

		public virtual int TaxRuleId
		{
			get;
			set;
		}

		public TaxRuleRuleListDto()
		{
		}
	}
}