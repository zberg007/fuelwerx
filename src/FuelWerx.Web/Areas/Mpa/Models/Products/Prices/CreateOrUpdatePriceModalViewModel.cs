using Abp.AutoMapper;
using FuelWerx.Products.Prices.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Products.Prices
{
	[AutoMapFrom(new Type[] { typeof(GetProductPriceForEditOutput) })]
	public class CreateOrUpdatePriceModalViewModel : GetProductPriceForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Price.Id.HasValue;
			}
		}

		public bool ProductIsCurrentlyActive
		{
			get;
			set;
		}

		public CreateOrUpdatePriceModalViewModel(GetProductPriceForEditOutput output)
		{
			output.MapTo<GetProductPriceForEditOutput, CreateOrUpdatePriceModalViewModel>(this);
		}
	}
}