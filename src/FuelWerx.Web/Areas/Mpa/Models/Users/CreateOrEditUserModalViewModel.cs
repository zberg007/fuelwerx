using Abp.AutoMapper;
using FuelWerx.Authorization.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Users
{
	[AutoMapFrom(new Type[] { typeof(GetUserForEditOutput) })]
	public class CreateOrEditUserModalViewModel : GetUserForEditOutput
	{
		public int AssignedRoleCount
		{
			get
			{
				return ((IEnumerable<UserRoleDto>)base.Roles).Count<UserRoleDto>((UserRoleDto r) => r.IsAssigned);
			}
		}

		public bool CanChangeUserName
		{
			get
			{
				return base.User.UserName != "admin";
			}
		}

		public bool IsEditMode
		{
			get
			{
				return base.User.Id.HasValue;
			}
		}

		public CreateOrEditUserModalViewModel(GetUserForEditOutput output)
		{
			output.MapTo<GetUserForEditOutput, CreateOrEditUserModalViewModel>(this);
		}
	}
}