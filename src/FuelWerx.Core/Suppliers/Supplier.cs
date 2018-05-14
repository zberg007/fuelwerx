using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Suppliers
{
	[Table("FuelWerxSuppliers")]
	public class Supplier : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNameLength = 255;

		public const int MaxPhoneNumberLength = 16;

		public const int MaxMobilePhoneNumberLength = 16;

		public const int MaxAddressLength = 255;

		public const int MaxSecondaryAddressLength = 255;

		public const int MaxCityLength = 255;

		public const int MaxPostalCodeLength = 255;

		public const int MaxContactNameLength = 255;

		public const int MaxContactEmailLength = 255;

		[MaxLength(255)]
		[Required]
		public virtual string Address
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string City
		{
			get;
			set;
		}

		[MaxLength(255)]
		[RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage="Format must be valid email address (user@domain.tld).")]
		public virtual string ContactEmail
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

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string Description
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

		public virtual Guid? LogoId
		{
			get;
			set;
		}

		[MaxLength(16)]
		[RegularExpression("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}", ErrorMessage="Format must be valid phone number (444-444-4444).")]
		public virtual string MobilePhoneNumber
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string Name
		{
			get;
			set;
		}

		[MaxLength(16)]
		[RegularExpression("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}", ErrorMessage="Format must be valid phone number (444-444-4444).")]
		public virtual string PhoneNumber
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

		public Supplier()
		{
		}
	}
}