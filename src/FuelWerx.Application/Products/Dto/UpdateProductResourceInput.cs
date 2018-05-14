using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	public class UpdateProductResourceInput
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

		public bool IsActive
		{
			get;
			set;
		}

		public long ProductId
		{
			get;
			set;
		}

		public Guid? ResourceId
		{
			get;
			set;
		}

		public UpdateProductResourceInput()
		{
		}
	}
}