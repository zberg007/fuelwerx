using Abp.AutoMapper;
using FuelWerx.Authorization.Users.Profile.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Profile
{
	[AutoMapFrom(new Type[] { typeof(CurrentUserProfileEditDto) })]
	public class MySettingsViewModel : CurrentUserProfileEditDto
	{
		public bool CanChangeUserName
		{
			get
			{
				return base.UserName != "admin";
			}
		}

		public MySettingsViewModel(CurrentUserProfileEditDto currentUserProfileEditDto)
		{
			currentUserProfileEditDto.MapTo<CurrentUserProfileEditDto, MySettingsViewModel>(this);
		}
	}
}