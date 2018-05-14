using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Authorization.Users;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Sessions.Dto
{
	[AutoMapFrom(new Type[] { typeof(User) })]
	public class UserLoginInfoDto : EntityDto<long>
	{
		public string EmailAddress
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string ProfilePictureId
		{
			get;
			set;
		}

		public string Surname
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public UserLoginInfoDto()
		{
		}
	}
}