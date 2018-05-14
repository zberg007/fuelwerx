using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Tenants
{
	[Table("FuelWerxTenantNotifications")]
	public class TenantNotifications : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNewOrderEmailsLength = 450;

		public const int MaxNewCustomerEmailsLength = 450;

		public const int MaxNewMessageEmailsLength = 450;

		public const int MaxLowPercentageEmailsLength = 450;

		public bool? LowPercentage
		{
			get;
			set;
		}

		[MaxLength(450)]
		public virtual string LowPercentageEmails
		{
			get;
			set;
		}

		public virtual decimal LowPercentageThreshold
		{
			get;
			set;
		}

		public bool? NewCustomer
		{
			get;
			set;
		}

		[MaxLength(450)]
		public virtual string NewCustomerEmails
		{
			get;
			set;
		}

		public bool? NewMessage
		{
			get;
			set;
		}

		[MaxLength(450)]
		public virtual string NewMessageEmails
		{
			get;
			set;
		}

		public bool? NewOrder
		{
			get;
			set;
		}

		[MaxLength(450)]
		public virtual string NewOrderEmails
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

		public virtual int TenantId
		{
			get;
			set;
		}

		public TenantNotifications()
		{
		}
	}
}