using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.MultiTenancy.Dto
{
	public class CreateTenantInput : IInputDto, IDto, IValidate
	{
		[EmailAddress]
		[Required]
		[StringLength(256)]
		public string AdminEmailAddress
		{
			get;
			set;
		}

		[StringLength(128)]
		public string AdminPassword
		{
			get;
			set;
		}

		public int? EditionId
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
		[StringLength(128)]
		public string Name
		{
			get;
			set;
		}

		public bool SendActivationEmail
		{
			get;
			set;
		}

		public bool ShouldChangePasswordOnNextLogin
		{
			get;
			set;
		}

		[RegularExpression("^[a-zA-Z][a-zA-Z0-9_-]{1,}$")]
		[Required]
		[StringLength(64)]
		public string TenancyName
		{
			get;
			set;
		}

		public CreateTenantInput()
		{
		}
	}
}