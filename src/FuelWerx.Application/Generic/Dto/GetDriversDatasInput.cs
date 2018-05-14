using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class GetDriversDatasInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public string Filter
		{
			get;
			set;
		}

		public long? OwnerId
		{
			get;
			set;
		}

		public GetDriversDatasInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "CDLExpiration";
			}
		}
	}
}