using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	public class GetProjectForEditOutput : IOutputDto, IDto
	{
		public ProjectEditDto Project
		{
			get;
			set;
		}

		public GetProjectForEditOutput()
		{
		}
	}
}