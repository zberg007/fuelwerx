using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	public class GetProjectTeamMembersForEditOutput : IOutputDto, IDto
	{
		public List<ProjectTeamMemberEditDto> ProjectTeamMembers
		{
			get;
			set;
		}

		public GetProjectTeamMembersForEditOutput()
		{
		}
	}
}