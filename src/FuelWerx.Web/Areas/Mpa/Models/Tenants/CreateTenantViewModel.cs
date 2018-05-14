using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Tenants
{
	public class CreateTenantViewModel
	{
		public IReadOnlyList<ComboboxItemDto> EditionItems
		{
			get;
			set;
		}

		public CreateTenantViewModel(IReadOnlyList<ComboboxItemDto> editionItems)
		{
			this.EditionItems = editionItems;
		}
	}
}