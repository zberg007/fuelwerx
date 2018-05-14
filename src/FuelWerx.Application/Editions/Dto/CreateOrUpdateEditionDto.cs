using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Editions.Dto
{
	public class CreateOrUpdateEditionDto : IInputDto, IDto, IValidate
	{
		[Required]
		public EditionEditDto Edition
		{
			get;
			set;
		}

		[Required]
		public List<NameValueDto> FeatureValues
		{
			get;
			set;
		}

		public CreateOrUpdateEditionDto()
		{
		}
	}
}