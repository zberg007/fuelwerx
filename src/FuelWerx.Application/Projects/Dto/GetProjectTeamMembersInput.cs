using Abp.Application.Services.Dto;
using FuelWerx.Projects;
using System;

namespace FuelWerx.Projects.Dto
{
	public class GetProjectTeamMembersInput : ListResultDto<ProjectTeamMember>
	{
		public GetProjectTeamMembersInput()
		{
		}
	}
}