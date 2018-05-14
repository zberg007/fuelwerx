using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Suppliers
{
	public class SupplierLogoUploadModel
	{
		public Guid? Id
		{
			get;
			set;
		}

		public int SupplierId
		{
			get;
			set;
		}

		public string SupplierName
		{
			get;
			set;
		}

		public SupplierLogoUploadModel()
		{
		}
	}
}