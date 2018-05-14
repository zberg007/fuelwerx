using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Authorization.Users;
using FuelWerx.Projects;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapFrom(new Type[] { typeof(ProjectTeamMember) })]
	public class ProductTeamMemberListDto : FullAuditedEntityDto
	{
		public virtual bool IsActive
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

		public virtual long TeamMemberId
		{
			get;
			set;
		}

		[ForeignKey("TeamMemberId")]
		public virtual FuelWerx.Authorization.Users.User User
		{
			get;
			set;
		}

		public ProductTeamMemberListDto()
		{
		}
	}
}