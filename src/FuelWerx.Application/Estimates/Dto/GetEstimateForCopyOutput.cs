using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	public class GetEstimateForCopyOutput : IOutputDto, IDto
	{
		public EstimateCopyDto Estimate
		{
			get;
			set;
		}

		public GetEstimateForCopyOutput()
		{
		}
	}
}