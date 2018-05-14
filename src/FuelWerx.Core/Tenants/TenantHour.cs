using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Tenants
{
	[Table("FuelWerxTenantHours")]
	public class TenantHour : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxTimeFieldsValueLength = 15;

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

		public TenantHour()
		{
		}
	}
}