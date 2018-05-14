using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Customers.Dto
{
	public class GetCustomerForEditOutput : IOutputDto, IDto
	{
		public CustomerEditDto Customer
		{
			get;
			set;
		}

		public virtual decimal? CustomerOwesTotal
		{
			get;
			set;
		}

		public GetCustomerForEditOutput()
		{
		}
	}
}