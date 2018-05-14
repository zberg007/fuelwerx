using Abp.AutoMapper;
using FuelWerx.Administrative.Titles.Dto;
using FuelWerx.Customers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Customers.Dto
{
	[AutoMapTo(new Type[] { typeof(Customer) })]
	public class CustomerDto
	{
		public virtual string BusinessName
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
				return string.Concat(this.FirstName, (this.LastName.Length > 0 ? string.Concat(" ", this.LastName) : string.Empty));
			}
		}

		public long Id
		{
			get;
			set;
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

		public Guid? LogoId
		{
			get;
			set;
		}

		[ForeignKey("TitleId")]
		public virtual TitleDto Title
		{
			get;
			set;
		}

		public virtual long? TitleId
		{
			get;
			set;
		}

		public CustomerDto()
		{
		}
	}
}