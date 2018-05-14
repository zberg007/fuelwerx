using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Customers.Dto;
using FuelWerx.Projects;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapFrom(new Type[] { typeof(Project) })]
	public class ProjectListDto : FullAuditedEntityDto
	{
		public virtual string CurrentStatus
		{
			get;
			set;
		}

		[ForeignKey("CustomerId")]
		public virtual CustomerDto Customer
		{
			get;
			set;
		}

		public virtual long CustomerId
		{
			get;
			set;
		}

		public virtual DateTime? Date
		{
			get;
			set;
		}

		public virtual string Description
		{
			get;
			set;
		}

		public virtual decimal Discount
		{
			get;
			set;
		}

		public virtual decimal Hours
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual string Label
		{
			get;
			set;
		}

		public virtual decimal LineTotal
		{
			get;
			set;
		}

		public virtual bool? LogDataAndTasksVisibleToCustomer
		{
			get;
			set;
		}

		public virtual string Number
		{
			get;
			set;
		}

		public virtual string PONumber
		{
			get;
			set;
		}

		public virtual decimal Rate
		{
			get;
			set;
		}

		public virtual int TaskTotal
		{
			get;
			set;
		}

		public virtual decimal Tax
		{
			get;
			set;
		}

		public virtual string Terms
		{
			get;
			set;
		}

		public virtual string TimeEntryLog
		{
			get;
			set;
		}

		public ProjectListDto()
		{
		}
	}
}