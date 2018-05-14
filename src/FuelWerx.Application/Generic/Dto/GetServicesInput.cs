using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class GetServicesInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public long? AddressId
		{
			get;
			set;
		}

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

		public string OwnerType
		{
			get;
			set;
		}

		public GetServicesInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "Name,Type";
			}
		}
	}
}