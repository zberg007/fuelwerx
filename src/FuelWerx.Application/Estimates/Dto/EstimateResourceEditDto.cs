using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Estimates;
using FuelWerx.Storage;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	[AutoMapFrom(new Type[] { typeof(EstimateResource) })]
	public class EstimateResourceEditDto : IValidate, IPassivable
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

		[ForeignKey("EstimateId")]
		public virtual FuelWerx.Estimates.Estimate Estimate
		{
			get;
			set;
		}

		[Required]
		public virtual long EstimateId
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

		public virtual int? TenantId
		{
			get;
			set;
		}

		public EstimateResourceEditDto()
		{
		}
	}
}