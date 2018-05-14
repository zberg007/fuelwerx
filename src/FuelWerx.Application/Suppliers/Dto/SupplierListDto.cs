using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Generic;
using FuelWerx.Suppliers;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Suppliers.Dto
{
	[AutoMapFrom(new Type[] { typeof(Supplier) })]
	public class SupplierListDto : FullAuditedEntityDto
	{
		public virtual string Address
		{
			get;
			set;
		}

		public virtual string City
		{
			get;
			set;
		}

		public virtual string ContactEmail
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

		public string Description
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public Guid? LogoId
		{
			get;
			set;
		}

		public virtual string MobilePhoneNumber
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public virtual string PhoneNumber
		{
			get;
			set;
		}

		public virtual string PostalCode
		{
			get;
			set;
		}

		public virtual string SecondaryAddress
		{
			get;
			set;
		}

		public SupplierListDto()
		{
		}
	}
}