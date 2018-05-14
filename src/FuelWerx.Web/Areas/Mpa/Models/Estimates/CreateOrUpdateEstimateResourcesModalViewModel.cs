using Abp.AutoMapper;
using FuelWerx.Estimates.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Estimates
{
	[AutoMapFrom(new Type[] { typeof(GetEstimateResourceForEditOutput) })]
	public class CreateOrUpdateEstimateResourcesModalViewModel : GetEstimateResourceForEditOutput
	{
		public long EstimateId
		{
			get;
			set;
		}

		public virtual string EstimateName
		{
			get;
			set;
		}

		public CreateOrUpdateEstimateResourcesModalViewModel(GetEstimateResourceForEditOutput output)
		{
			output.MapTo<GetEstimateResourceForEditOutput, CreateOrUpdateEstimateResourcesModalViewModel>(this);
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