using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	public class GetEstimateForEditOutput : IOutputDto, IDto
	{
		public EstimateEditDto Estimate
		{
			get;
			set;
		}

		public GetEstimateForEditOutput()
		{
		}
	}
}