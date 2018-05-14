using Abp.AutoMapper;
using FuelWerx.Assets.Trucks.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Trucks
{
	[AutoMapFrom(new Type[] { typeof(GetTruckForEditOutput) })]
	public class CreateOrEditTruckModalViewModel : GetTruckForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Truck.Id.HasValue;
			}
		}

		public CreateOrEditTruckModalViewModel(GetTruckForEditOutput output)
		{
			output.MapTo<GetTruckForEditOutput, CreateOrEditTruckModalViewModel>(this);
		}
	}
}