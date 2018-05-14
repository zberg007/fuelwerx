using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	[AutoMapFrom(new Type[] { typeof(CreateOrUpdateProjectTeamMemberInput) })]
	public class CreateOrUpdateProjectTeamMemberInput : IInputDto, IDto, IValidate
	{
		[Required]
		public virtual long? ProjectId
		{
			get;
			set;
		}

		public List<ProjectTeamMemberEditDto> ProjectTeamMembers
		{
			get;
			set;
		}

		public CreateOrUpdateProjectTeamMemberInput()
		{
		}
	}
}