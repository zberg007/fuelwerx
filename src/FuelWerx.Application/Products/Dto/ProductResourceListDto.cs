using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Products;
using FuelWerx.Storage;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapFrom(new Type[] { typeof(ProductResource) })]
	public class ProductResourceListDto : FullAuditedEntityDto
	{
		[ForeignKey("BinaryObjectId")]
		public virtual FuelWerx.Storage.BinaryObject BinaryObject
		{
			get;
			set;
		}

		public virtual Guid BinaryObjectId
		{
			get;
			set;
		}

		public virtual string Category
		{
			get;
			set;
		}

		public virtual string Description
		{
			get;
			set;
		}

		public virtual string FileExtension
		{
			get;
			set;
		}

		public virtual string FileName
		{
			get;
			set;
		}

		public virtual string FileSize
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

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

		public virtual long ProductId
		{
			get;
			set;
		}

		public ProductResourceListDto()
		{
		}
	}
}