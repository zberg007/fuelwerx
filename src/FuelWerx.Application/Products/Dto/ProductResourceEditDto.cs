using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Products;
using FuelWerx.Storage;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapFrom(new Type[] { typeof(ProductResource) })]
	public class ProductResourceEditDto : IValidate, IPassivable
	{
		[ForeignKey("BinaryObjectId")]
		public virtual FuelWerx.Storage.BinaryObject BinaryObject
		{
			get;
			set;
		}

		[Required]
		public virtual Guid BinaryObjectId
		{
			get;
			set;
		}

		[Required]
		public virtual string Category
		{
			get;
			set;
		}

		[MaxLength(1200)]
		public virtual string Description
		{
			get;
			set;
		}

		[Required]
		public virtual string FileExtension
		{
			get;
			set;
		}

		[Required]
		public virtual string FileName
		{
			get;
			set;
		}

		[Required]
		public virtual string FileSize
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

		public virtual int? TenantId
		{
			get;
			set;
		}

		public ProductResourceEditDto()
		{
		}
	}
}