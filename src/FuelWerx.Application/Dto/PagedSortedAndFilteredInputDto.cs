using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Dto
{
	public class PagedSortedAndFilteredInputDto : PagedAndSortedInputDto
	{
		public string Filter
		{
			get;
			set;
		}

		public PagedSortedAndFilteredInputDto()
		{
		}
	}
}