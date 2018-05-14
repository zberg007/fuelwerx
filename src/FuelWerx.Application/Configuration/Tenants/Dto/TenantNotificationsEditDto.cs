using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.Tenants;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	[AutoMapFrom(new Type[] { typeof(TenantNotifications) })]
	public class TenantNotificationsEditDto : IValidate
	{
		public long? Id
		{
			get;
			set;
		}

		public bool? LowPercentage
		{
			get;
			set;
		}

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

		public virtual string NewOrderEmails
		{
			get;
			set;
		}

		[Required]
		public virtual int TenantId
		{
			get;
			set;
		}

		public TenantNotificationsEditDto()
		{
		}
	}
}