using Abp.AutoMapper;
using FuelWerx.Customers.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Customers
{
	[AutoMapFrom(new Type[] { typeof(GetCustomerForEditOutput) })]
	public class CreateOrEditCustomerModalViewModel : GetCustomerForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Customer.Id.HasValue;
			}
		}

		public CreateOrEditCustomerModalViewModel(GetCustomerForEditOutput output)
		{
			output.MapTo<GetCustomerForEditOutput, CreateOrEditCustomerModalViewModel>(this);
		}
	}
}