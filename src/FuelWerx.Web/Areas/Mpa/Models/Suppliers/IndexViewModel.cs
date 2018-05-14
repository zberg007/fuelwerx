using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Suppliers.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Suppliers
{
	[AutoMapFrom(new Type[] { typeof(ListResultOutput<SupplierListDto>) })]
	public class IndexViewModel : ListResultOutput<SupplierListDto>
	{
		public string Filter
		{
			get;
			set;
		}

		public IndexViewModel(ListResultOutput<SupplierListDto> output, string filter = null)
		{
			output.MapTo<ListResultOutput<SupplierListDto>, IndexViewModel>(this);
			this.Filter = filter;
		}
	}
}