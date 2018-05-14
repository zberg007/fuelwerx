using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.Storage;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects
{
	[Table("FuelWerxProjectResources")]
	public class ProjectResource : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNameLength = 255;

		public const int MaxDescriptionLength = 1200;

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

		public virtual int TenantId
		{
			get;
			set;
		}

		public ProjectResource()
		{
		}
	}
}