using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Customers.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Customers
{
	[AutoMapFrom(new Type[] { typeof(ListResultOutput<CustomerListDto>) })]
	public class IndexViewModel : ListResultOutput<CustomerListDto>
	{
		public string Filter
		{
			get;
			set;
		}

		public IndexViewModel(ListResultOutput<CustomerListDto> output, string filter = null)
		{
			output.MapTo<ListResultOutput<CustomerListDto>, IndexViewModel>(this);
			this.Filter = filter;
		}
	}
}