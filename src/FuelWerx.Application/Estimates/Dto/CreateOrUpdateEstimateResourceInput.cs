using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	[AutoMapFrom(new Type[] { typeof(CreateOrUpdateEstimateResourceInput) })]
	public class CreateOrUpdateEstimateResourceInput : IInputDto, IDto, IValidate
	{
		[Required]
		public virtual long? EstimateId
		{
			get;
			set;
		}

		public List<EstimateResourceEditDto> EstimateResources
		{
			get;
			set;
		}

		public CreateOrUpdateEstimateResourceInput()
		{
		}
	}
}