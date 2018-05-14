using Abp.Domain.Entities;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	public class Lookup : IMustHaveTenant
	{
		public virtual string AssociatedClass
		{
			get;
			set;
		}

		public virtual bool Disabled
		{
			get;
			set;
		}

		public virtual string Group
		{
			get;
			set;
		}

		public virtual bool Selected
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public virtual string Text
		{
			get;
			set;
		}

		public virtual string Value
		{
			get;
			set;
		}

		public Lookup()
		{
		}
	}
}