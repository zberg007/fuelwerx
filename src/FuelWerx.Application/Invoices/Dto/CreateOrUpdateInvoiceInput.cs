using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class CreateOrUpdateInvoiceInput : IInputDto, IDto, IValidate
	{
		[Required]
		public InvoiceEditDto Invoice
		{
			get;
			set;
		}

		public CreateOrUpdateInvoiceInput()
		{
		}
	}
}