using Abp.AutoMapper;
using FuelWerx.Administrative.Taxes.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Taxes
{
	[AutoMapFrom(new Type[] { typeof(GetTaxForEditOutput) })]
	public class CreateOrUpdateTaxModalViewModel : GetTaxForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Tax.Id.HasValue;
			}
		}

		public CreateOrUpdateTaxModalViewModel(GetTaxForEditOutput output)
		{
			output.MapTo<GetTaxForEditOutput, CreateOrUpdateTaxModalViewModel>(this);
		}
	}
}