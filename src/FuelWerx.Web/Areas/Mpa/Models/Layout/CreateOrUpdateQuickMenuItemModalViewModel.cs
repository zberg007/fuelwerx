using Abp.AutoMapper;
using FuelWerx.Generic.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Layout
{
	[AutoMapFrom(new Type[] { typeof(GetQuickMenuItemForEditOutput) })]
	public class CreateOrUpdateQuickMenuItemModalViewModel : GetQuickMenuItemForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.QuickMenuItem.Id.HasValue;
			}
		}

		public CreateOrUpdateQuickMenuItemModalViewModel(GetQuickMenuItemForEditOutput output)
		{
			output.MapTo<GetQuickMenuItemForEditOutput, CreateOrUpdateQuickMenuItemModalViewModel>(this);
		}
	}
}