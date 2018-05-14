using Abp.Domain.Entities.Auditing;
using FuelWerx.Products;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates
{
	[Table("FuelWerxEstimateProducts")]
	public class EstimateProduct : FullAuditedEntity<long>
	{
		public virtual FuelWerx.Estimates.Estimate Estimate
		{
			get;
			set;
		}

		[ForeignKey("Estimate")]
		public virtual long EstimateId
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

		public virtual EstimateProductLineItem LineItem
		{
			get;
			set;
		}

		[ForeignKey("LineItem")]
		public virtual long? LineItemId
		{
			get;
			set;
		}

		public virtual FuelWerx.Products.Product Product
		{
			get;
			set;
		}

		[ForeignKey("Product")]
		public virtual long ProductId
		{
			get;
			set;
		}

		public EstimateProduct()
		{
		}
	}
}