using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Estimates;
using FuelWerx.Storage;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	[AutoMapFrom(new Type[] { typeof(EstimateResource) })]
	public class EstimateResourceListDto : FullAuditedEntityDto
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

		[ForeignKey("EstimateId")]
		public virtual FuelWerx.Estimates.Estimate Estimate
		{
			get;
			set;
		}

		public virtual long EstimateId
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

		public EstimateResourceListDto()
		{
		}
	}
}