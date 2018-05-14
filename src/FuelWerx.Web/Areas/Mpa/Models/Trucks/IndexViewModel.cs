using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Assets.Trucks.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Trucks
{
	[AutoMapFrom(new Type[] { typeof(ListResultOutput<TruckListDto>) })]
	public class IndexViewModel : ListResultOutput<TruckListDto>
	{
		public string Filter
		{
			get;
			set;
		}

		public IndexViewModel(ListResultOutput<TruckListDto> output, string filter = null)
		{
			output.MapTo<ListResultOutput<TruckListDto>, IndexViewModel>(this);
			this.Filter = filter;
		}
	}
}