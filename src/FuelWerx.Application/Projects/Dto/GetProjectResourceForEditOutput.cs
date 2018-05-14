using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	public class GetProjectResourceForEditOutput : IOutputDto, IDto
	{
		public List<ProjectResourceEditDto> ProjectResources
		{
			get;
			set;
		}

		public GetProjectResourceForEditOutput()
		{
		}
	}
}