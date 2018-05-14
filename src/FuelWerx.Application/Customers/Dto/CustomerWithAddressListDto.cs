using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Customers.Dto
{
	public class CustomerWithAddressListDto
	{
		public virtual long AddressId
		{
			get;
			set;
		}

		public virtual bool AddressIsActive
		{
			get;
			set;
		}

		public virtual bool AllowBillPay
		{
			get;
			set;
		}

		public virtual string BusinessName
		{
			get;
			set;
		}

		public virtual string City
		{
			get;
			set;
		}

		public virtual string ContactName
		{
			get;
			set;
		}

		public virtual string CountryCode
		{
			get;
			set;
		}

		public virtual string CountryRegionCode
		{
			get;
			set;
		}

		public virtual DateTime? CreationTime
		{
			get;
			set;
		}

		public virtual long CustomerId
		{
			get;
			set;
		}

		public virtual string Email
		{
			get;
			set;
		}

		public virtual string FirstName
		{
			get;
			set;
		}

		public virtual string FullName
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

		public virtual string LastName
		{
			get;
			set;
		}

		public virtual double? Latitude
		{
			get;
			set;
		}

		public virtual double? Longitude
		{
			get;
			set;
		}

		public virtual bool PaymentAssistanceParticipant
		{
			get;
			set;
		}

		public virtual string PostalCode
		{
			get;
			set;
		}

		public virtual string PrimaryAddress
		{
			get;
			set;
		}

		public virtual string SecondaryAddress
		{
			get;
			set;
		}

		public virtual string Type
		{
			get;
			set;
		}

		public virtual long? UserId
		{
			get;
			set;
		}

		public CustomerWithAddressListDto()
		{
		}
	}
}