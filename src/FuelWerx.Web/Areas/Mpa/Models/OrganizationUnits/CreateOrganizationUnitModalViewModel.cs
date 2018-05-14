using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.OrganizationUnits
{
	public class CreateOrganizationUnitModalViewModel
	{
		public long? ParentId
		{
			get;
			set;
		}

		public CreateOrganizationUnitModalViewModel(long? parentId)
		{
			this.ParentId = parentId;
		}
	}
}