using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapFrom(new Type[] { typeof(CreateOrUpdateProjectResourceInput) })]
	public class CreateOrUpdateProjectResourceInput : IInputDto, IDto, IValidate
	{
		[Required]
		public virtual long? ProjectId
		{
			get;
			set;
		}

		public List<ProjectResourceEditDto> ProjectResources
		{
			get;
			set;
		}

		public CreateOrUpdateProjectResourceInput()
		{
		}
	}
}