using Abp.AutoMapper;
using FuelWerx.Authorization.Dto;
using FuelWerx.Authorization.Roles.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Common;
using System;
using System.Collections.Generic;

namespace FuelWerx.Web.Areas.Mpa.Models.Roles
{
	[AutoMapFrom(new Type[] { typeof(GetRoleForEditOutput) })]
	public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
	{
		public bool IsEditMode
		{
			get
			{
				return base.Role.Id.HasValue;
			}
		}

		public CreateOrEditRoleModalViewModel(GetRoleForEditOutput output)
		{
			output.MapTo<GetRoleForEditOutput, CreateOrEditRoleModalViewModel>(this);
		}
	}
}