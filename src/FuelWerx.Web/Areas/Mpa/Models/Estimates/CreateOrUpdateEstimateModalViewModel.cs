using Abp.AutoMapper;
using FuelWerx.Estimates.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Estimates
{
	[AutoMapFrom(new Type[] { typeof(GetEstimateForEditOutput) })]
	public class CreateOrUpdateEstimateModalViewModel : GetEstimateForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Estimate.Id.HasValue;
			}
		}

		public CreateOrUpdateEstimateModalViewModel(GetEstimateForEditOutput output)
		{
			output.MapTo<GetEstimateForEditOutput, CreateOrUpdateEstimateModalViewModel>(this);
		}
	}
}