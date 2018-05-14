using Abp.AutoMapper;
using FuelWerx.FuelCastSettings.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.FuelCastSettings
{
	[AutoMapFrom(new Type[] { typeof(GetSettingsOutput) })]
	public class ManageSettings : GetSettingsOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Settings.Id.HasValue;
			}
		}

		public ManageSettings(GetSettingsOutput output)
		{
			output.MapTo<GetSettingsOutput, ManageSettings>(this);
		}
	}
}