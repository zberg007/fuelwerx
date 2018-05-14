using Abp.AutoMapper;
using FuelWerx.Tenants;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	[AutoMapFrom(new Type[] { typeof(TenantCustomerService) })]
	public class TenantCustomerServiceEditDto
	{
		[MaxLength(255)]
		public virtual string Email
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string FridayClose
		{
			get;
			set;
		}

		public virtual bool FridayLunchObserved
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string FridayOpen
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string Label
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string MondayClose
		{
			get;
			set;
		}

		public virtual bool MondayLunchObserved
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string MondayOpen
		{
			get;
			set;
		}

		public virtual string Note
		{
			get;
			set;
		}

		[MaxLength(16)]
		public virtual string PhoneNumber
		{
			get;
			set;
		}

		[MaxLength(16)]
		public virtual string PhoneNumberEmergency
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string SaturdayClose
		{
			get;
			set;
		}

		public virtual bool SaturdayLunchObserved
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string SaturdayOpen
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string SundayClose
		{
			get;
			set;
		}

		public virtual bool SundayLunchObserved
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string SundayOpen
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string ThursdayClose
		{
			get;
			set;
		}

		public virtual bool ThursdayLunchObserved
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string ThursdayOpen
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string TuesdayClose
		{
			get;
			set;
		}

		public virtual bool TuesdayLunchObserved
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string TuesdayOpen
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string WednesdayClose
		{
			get;
			set;
		}

		public virtual bool WednesdayLunchObserved
		{
			get;
			set;
		}

		[MaxLength(15)]
		public virtual string WednesdayOpen
		{
			get;
			set;
		}

		public TenantCustomerServiceEditDto()
		{
		}
	}
}