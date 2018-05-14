using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Tenants
{
	[Table("FuelWerxCustomerServices")]
	public class TenantCustomerService : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxTimeFieldsValueLength = 15;

		public const int MaxLabelLength = 255;

		public const int MaxPhoneNumberLength = 16;

		public const int MaxPhoneNumberEmergencyLength = 16;

		public const int MaxEmailLength = 255;

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

		[Column(TypeName="nvarchar(MAX)")]
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

		public TenantCustomerService()
		{
		}
	}
}