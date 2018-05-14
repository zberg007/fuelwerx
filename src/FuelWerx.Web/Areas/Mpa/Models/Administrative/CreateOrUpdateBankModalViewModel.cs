using Abp.AutoMapper;
using FuelWerx.Generic.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Administrative
{
	[AutoMapFrom(new Type[] { typeof(GetBankForEditOutput) })]
	public class CreateOrUpdateBankModalViewModel : GetBankForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Bank.Id.HasValue;
			}
		}

		public CreateOrUpdateBankModalViewModel(GetBankForEditOutput output)
		{
			output.MapTo<GetBankForEditOutput, CreateOrUpdateBankModalViewModel>(this);
		}
	}
}