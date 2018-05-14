using Abp.AutoMapper;
using FuelWerx.Products.SpecificPrices.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Products.SpecificPrices
{
	[AutoMapFrom(new Type[] { typeof(GetProductSpecificPriceForEditOutput) })]
	public class CreateOrUpdateSpecificPriceModalViewModel : GetProductSpecificPriceForEditOutput
	{
		public decimal? BaseCost
		{
			get;
			set;
		}

		public string CurrentlySelectedCustomerName
		{
			get;
			set;
		}

		public bool IsEditMode
		{
			get
			{
				return base.SpecificPrice.Id.HasValue;
			}
		}

		public string QuantitySoldIn
		{
			get;
			set;
		}

		public CreateOrUpdateSpecificPriceModalViewModel(GetProductSpecificPriceForEditOutput output)
		{
			output.MapTo<GetProductSpecificPriceForEditOutput, CreateOrUpdateSpecificPriceModalViewModel>(this);
		}
	}
}