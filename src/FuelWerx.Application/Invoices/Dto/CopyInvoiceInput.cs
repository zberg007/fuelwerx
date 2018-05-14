using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class CopyInvoiceInput : IInputDto, IDto, IValidate
	{
		[Required]
		public InvoiceCopyDto Invoice
		{
			get;
			set;
		}

		public CopyInvoiceInput()
		{
		}
	}
}