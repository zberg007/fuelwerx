using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.Trucks.Dto
{
	public class GetTruckForEditOutput : IOutputDto, IDto
	{
		public Guid? ImageId
		{
			get;
			set;
		}

		public TruckEditDto Truck
		{
			get;
			set;
		}

		public GetTruckForEditOutput()
		{
		}
	}
}