using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Tenants
{
	[Table("FuelWerxTenantPaymentGatewaySettings")]
	public class TenantPaymentGatewaySettings : FullAuditedEntity<long>, IMustHaveTenant
	{
		[Column(TypeName="nvarchar(MAX)")]
		[Required]
		public virtual string GatewaySettings
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public TenantPaymentGatewaySettings()
		{
		}
	}
}