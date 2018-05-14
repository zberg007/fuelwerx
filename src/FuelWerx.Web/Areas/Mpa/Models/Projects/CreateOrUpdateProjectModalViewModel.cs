using Abp.AutoMapper;
using FuelWerx.Projects.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Projects
{
	[AutoMapFrom(new Type[] { typeof(GetProjectForEditOutput) })]
	public class CreateOrUpdateProjectModalViewModel : GetProjectForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Project.Id.HasValue;
			}
		}

		public CreateOrUpdateProjectModalViewModel(GetProjectForEditOutput output)
		{
			output.MapTo<GetProjectForEditOutput, CreateOrUpdateProjectModalViewModel>(this);
		}
	}
}