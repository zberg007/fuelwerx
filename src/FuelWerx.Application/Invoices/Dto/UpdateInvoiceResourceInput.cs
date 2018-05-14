using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class UpdateInvoiceResourceInput
	{
		public string FileExtension
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}

		public string FileSize
		{
			get;
			set;
		}

		public long Id
		{
			get;
			set;
		}

		public long InvoiceId
		{
			get;
			set;
		}

		public bool IsActive
		{
			get;
			set;
		}

		public Guid? ResourceId
		{
			get;
			set;
		}

		public UpdateInvoiceResourceInput()
		{
		}
	}
}