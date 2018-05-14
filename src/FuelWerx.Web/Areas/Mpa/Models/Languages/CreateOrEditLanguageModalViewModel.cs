using Abp.AutoMapper;
using FuelWerx.Localization.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Languages
{
	[AutoMapFrom(new Type[] { typeof(GetLanguageForEditOutput) })]
	public class CreateOrEditLanguageModalViewModel : GetLanguageForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Language.Id.HasValue;
			}
		}

		public CreateOrEditLanguageModalViewModel(GetLanguageForEditOutput output)
		{
			output.MapTo<GetLanguageForEditOutput, CreateOrEditLanguageModalViewModel>(this);
		}
	}
}