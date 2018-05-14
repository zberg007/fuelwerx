using Abp.AutoMapper;
using FuelWerx.Generic.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Administrative
{
	[AutoMapFrom(new Type[] { typeof(GetPhoneForEditOutput) })]
	public class CreateOrUpdatePhoneModalViewModel : GetPhoneForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Phone.Id.HasValue;
			}
		}

		public CreateOrUpdatePhoneModalViewModel(GetPhoneForEditOutput output)
		{
			output.MapTo<GetPhoneForEditOutput, CreateOrUpdatePhoneModalViewModel>(this);
		}
	}
}