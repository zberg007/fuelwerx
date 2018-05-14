using Abp.AutoMapper;
using FuelWerx.Estimates;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	[AutoMapTo(new Type[] { typeof(EstimateProductLineItem) })]
	public class EstimateProductLineItemDto
	{
		public virtual decimal Cost
		{
			get;
			set;
		}

		public virtual ICollection<EstimateProductLineItemOptionDto> Options
		{
			get;
			set;
		}

		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual int Quantity
		{
			get;
			set;
		}

		public EstimateProductLineItemDto()
		{
		}
	}
}