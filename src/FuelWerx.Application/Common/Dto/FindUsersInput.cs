using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Common.Dto
{
	public class FindUsersInput : PagedAndFilteredInputDto
	{
		public int? TenantId
		{
			get;
			set;
		}

		public FindUsersInput()
		{
		}
	}
}