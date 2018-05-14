using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	public class SaveProductResourceDetailsInput
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

		public SaveProductResourceDetailsInput()
		{
		}
	}
}