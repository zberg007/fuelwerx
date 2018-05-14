using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Trucks
{
	public class TruckImageUploadModel
	{
		public Guid? Id
		{
			get;
			set;
		}

		public int TruckId
		{
			get;
			set;
		}

		public string TruckName
		{
			get;
			set;
		}

		public string TruckNumber
		{
			get;
			set;
		}

		public TruckImageUploadModel()
		{
		}
	}
}