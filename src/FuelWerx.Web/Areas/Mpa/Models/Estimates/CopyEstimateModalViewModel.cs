using Abp.AutoMapper;
using FuelWerx.Estimates.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Estimates
{
	[AutoMapFrom(new Type[] { typeof(GetEstimateForCopyOutput) })]
	public class CopyEstimateModalViewModel : GetEstimateForCopyOutput
	{
		public CopyEstimateModalViewModel(GetEstimateForCopyOutput output)
		{
			output.MapTo<GetEstimateForCopyOutput, CopyEstimateModalViewModel>(this);
		}
	}
}