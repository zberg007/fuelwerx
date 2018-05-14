using Abp.Application.Services.Dto;
using FuelWerx.Editions.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.MultiTenancy.Dto
{
	public class GetTenantFeaturesForEditOutput : IOutputDto, IDto
	{
		public List<FlatFeatureDto> Features
		{
			get;
			set;
		}

		public List<NameValueDto> FeatureValues
		{
			get;
			set;
		}

		public GetTenantFeaturesForEditOutput()
		{
		}
	}
}