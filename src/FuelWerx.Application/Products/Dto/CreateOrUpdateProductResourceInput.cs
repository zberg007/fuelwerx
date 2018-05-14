using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapFrom(new Type[] { typeof(CreateOrUpdateProductResourceInput) })]
	public class CreateOrUpdateProductResourceInput : IInputDto, IDto, IValidate
	{
		[Required]
		public virtual long? ProductId
		{
			get;
			set;
		}

		public List<ProductResourceEditDto> ProductResources
		{
			get;
			set;
		}

		public CreateOrUpdateProductResourceInput()
		{
		}
	}
}