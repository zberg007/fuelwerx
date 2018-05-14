using Abp.AutoMapper;
using FuelWerx.Products.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Products
{
	[AutoMapFrom(new Type[] { typeof(GetProductSuppliersForEditOutput) })]
	public class CreateOrUpdateProductSuppliersModalViewModel : GetProductSuppliersForEditOutput
	{
		public long ProductId
		{
			get;
			set;
		}

		public CreateOrUpdateProductSuppliersModalViewModel(GetProductSuppliersForEditOutput output)
		{
			output.MapTo<GetProductSuppliersForEditOutput, CreateOrUpdateProductSuppliersModalViewModel>(this);
		}
	}
}