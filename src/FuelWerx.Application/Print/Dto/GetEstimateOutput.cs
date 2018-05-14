using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Print.Dto
{
	public class GetEstimateOutput : IOutputDto, IDto
	{
		public GetEstimateOutput Estimate
		{
			get;
			set;
		}

		public GetEstimateOutput()
		{
		}
	}
}