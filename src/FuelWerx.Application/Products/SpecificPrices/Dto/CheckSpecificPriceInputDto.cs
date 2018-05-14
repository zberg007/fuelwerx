using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.SpecificPrices.Dto
{
	public class CheckSpecificPriceInputDto
	{
		public string ForCurrency
		{
			get;
			set;
		}

		public int ForCustomerAddressId
		{
			get;
			set;
		}

		public int ForCustomerId
		{
			get;
			set;
		}

		public int ProductId
		{
			get;
			set;
		}

		public string ProductOptionIds
		{
			get;
			set;
		}

		public int Quantity
		{
			get;
			set;
		}

		public CheckSpecificPriceInputDto()
		{
		}
	}
}