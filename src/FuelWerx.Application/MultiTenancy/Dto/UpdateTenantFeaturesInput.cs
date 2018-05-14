using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.MultiTenancy.Dto
{
	public class UpdateTenantFeaturesInput : IInputDto, IDto, IValidate
	{
		[Required]
		public List<NameValueDto> FeatureValues
		{
			get;
			set;
		}

		[Range(1, 2147483647)]
		public int Id
		{
			get;
			set;
		}

		public UpdateTenantFeaturesInput()
		{
		}
	}
}