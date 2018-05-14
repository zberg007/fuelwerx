using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new System.Type[] { typeof(Address) })]
	public class AddressListDto : FullAuditedEntityDto
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

		public FuelWerx.Generic.Country Country
		{
			get;
			set;
		}

		public virtual int CountryId
		{
			get;
			set;
		}

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

		public AddressListDto()
		{
		}
	}
}