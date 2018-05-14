using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.FillLots.Dto
{
	public class GetFillLotForEditOutput : IOutputDto, IDto
	{
		public FillLotEditDto FillLot
		{
			get;
			set;
		}

		public GetFillLotForEditOutput()
		{
		}
	}
}