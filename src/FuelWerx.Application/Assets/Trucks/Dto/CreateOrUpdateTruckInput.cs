using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.Trucks.Dto
{
	public class CreateOrUpdateTruckInput : IInputDto, IDto, IValidate
	{
		[Required]
		public TruckEditDto Truck
		{
			get;
			set;
		}

		public CreateOrUpdateTruckInput()
		{
		}
	}
}