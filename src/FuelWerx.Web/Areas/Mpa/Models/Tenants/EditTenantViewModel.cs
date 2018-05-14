using Abp.Application.Services.Dto;
using FuelWerx.MultiTenancy.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Tenants
{
	public class EditTenantViewModel
	{
		public IReadOnlyList<ComboboxItemDto> EditionItems
		{
			get;
			set;
		}

		public TenantEditDto Tenant
		{
			get;
			set;
		}

		public EditTenantViewModel(TenantEditDto tenant, IReadOnlyList<ComboboxItemDto> editionItems)
		{
			this.Tenant = tenant;
			this.EditionItems = editionItems;
		}
	}
}