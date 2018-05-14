using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Taxes.Dto
{
	public class GetTaxForEditOutput : IOutputDto, IDto
	{
		public TaxEditDto Tax
		{
			get;
			set;
		}

		public GetTaxForEditOutput()
		{
		}
	}
}