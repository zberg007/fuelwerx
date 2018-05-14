using Abp.AutoMapper;
using FuelWerx.Products.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Products
{
	[AutoMapFrom(new Type[] { typeof(GetProductResourceForEditOutput) })]
	public class CreateOrUpdateProductResourcesModalViewModel : GetProductResourceForEditOutput
	{
		public long ProductId
		{
			get;
			set;
		}

		public virtual string ProductName
		{
			get;
			set;
		}

		public CreateOrUpdateProductResourcesModalViewModel(GetProductResourceForEditOutput output)
		{
			output.MapTo<GetProductResourceForEditOutput, CreateOrUpdateProductResourcesModalViewModel>(this);
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