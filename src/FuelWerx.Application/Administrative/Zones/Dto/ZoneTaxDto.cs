using Abp.AutoMapper;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Taxes.Dto;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Zones.Dto
{
	[AutoMapFrom(new Type[] { typeof(ZoneTax) })]
	public class ZoneTaxDto
	{
		public virtual long Id
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		[ForeignKey("TaxId")]
		public virtual TaxDto Tax
		{
			get;
			set;
		}

		public virtual long TaxId
		{
			get;
			set;
		}

		public virtual long ZoneId
		{
			get;
			set;
		}

		public ZoneTaxDto()
		{
		}
	}
}