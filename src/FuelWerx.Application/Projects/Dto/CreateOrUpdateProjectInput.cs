using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	public class CreateOrUpdateProjectInput : IInputDto, IDto, IValidate
	{
		[Required]
		public ProjectEditDto Project
		{
			get;
			set;
		}

		public CreateOrUpdateProjectInput()
		{
		}
	}
}