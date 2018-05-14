using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	public class CreateOrUpdateEstimateInput : IInputDto, IDto, IValidate
	{
		[Required]
		public EstimateEditDto Estimate
		{
			get;
			set;
		}

		public CreateOrUpdateEstimateInput()
		{
		}
	}
}