using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapFrom(new Type[] { typeof(CreateOrUpdateInvoiceResourceInput) })]
	public class CreateOrUpdateInvoiceResourceInput : IInputDto, IDto, IValidate
	{
		[Required]
		public virtual long? InvoiceId
		{
			get;
			set;
		}

		public List<InvoiceResourceEditDto> InvoiceResources
		{
			get;
			set;
		}

		public CreateOrUpdateInvoiceResourceInput()
		{
		}
	}
}