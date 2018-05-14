using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapTo(new System.Type[] { typeof(Address) })]
	public class AddressEditDto : IValidate, IPassivable
	{
		[Required]
		[StringLength(255)]
		public virtual string City
		{
			get;
			set;
		}

		[StringLength(255)]
		public virtual string ContactName
		{
			get;
			set;
		}

		[Required]
		public virtual int CountryId
		{
			get;
			set;
		}

		public virtual int? CountryRegionId
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		[Required]
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

		[Required]
		public virtual long OwnerId
		{
			get;
			set;
		}

		[Required]
		public virtual string OwnerType
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public virtual string PostalCode
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public virtual string PrimaryAddress
		{
			get;
			set;
		}

		[StringLength(255)]
		public virtual string SecondaryAddress
		{
			get;
			set;
		}

		[Required]
		[StringLength(50)]
		public virtual string Type
		{
			get;
			set;
		}

		public virtual string WeatherStationId
		{
			get;
			set;
		}

		public AddressEditDto()
		{
		}
	}
}