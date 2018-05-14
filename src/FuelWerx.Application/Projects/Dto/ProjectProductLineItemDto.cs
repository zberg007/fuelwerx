using Abp.AutoMapper;
using FuelWerx.Projects;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapTo(new Type[] { typeof(ProjectProductLineItem) })]
	public class ProjectProductLineItemDto
	{
		public virtual decimal Cost
		{
			get;
			set;
		}

		public virtual ICollection<ProjectProductLineItemOptionDto> Options
		{
			get;
			set;
		}

		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual int Quantity
		{
			get;
			set;
		}

		public ProjectProductLineItemDto()
		{
		}
	}
}