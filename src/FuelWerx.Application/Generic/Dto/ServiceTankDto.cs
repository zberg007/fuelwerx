using Abp.AutoMapper;
using FuelWerx.ServiceTanks;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapTo(new Type[] { typeof(ServiceTank) })]
	public class ServiceTankDto
	{
		public virtual decimal BaseTemperature
		{
			get;
			set;
		}

		public virtual decimal Capacity
		{
			get;
			set;
		}

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

		public virtual bool IsOwned
		{
			get;
			set;
		}

		public virtual string LastInspectionComments
		{
			get;
			set;
		}

		public virtual DateTime? LastInspectionDate
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual string Number
		{
			get;
			set;
		}

		public virtual decimal RemainingCapacity
		{
			get;
			set;
		}

		public virtual long ServiceId
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public ServiceTankDto()
		{
		}
	}
}