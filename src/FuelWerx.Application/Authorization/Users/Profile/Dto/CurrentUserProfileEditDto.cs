using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Profile.Dto
{
	[AutoMap(new Type[] { typeof(User) })]
	public class CurrentUserProfileEditDto : IDoubleWayDto, IInputDto, IDto, IValidate, IOutputDto
	{
		[Required]
		[StringLength(256)]
		public string EmailAddress
		{
			get;
			set;
		}

		[Required]
		[StringLength(32)]
		public string Name
		{
			get;
			set;
		}

		[Required]
		[StringLength(32)]
		public string Surname
		{
			get;
			set;
		}

		[Required]
		[StringLength(32)]
		public string UserName
		{
			get;
			set;
		}

		public CurrentUserProfileEditDto()
		{
		}
	}
}