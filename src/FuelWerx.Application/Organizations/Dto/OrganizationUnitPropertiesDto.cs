using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Organizations;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Organizations.Dto
{
	[AutoMapFrom(new Type[] { typeof(OrganizationUnitProperties) })]
	public class OrganizationUnitPropertiesDto : AuditedEntityDto<long>
	{
		public decimal? Discount
		{
			get;
			set;
		}

		public long OrganizationUnitId
		{
			get;
			set;
		}

		public bool? ShowPrice
		{
			get;
			set;
		}

		public bool? SpecificPricesEnabled
		{
			get;
			set;
		}

		public decimal? Upcharge
		{
			get;
			set;
		}

		public OrganizationUnitPropertiesDto()
		{
		}
	}
}