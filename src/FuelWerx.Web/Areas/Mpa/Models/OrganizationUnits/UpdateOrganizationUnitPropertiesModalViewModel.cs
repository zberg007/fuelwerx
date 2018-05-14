using Abp.AutoMapper;
using FuelWerx.Organizations;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.OrganizationUnits
{
	[AutoMapFrom(new Type[] { typeof(OrganizationUnitProperties) })]
	public class UpdateOrganizationUnitPropertiesModalViewModel
	{
		public decimal? Discount
		{
			get;
			set;
		}

		public long? Id
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

		public UpdateOrganizationUnitPropertiesModalViewModel(long organizationUnitId)
		{
			this.OrganizationUnitId = organizationUnitId;
		}
	}
}