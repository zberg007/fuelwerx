using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Products
{
	public class ViewResourceViewModel
	{
		public Guid? BinaryObjectId
		{
			get;
			set;
		}

		public string FileExtension
		{
			get;
			set;
		}

		public ViewResourceViewModel()
		{
		}
	}
}