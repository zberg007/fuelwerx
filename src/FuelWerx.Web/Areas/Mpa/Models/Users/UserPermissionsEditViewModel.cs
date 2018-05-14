using Abp.AutoMapper;
using FuelWerx.Authorization.Dto;
using FuelWerx.Authorization.Users;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Common;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Users
{
	[AutoMapFrom(new Type[] { typeof(GetUserPermissionsForEditOutput) })]
	public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
	{
		public FuelWerx.Authorization.Users.User User
		{
			get;
			private set;
		}

		public UserPermissionsEditViewModel(GetUserPermissionsForEditOutput output, FuelWerx.Authorization.Users.User user)
		{
			this.User = user;
			output.MapTo<GetUserPermissionsForEditOutput, UserPermissionsEditViewModel>(this);
		}
	}
}