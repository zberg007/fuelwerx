using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Projects;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapFrom(new Type[] { typeof(ProjectTeamMember) })]
	public class ProjectTeamMemberEditDto : IValidate, IPassivable
	{
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

		[Required]
		public virtual long ProjectId
		{
			get;
			set;
		}

		[Required]
		public virtual long TeamMemberId
		{
			get;
			set;
		}

		public ProjectTeamMemberEditDto()
		{
		}
	}
}