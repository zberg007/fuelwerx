using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Administrative;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Dto
{
	[AutoMapTo(new Type[] { typeof(TaxRuleRule) })]
	public class TaxRuleRuleEditDto : IValidate, IPassivable
	{
		[MaxLength(40)]
		[Required]
		public virtual string Behavior
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string Caption
		{
			get;
			set;
		}

		[ForeignKey("CountryId")]
		public virtual FuelWerx.Generic.Country Country
		{
			get;
			set;
		}

		[Required]
		public virtual int CountryId
		{
			get;
			set;
		}

		[ForeignKey("CountryRegionId")]
		public virtual FuelWerx.Generic.CountryRegion CountryRegion
		{
			get;
			set;
		}

		public virtual int? CountryRegionId
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

		[MaxLength(15)]
		public virtual string PostalCodeRange
		{
			get;
			set;
		}

		[ForeignKey("TaxId")]
		public virtual FuelWerx.Administrative.Tax Tax
		{
			get;
			set;
		}

		[Required]
		public virtual long TaxId
		{
			get;
			set;
		}

		[ForeignKey("TaxRuleId")]
		public virtual FuelWerx.Administrative.TaxRule TaxRule
		{
			get;
			set;
		}

		[Required]
		public virtual long TaxRuleId
		{
			get;
			set;
		}

		public TaxRuleRuleEditDto()
		{
		}
	}
}