using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.MultiTenancy;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.MultiTenancy.Dto
{
	[AutoMap(new Type[] { typeof(Tenant) })]
	public class TenantEditDto : EntityDto, IDoubleWayDto, IInputDto, IDto, IValidate, IOutputDto
	{
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

		[Required]
		[StringLength(64)]
		public string TenancyName
		{
			get;
			set;
		}

		public TenantEditDto()
		{
		}
	}
}