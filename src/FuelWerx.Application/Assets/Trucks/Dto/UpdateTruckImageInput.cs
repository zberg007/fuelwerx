using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.Trucks.Dto
{
	public class UpdateTruckImageInput
	{
		public Guid? ImageId
		{
			get;
			set;
		}

		public long TruckId
		{
			get;
			set;
		}

		public UpdateTruckImageInput()
		{
		}
	}
}