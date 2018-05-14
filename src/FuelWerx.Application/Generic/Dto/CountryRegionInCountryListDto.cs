using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new Type[] { typeof(CountryRegion) })]
	public class CountryRegionInCountryListDto : FullAuditedEntityDto
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

		public string Name
		{
			get;
			set;
		}

		public CountryRegionInCountryListDto()
		{
		}
	}
}