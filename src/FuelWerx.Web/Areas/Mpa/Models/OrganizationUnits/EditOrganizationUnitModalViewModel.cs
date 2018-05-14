using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.OrganizationUnits
{
	[AutoMapFrom(new Type[] { typeof(OrganizationUnit) })]
	public class EditOrganizationUnitModalViewModel
	{
		public string DisplayName
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		public EditOrganizationUnitModalViewModel()
		{
		}
	}
}