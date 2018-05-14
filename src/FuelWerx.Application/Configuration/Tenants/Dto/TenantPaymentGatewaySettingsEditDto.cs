using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.Tenants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	[AutoMapFrom(new Type[] { typeof(TenantPaymentGatewaySettings) })]
	public class TenantPaymentGatewaySettingsEditDto : IValidate
	{
		[Column(TypeName="nvarchar(MAX)")]
		[Required]
		public virtual string GatewaySettings
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public TenantPaymentGatewaySettingsEditDto()
		{
		}
	}
}