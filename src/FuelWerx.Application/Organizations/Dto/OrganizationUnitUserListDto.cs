using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Authorization.Users;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Organizations.Dto
{
	[AutoMapFrom(new Type[] { typeof(User) })]
	public class OrganizationUnitUserListDto : EntityDto<long>
	{
		public DateTime AddedTime
		{
			get;
			set;
		}

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

		public Guid? ProfilePictureId
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

		public OrganizationUnitUserListDto()
		{
		}
	}
}