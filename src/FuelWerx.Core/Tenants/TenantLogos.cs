using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Tenants
{
	[Table("FuelWerxTenantLogos")]
	public class TenantLogos : FullAuditedEntity<long>, IMustHaveTenant
	{
		public virtual Guid? HeaderImageId
		{
			get;
			set;
		}

		public virtual Guid? HeaderMobileImageId
		{
			get;
			set;
		}

		public virtual Guid? InvoiceImageId
		{
			get;
			set;
		}

		public virtual Guid? MailImageId
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public TenantLogos()
		{
		}
	}
}