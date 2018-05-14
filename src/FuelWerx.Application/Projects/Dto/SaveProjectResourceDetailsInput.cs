using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects.Dto
{
	public class SaveProjectResourceDetailsInput
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

		public SaveProjectResourceDetailsInput()
		{
		}
	}
}