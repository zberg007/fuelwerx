using Abp.AutoMapper;
using FuelWerx.Administrative.Titles.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Titles
{
	[AutoMapFrom(new Type[] { typeof(GetTitleForEditOutput) })]
	public class CreateOrUpdateTitleModalViewModel : GetTitleForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Title.Id.HasValue;
			}
		}

		public CreateOrUpdateTitleModalViewModel(GetTitleForEditOutput output)
		{
			output.MapTo<GetTitleForEditOutput, CreateOrUpdateTitleModalViewModel>(this);
		}
	}
}