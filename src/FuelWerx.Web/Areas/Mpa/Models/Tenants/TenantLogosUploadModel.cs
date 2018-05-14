using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Tenants
{
	public class TenantLogosUploadModel
	{
		public bool? ClearHeaderImageId
		{
			get;
			set;
		}

		public bool? ClearHeaderMobileImageId
		{
			get;
			set;
		}

		public bool? ClearInvoiceImageId
		{
			get;
			set;
		}

		public bool? ClearMailImageId
		{
			get;
			set;
		}

		public Guid? HeaderImageId
		{
			get;
			set;
		}

		public Guid? HeaderMobileImageId
		{
			get;
			set;
		}

		public Guid? InvoiceImageId
		{
			get;
			set;
		}

		public Guid? MailImageId
		{
			get;
			set;
		}

		public int? TenantId
		{
			get;
			set;
		}

		public TenantLogosUploadModel()
		{
		}
	}
}