using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Organizations
{
	[Table("FuelWerxOrganizationUnitProperties")]
	public class OrganizationUnitProperties : FullAuditedEntity<long>, IMustHaveTenant
	{
		public decimal? Discount
		{
			get;
			set;
		}

		[ForeignKey("OrganizationUnitId")]
		public virtual Abp.Organizations.OrganizationUnit OrganizationUnit
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

		[Required]
		public virtual int TenantId
		{
			get;
			set;
		}

		public decimal? Upcharge
		{
			get;
			set;
		}

		public OrganizationUnitProperties()
		{
		}
	}
}