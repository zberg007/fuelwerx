using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products
{
	[Table("FuelWerxProducts")]
	public class Product : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNameLength = 255;

		public const int MaxReferenceLength = 255;

		public const int MaxSkuLength = 99;

		[Required]
		public virtual decimal BasePrice
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string Description
		{
			get;
			set;
		}

		[Required]
		public virtual decimal FinalPrice
		{
			get;
			set;
		}

		public virtual Guid? ImageId
		{
			get;
			set;
		}

		[Required]
		public virtual bool IsActive
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string Name
		{
			get;
			set;
		}

		[Required]
		public virtual int QuantityOnHand
		{
			get;
			set;
		}

		[Required]
		public virtual string QuantitySoldIn
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string Reference
		{
			get;
			set;
		}

		[MaxLength(99)]
		public virtual string Sku
		{
			get;
			set;
		}

		[Required]
		public virtual decimal Surcharge
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public Product()
		{
		}
	}
}