using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class GetInvoicePaymentsInput : PagedAndSortedInputDto, IShouldNormalize
	{
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

		public long? InvoiceId
		{
			get;
			set;
		}

		public GetInvoicePaymentsInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "TransactionDateTime";
			}
		}
	}
}