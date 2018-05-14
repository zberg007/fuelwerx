using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Editions.Dto;
using FuelWerx.MultiTenancy;
using FuelWerx.MultiTenancy.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Common;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Tenants
{
	[AutoMapFrom(new Type[] { typeof(GetTenantFeaturesForEditOutput) })]
	public class TenantFeaturesEditViewModel : GetTenantFeaturesForEditOutput, IFeatureEditViewModel
	{
		public FuelWerx.MultiTenancy.Tenant Tenant
		{
			get;
			set;
		}

		public TenantFeaturesEditViewModel(FuelWerx.MultiTenancy.Tenant tenant, GetTenantFeaturesForEditOutput output)
		{
			this.Tenant = tenant;
			output.MapTo<GetTenantFeaturesForEditOutput, TenantFeaturesEditViewModel>(this);
		}
	}
}