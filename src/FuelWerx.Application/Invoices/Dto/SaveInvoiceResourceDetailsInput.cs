using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class SaveInvoiceResourceDetailsInput
	{
		public virtual string Description
		{
			get;
			set;
		}

		public virtual long Id
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public SaveInvoiceResourceDetailsInput()
		{
		}
	}
}