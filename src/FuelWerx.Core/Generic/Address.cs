using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxAddresses")]
	public class Address : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxTypeLength = 50;

		public const int MaxPrimaryAddressLength = 255;

		public const int MaxSecondaryAddressLength = 255;

		public const int MaxCityLength = 255;

		public const int MaxPostalCodeLength = 255;

		public const int MaxContactNameLength = 255;

		public const int MaxWeatherStationIdLength = 18;

		[MaxLength(255)]
		[Required]
		public virtual string City
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string ContactName
		{
			get;
			set;
		}

		[ForeignKey("CountryId")]
		public FuelWerx.Generic.Country Country
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

		[ForeignKey("CountryRegionId")]
		public FuelWerx.Generic.CountryRegion CountryRegion
		{
			get;
			set;
		}

		public virtual int? CountryRegionId
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

		[MaxLength(255)]
		public virtual string PostalCode
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string PrimaryAddress
		{
			get;
			set;
		}

		[MaxLength(255)]
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

		[MaxLength(50)]
		[Required]
		public virtual string Type
		{
			get;
			set;
		}

		[MaxLength(18)]
		public virtual string WeatherStationId
		{
			get;
			set;
		}

		public Address()
		{
		}
	}
}