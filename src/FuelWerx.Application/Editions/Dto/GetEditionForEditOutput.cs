using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Editions.Dto
{
	public class GetEditionForEditOutput : IOutputDto, IDto
	{
		public EditionEditDto Edition
		{
			get;
			set;
		}

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

		public GetEditionForEditOutput()
		{
		}
	}
}