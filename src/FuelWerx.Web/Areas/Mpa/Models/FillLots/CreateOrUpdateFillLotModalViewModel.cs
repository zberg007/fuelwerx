using Abp.AutoMapper;
using FuelWerx.Assets.FillLots.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.FillLots
{
	[AutoMapFrom(new Type[] { typeof(GetFillLotForEditOutput) })]
	public class CreateOrUpdateFillLotModalViewModel : GetFillLotForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.FillLot.Id.HasValue;
			}
		}

		public CreateOrUpdateFillLotModalViewModel(GetFillLotForEditOutput output)
		{
			output.MapTo<GetFillLotForEditOutput, CreateOrUpdateFillLotModalViewModel>(this);
		}
	}
}