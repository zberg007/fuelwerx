using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class GetCustomerInvoicesInput : PagedAndSortedInputDto, IShouldNormalize
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

		public long? Id
		{
			get;
			set;
		}

		public GetCustomerInvoicesInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "DueDate,Number";
			}
		}
	}
}