using Abp.Domain.Entities.Auditing;
using FuelWerx.Products;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects
{
	[Table("FuelWerxProjectProducts")]
	public class ProjectProduct : FullAuditedEntity<long>
	{
		[Required]
		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual ProjectProductLineItem LineItem
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

		[ForeignKey("ProductId")]
		public virtual FuelWerx.Products.Product Product
		{
			get;
			set;
		}

		[Required]
		public virtual long ProductId
		{
			get;
			set;
		}

		[ForeignKey("ProjectId")]
		public virtual FuelWerx.Projects.Project Project
		{
			get;
			set;
		}

		[Required]
		public virtual long ProjectId
		{
			get;
			set;
		}

		public ProjectProduct()
		{
		}
	}
}