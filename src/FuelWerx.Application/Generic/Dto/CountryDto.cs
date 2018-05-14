using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new Type[] { typeof(Country) })]
	public class CountryDto
	{
		public string Code
		{
			get;
			set;
		}

		public ICollection<CountryRegionDto> CoountryRegions
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public CountryDto()
		{
		}
	}
}