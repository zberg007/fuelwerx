using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	public class GetEstimateResourceForEditOutput : IOutputDto, IDto
	{
		public List<EstimateResourceEditDto> EstimateResources
		{
			get;
			set;
		}

		public GetEstimateResourceForEditOutput()
		{
		}
	}
}