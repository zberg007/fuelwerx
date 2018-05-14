using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class GetBankForEditOutput : IOutputDto, IDto
	{
		public BankEditDto Bank
		{
			get;
			set;
		}

		public string OwnerName
		{
			get;
			set;
		}

		public GetBankForEditOutput()
		{
		}
	}
}