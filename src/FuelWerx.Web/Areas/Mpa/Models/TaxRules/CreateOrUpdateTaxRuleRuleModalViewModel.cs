using Abp.AutoMapper;
using FuelWerx.Administrative.TaxRules.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.TaxRules
{
	[AutoMapFrom(new Type[] { typeof(GetTaxRuleRuleForEditOutput) })]
	public class CreateOrUpdateTaxRuleRuleModalViewModel : GetTaxRuleRuleForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.TaxRuleRule.Id.HasValue;
			}
		}

		public CreateOrUpdateTaxRuleRuleModalViewModel(GetTaxRuleRuleForEditOutput output)
		{
			output.MapTo<GetTaxRuleRuleForEditOutput, CreateOrUpdateTaxRuleRuleModalViewModel>(this);
		}
	}
}