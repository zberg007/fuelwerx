using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	public class CopyEstimateInput : IInputDto, IDto, IValidate
	{
		[Required]
		public EstimateCopyDto Estimate
		{
			get;
			set;
		}

		public CopyEstimateInput()
		{
		}
	}
}