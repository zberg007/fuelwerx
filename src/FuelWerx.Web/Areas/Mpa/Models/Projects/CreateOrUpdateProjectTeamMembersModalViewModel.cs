using Abp.AutoMapper;
using FuelWerx.Projects.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Projects
{
	[AutoMapFrom(new Type[] { typeof(GetProjectTeamMembersForEditOutput) })]
	public class CreateOrUpdateProjectTeamMembersModalViewModel : GetProjectTeamMembersForEditOutput
	{
		public long ProjectId
		{
			get;
			set;
		}

		public CreateOrUpdateProjectTeamMembersModalViewModel(GetProjectTeamMembersForEditOutput output)
		{
			output.MapTo<GetProjectTeamMembersForEditOutput, CreateOrUpdateProjectTeamMembersModalViewModel>(this);
		}
	}
}