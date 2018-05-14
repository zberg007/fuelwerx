using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	public class UpdateProductImageInput
	{
		public Guid? ImageId
		{
			get;
			set;
		}

		public long ProductId
		{
			get;
			set;
		}

		public UpdateProductImageInput()
		{
		}
	}
}