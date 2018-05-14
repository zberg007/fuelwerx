using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Contacts.Dto
{
	public class CreateOrUpdateContactInput : IInputDto, IDto, IValidate
	{
		[Required]
		public ContactEditDto Contact
		{
			get;
			set;
		}

		public byte?[] TitleImage
		{
			get;
			set;
		}

		public CreateOrUpdateContactInput()
		{
		}
	}
}