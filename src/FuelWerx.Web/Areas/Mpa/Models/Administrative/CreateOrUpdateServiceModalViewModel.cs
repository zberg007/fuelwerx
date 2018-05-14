using Abp.AutoMapper;
using FuelWerx.Generic.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Administrative
{
	[AutoMapFrom(new Type[] { typeof(GetServiceForEditOutput) })]
	public class CreateOrUpdateServiceModalViewModel : GetServiceForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Service.Id.HasValue;
			}
		}

		public CreateOrUpdateServiceModalViewModel(GetServiceForEditOutput output)
		{
			output.MapTo<GetServiceForEditOutput, CreateOrUpdateServiceModalViewModel>(this);
		}
	}
}