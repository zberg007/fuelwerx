using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Customers;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Customers.Dto
{
	[AutoMapFrom(new Type[] { typeof(Customer) })]
	public class CustomerListDto : FullAuditedEntityDto
	{
		public virtual bool AllowBillPay
		{
			get;
			set;
		}

		public string BusinessName
		{
			get;
			set;
		}

		public virtual string Email
		{
			get;
			set;
		}

		public string FirstName
		{
			get;
			set;
		}

		public string FullName
		{
			get
			{
				return string.Concat(this.FirstName, " ", this.LastName);
			}
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public string LastName
		{
			get;
			set;
		}

		public virtual bool PaymentAssistanceParticipant
		{
			get;
			set;
		}

		public virtual long? UserId
		{
			get;
			set;
		}

		public CustomerListDto()
		{
		}
	}
}