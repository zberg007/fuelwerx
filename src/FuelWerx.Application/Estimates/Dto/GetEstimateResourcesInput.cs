using Abp.Application.Services.Dto;
using FuelWerx.Estimates;
using System;

namespace FuelWerx.Estimates.Dto
{
	public class GetEstimateResourcesInput : ListResultDto<EstimateResource>
	{
		public GetEstimateResourcesInput()
		{
		}
	}
}