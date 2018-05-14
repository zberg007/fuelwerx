using Abp.AutoMapper;
using FuelWerx.Products.Dto;
using FuelWerx.Projects;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapTo(new Type[] { typeof(ProjectProduct) })]
	public class ProjectProductDto
	{
		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual ProjectProductLineItemDto LineItem
		{
			get;
			set;
		}

		public virtual ProductDto Product
		{
			get;
			set;
		}

		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual long ProjectId
		{
			get;
			set;
		}

		public ProjectProductDto()
		{
		}
	}
}