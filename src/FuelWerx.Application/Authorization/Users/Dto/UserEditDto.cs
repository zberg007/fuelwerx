using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Dto
{
	public class UserEditDto : IValidate, IPassivable
	{
		[EmailAddress]
		[Required]
		[StringLength(256)]
		public string EmailAddress
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		public bool IsActive
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

		[StringLength(32)]
		public string Password
		{
			get;
			set;
		}

		public bool ShouldChangePasswordOnNextLogin
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

		public UserEditDto()
		{
		}
	}
}