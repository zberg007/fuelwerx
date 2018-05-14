using Abp.AutoMapper;
using FuelWerx.Projects.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Projects
{
	[AutoMapFrom(new Type[] { typeof(GetProjectResourceForEditOutput) })]
	public class CreateOrUpdateProjectResourcesModalViewModel : GetProjectResourceForEditOutput
	{
		public long ProjectId
		{
			get;
			set;
		}

		public virtual string ProjectName
		{
			get;
			set;
		}

		public CreateOrUpdateProjectResourcesModalViewModel(GetProjectResourceForEditOutput output)
		{
			output.MapTo<GetProjectResourceForEditOutput, CreateOrUpdateProjectResourcesModalViewModel>(this);
		}

		public string FileSizeDisplay(long fileSize)
		{
			string[] strArrays = new string[] { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
			int num = 0;
			while (fileSize >= (long)1024)
			{
				num++;
				fileSize = fileSize / (long)1024;
			}
			return string.Format("{0} {1}", fileSize, strArrays[num]);
		}
	}
}