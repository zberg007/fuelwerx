using Abp.AutoMapper;
using FuelWerx.Products.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Products
{
	[AutoMapFrom(new Type[] { typeof(GetProductForEditOutput) })]
	public class CreateOrUpdateProductModalViewModel : GetProductForEditOutput
	{
		public bool? CanMakeActive
		{
			get;
			set;
		}

		public bool IsEditMode
		{
			get
			{
				return base.Product.Id.HasValue;
			}
		}

		public CreateOrUpdateProductModalViewModel(GetProductForEditOutput output)
		{
			output.MapTo<GetProductForEditOutput, CreateOrUpdateProductModalViewModel>(this);
		}
	}
}