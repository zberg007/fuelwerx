using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Tenants.Dashboard.Dto
{
	public class GetMemberActivityOutput
	{
		public List<int> NewMembers
		{
			get;
			set;
		}

		public List<int> TotalMembers
		{
			get;
			set;
		}

		public GetMemberActivityOutput()
		{
		}
	}
}