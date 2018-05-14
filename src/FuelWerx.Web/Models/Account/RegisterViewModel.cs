using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class RegisterViewModel : IInputDto, IDto, IValidate
	{
		[EmailAddress]
		[Required]
		[StringLength(256)]
		public string EmailAddress
		{
			get;
			set;
		}

		public bool IsExternalLogin
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

		[Required]
		[StringLength(32)]
		public string Surname
		{
			get;
			set;
		}

		[StringLength(64)]
		public string TenancyName
		{
			get;
			set;
		}

		[StringLength(32)]
		public string UserName
		{
			get;
			set;
		}

		public RegisterViewModel()
		{
		}
	}
}