using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Suppliers.Dto
{
	public class UpdateSupplierLogoInput
	{
		public Guid? LogoId
		{
			get;
			set;
		}

		public long SupplierId
		{
			get;
			set;
		}

		public UpdateSupplierLogoInput()
		{
		}
	}
}