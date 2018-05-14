using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Organizations.Dto
{
	public class CreateOrganizationUnitInput : IInputDto, IDto, IValidate
	{
		[Required]
		[StringLength(128)]
		public string DisplayName
		{
			get;
			set;
		}

		public long? ParentId
		{
			get;
			set;
		}

		public CreateOrganizationUnitInput()
		{
		}
	}
}