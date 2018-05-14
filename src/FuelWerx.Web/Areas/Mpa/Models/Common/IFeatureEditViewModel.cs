using Abp.Application.Services.Dto;
using FuelWerx.Editions.Dto;
using System;
using System.Collections.Generic;

namespace FuelWerx.Web.Areas.Mpa.Models.Common
{
	public interface IFeatureEditViewModel
	{
		List<FlatFeatureDto> Features
		{
			get;
			set;
		}

		List<NameValueDto> FeatureValues
		{
			get;
			set;
		}
	}
}