using Abp.AutoMapper;
using FuelWerx.Generic.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Administrative
{
	[AutoMapFrom(new Type[] { typeof(GetAddressForEditOutput) })]
	public class CreateOrUpdateAddressModalViewModel : GetAddressForEditOutput
	{
		public int CountryId
		{
			get
			{
				return base.Address.CountryId;
			}
		}

		public bool IsEditMode
		{
			get
			{
				return base.Address.Id.HasValue;
			}
		}

		public CreateOrUpdateAddressModalViewModel(GetAddressForEditOutput output)
		{
			output.MapTo<GetAddressForEditOutput, CreateOrUpdateAddressModalViewModel>(this);
		}
	}
}