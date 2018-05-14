using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.FillLots.Dto
{
	public class CreateOrUpdateFillLotInput : IInputDto, IDto, IValidate
	{
		[Required]
		public FillLotEditDto FillLot
		{
			get;
			set;
		}

		public CreateOrUpdateFillLotInput()
		{
		}
	}
}