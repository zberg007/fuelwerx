using Abp.AutoMapper;
using FuelWerx.Administrative.TaxRules.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.TaxRules
{
	[AutoMapFrom(new Type[] { typeof(GetTaxRuleForEditOutput) })]
	public class CreateOrUpdateTaxRuleModalViewModel : GetTaxRuleForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.TaxRule.Id.HasValue;
			}
		}

		public CreateOrUpdateTaxRuleModalViewModel(GetTaxRuleForEditOutput output)
		{
			output.MapTo<GetTaxRuleForEditOutput, CreateOrUpdateTaxRuleModalViewModel>(this);
		}
	}
}