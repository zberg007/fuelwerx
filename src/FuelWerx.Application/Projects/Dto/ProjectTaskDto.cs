using Abp.AutoMapper;
using FuelWerx.Projects;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapTo(new Type[] { typeof(ProjectTask) })]
	public class ProjectTaskDto
	{
		public virtual string Comment
		{
			get;
			set;
		}

		public virtual decimal? Cost
		{
			get;
			set;
		}

		public virtual decimal? Discount
		{
			get;
			set;
		}

		public virtual long Id
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual bool IsComplete
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

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

		public virtual decimal? Retail
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public ProjectTaskDto()
		{
		}
	}
}