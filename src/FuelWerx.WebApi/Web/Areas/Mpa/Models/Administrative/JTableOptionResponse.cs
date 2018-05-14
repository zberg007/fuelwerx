using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Administrative
{
	public class JTableOptionResponse
	{
		public List<JTableOption> Options
		{
			get;
			set;
		}

		public string Result
		{
			get;
			set;
		}

		public JTableOptionResponse()
		{
		}
	}
}