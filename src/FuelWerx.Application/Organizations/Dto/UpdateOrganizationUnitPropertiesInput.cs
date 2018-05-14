using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Organizations.Dto
{
	public class UpdateOrganizationUnitPropertiesInput : IInputDto, IDto, IValidate
	{
		public decimal? Discount
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		[Required]
		public long OrganizationUnitId
		{
			get;
			set;
		}

		public bool? ShowPrice
		{
			get;
			set;
		}

		public bool? SpecificPricesEnabled
		{
			get;
			set;
		}

		public decimal? Upcharge
		{
			get;
			set;
		}

		public UpdateOrganizationUnitPropertiesInput()
		{
		}
	}
}