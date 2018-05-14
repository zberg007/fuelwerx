using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxPaymentSettings")]
	public class PaymentSetting : Entity<long>, IMustHaveTenant
	{
		public virtual string Setting
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public PaymentSetting()
		{
		}
	}
}