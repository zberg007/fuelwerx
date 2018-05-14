using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapTo(new System.Type[] { typeof(Address) })]
	public class AddressDto
	{
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

		[ForeignKey("CountryId")]
		public CountryDto Country
		{
			get;
			set;
		}

		public virtual int CountryId
		{
			get;
			set;
		}

		[ForeignKey("CountryRegionId")]
		public CountryRegionDto CountryRegion
		{
			get;
			set;
		}

		public virtual int? CountryRegionId
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

		public virtual double? Latitude
		{
			get;
			set;
		}

		public DbGeography Location
		{
			get;
			set;
		}

		public virtual double? Longitude
		{
			get;
			set;
		}

		public virtual long OwnerId
		{
			get;
			set;
		}

		public virtual string OwnerType
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

		public virtual int TenantId
		{
			get;
			set;
		}

		public virtual string Type
		{
			get;
			set;
		}

		public AddressDto()
		{
		}
	}
}