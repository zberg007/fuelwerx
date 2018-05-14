using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Dto
{
	[AutoMapFrom(new Type[] { typeof(User) })]
	public class UserListDto : EntityDto<long>, IPassivable, IHasCreationTime
	{
		public DateTime CreationTime
		{
			get;
			set;
		}

		public string EmailAddress
		{
			get;
			set;
		}

		public bool IsActive
		{
			get;
			set;
		}

		public bool IsEmailConfirmed
		{
			get;
			set;
		}

		public DateTime? LastLoginTime
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public Guid? ProfilePictureId
		{
			get;
			set;
		}

		public List<UserListDto.UserListRoleDto> Roles
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

		public UserListDto()
		{
		}

		[AutoMapFrom(new Type[] { typeof(UserRole) })]
		public class UserListRoleDto
		{
			public int RoleId
			{
				get;
				set;
			}

			public string RoleName
			{
				get;
				set;
			}

			public UserListRoleDto()
			{
			}
		}
	}
}