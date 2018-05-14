using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Dto
{
	public class GetUserForEditOutput : IOutputDto, IDto
	{
		public int? ConvertToUserCustomerId
		{
			get;
			set;
		}

		public Guid? ProfilePictureId
		{
			get;
			set;
		}

		public UserRoleDto[] Roles
		{
			get;
			set;
		}

		public UserEditDto User
		{
			get;
			set;
		}

		public UserSettingDataEditDto UserSettingData
		{
			get;
			set;
		}

		public GetUserForEditOutput()
		{
		}
	}
}