using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new Type[] { typeof(Country) })]
	public class CountriesListDto : FullAuditedEntityDto
	{
		public string Code
		{
			get;
			set;
		}

		public virtual IList<CountryRegionInCountryListDto> CountryRegions
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public CountriesListDto()
		{
		}
	}
}