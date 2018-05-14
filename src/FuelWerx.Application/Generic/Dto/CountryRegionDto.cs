using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new Type[] { typeof(CountryRegion) })]
	public class CountryRegionDto
	{
		public string Code
		{
			get;
			set;
		}

		public int CountryId
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

		public CountryRegionDto()
		{
		}
	}
}