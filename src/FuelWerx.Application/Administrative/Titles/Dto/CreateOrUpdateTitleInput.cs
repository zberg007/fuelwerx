using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Titles.Dto
{
	public class CreateOrUpdateTitleInput : IInputDto, IDto, IValidate
	{
		[Required]
		public TitleEditDto Title
		{
			get;
			set;
		}

		public byte?[] TitleImage
		{
			get;
			set;
		}

		public CreateOrUpdateTitleInput()
		{
		}
	}
}