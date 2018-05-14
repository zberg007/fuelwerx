using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.Tenants;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	[AutoMapFrom(new Type[] { typeof(TenantDateTimeSettings) })]
	public class TenantDateTimeSettingsEditDto : IValidate
	{
		public long? Id
		{
			get;
			set;
		}

		[Required]
		public virtual int TenantId
		{
			get;
			set;
		}

		[MaxLength(70)]
		[Required]
		public virtual string TimezoneId
		{
			get;
			set;
		}

		public TenantDateTimeSettingsEditDto()
		{
		}
	}
}