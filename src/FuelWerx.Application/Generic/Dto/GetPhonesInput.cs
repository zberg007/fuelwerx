using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class GetPhonesInput : PagedAndSortedInputDto, IShouldNormalize
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

		public string OwnerType
		{
			get;
			set;
		}

		public GetPhonesInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "Type,PhoneNumber";
			}
		}
	}
}