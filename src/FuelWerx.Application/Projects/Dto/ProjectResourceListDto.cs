using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Projects;
using FuelWerx.Storage;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapFrom(new Type[] { typeof(ProjectResource) })]
	public class ProjectResourceListDto : FullAuditedEntityDto
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

		[ForeignKey("ProjectId")]
		public virtual FuelWerx.Projects.Project Project
		{
			get;
			set;
		}

		public virtual long ProjectId
		{
			get;
			set;
		}

		public ProjectResourceListDto()
		{
		}
	}
}