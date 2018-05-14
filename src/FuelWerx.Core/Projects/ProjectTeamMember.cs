using Abp.Domain.Entities.Auditing;
using FuelWerx.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects
{
	[Table("FuelWerxProjectTeamMembers")]
	public class ProjectTeamMember : FullAuditedEntity<long>
	{
		[Required]
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

		[Required]
		public virtual long ProjectId
		{
			get;
			set;
		}

		[ForeignKey("TeamMemberId")]
		public virtual User TeamMember
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

		public ProjectTeamMember()
		{
		}
	}
}