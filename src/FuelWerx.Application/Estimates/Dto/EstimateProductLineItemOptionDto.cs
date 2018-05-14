using Abp.AutoMapper;
using FuelWerx.Estimates;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	[AutoMapTo(new Type[] { typeof(EstimateProductLineItemOption) })]
	public class EstimateProductLineItemOptionDto
	{
		public virtual long EstimateId
		{
			get;
			set;
		}

		public virtual long Id
		{
			get;
			set;
		}

		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual long ProductOptionId
		{
			get;
			set;
		}

		public EstimateProductLineItemOptionDto()
		{
		}
	}
}