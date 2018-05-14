using Abp.AutoMapper;
using FuelWerx.Generic.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Administrative
{
	[AutoMapFrom(new Type[] { typeof(GetDriversDataForEditOutput) })]
	public class CreateOrUpdateDriversDataModalViewModel : GetDriversDataForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.DriversData.Id.HasValue;
			}
		}

		public CreateOrUpdateDriversDataModalViewModel(GetDriversDataForEditOutput output)
		{
			output.MapTo<GetDriversDataForEditOutput, CreateOrUpdateDriversDataModalViewModel>(this);
		}
	}
}