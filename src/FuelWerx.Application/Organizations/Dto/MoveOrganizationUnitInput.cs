using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Organizations.Dto
{
	public class MoveOrganizationUnitInput : IInputDto, IDto, IValidate
	{
		[Range(1, 9.22337203685478E+18)]
		public long Id
		{
			get;
			set;
		}

		public long? NewParentId
		{
			get;
			set;
		}

		public MoveOrganizationUnitInput()
		{
		}
	}
}