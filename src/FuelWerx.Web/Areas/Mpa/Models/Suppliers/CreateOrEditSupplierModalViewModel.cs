using Abp.AutoMapper;
using FuelWerx.Suppliers.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Suppliers
{
	[AutoMapFrom(new Type[] { typeof(GetSupplierForEditOutput) })]
	public class CreateOrEditSupplierModalViewModel : GetSupplierForEditOutput
	{
		public int CountryId
		{
			get
			{
				return base.Supplier.CountryId;
			}
		}

		public bool IsEditMode
		{
			get
			{
				return base.Supplier.Id.HasValue;
			}
		}

		public CreateOrEditSupplierModalViewModel(GetSupplierForEditOutput output)
		{
			output.MapTo<GetSupplierForEditOutput, CreateOrEditSupplierModalViewModel>(this);
		}
	}
}