using Abp.AutoMapper;
using FuelWerx.Administrative.Contacts.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Contacts
{
	[AutoMapFrom(new Type[] { typeof(GetContactForEditOutput) })]
	public class CreateOrUpdateContactModalViewModel : GetContactForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Contact.Id.HasValue;
			}
		}

		public CreateOrUpdateContactModalViewModel(GetContactForEditOutput output)
		{
			output.MapTo<GetContactForEditOutput, CreateOrUpdateContactModalViewModel>(this);
		}
	}
}