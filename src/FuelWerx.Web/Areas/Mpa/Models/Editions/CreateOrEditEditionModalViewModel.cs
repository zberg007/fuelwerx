using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Editions.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Common;
using System;
using System.Collections.Generic;

namespace FuelWerx.Web.Areas.Mpa.Models.Editions
{
	[AutoMapFrom(new Type[] { typeof(GetEditionForEditOutput) })]
	public class CreateOrEditEditionModalViewModel : GetEditionForEditOutput, IFeatureEditViewModel
	{
		public bool IsEditMode
		{
			get
			{
				return base.Edition.Id.HasValue;
			}
		}

		public CreateOrEditEditionModalViewModel(GetEditionForEditOutput output)
		{
			output.MapTo<GetEditionForEditOutput, CreateOrEditEditionModalViewModel>(this);
		}
	}
}