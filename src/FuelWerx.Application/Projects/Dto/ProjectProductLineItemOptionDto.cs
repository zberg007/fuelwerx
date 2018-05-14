using Abp.AutoMapper;
using FuelWerx.Projects;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapTo(new Type[] { typeof(ProjectProductLineItemOption) })]
	public class ProjectProductLineItemOptionDto
	{
		public virtual long Id
		{
			get;
			set;
		}

		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual long ProductOptionId
		{
			get;
			set;
		}

		public virtual long ProjectId
		{
			get;
			set;
		}

		public ProjectProductLineItemOptionDto()
		{
		}
	}
}