using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Zones.Dto
{
	public class CreateOrUpdateZoneInput : IInputDto, IDto, IValidate
	{
		[Required]
		public ZoneEditDto Zone
		{
			get;
			set;
		}

		public CreateOrUpdateZoneInput()
		{
		}
	}
}