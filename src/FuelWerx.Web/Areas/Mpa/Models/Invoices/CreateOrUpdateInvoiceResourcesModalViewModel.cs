using Abp.AutoMapper;
using FuelWerx.Invoices.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Invoices
{
	[AutoMapFrom(new Type[] { typeof(GetInvoiceResourceForEditOutput) })]
	public class CreateOrUpdateInvoiceResourcesModalViewModel : GetInvoiceResourceForEditOutput
	{
		public long InvoiceId
		{
			get;
			set;
		}

		public virtual string InvoiceName
		{
			get;
			set;
		}

		public CreateOrUpdateInvoiceResourcesModalViewModel(GetInvoiceResourceForEditOutput output)
		{
			output.MapTo<GetInvoiceResourceForEditOutput, CreateOrUpdateInvoiceResourcesModalViewModel>(this);
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