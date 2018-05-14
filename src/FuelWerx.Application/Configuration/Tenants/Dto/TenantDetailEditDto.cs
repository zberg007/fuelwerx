using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.Generic.Dto;
using FuelWerx.Tenants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	[AutoMapTo(new Type[] { typeof(TenantDetail) })]
	public class TenantDetailEditDto : IValidate
	{
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

		[ForeignKey("CountryId")]
		public CountryDto Country
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

		[MaxLength(255)]
		[RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage="Format must be valid email address (user@domain.tld).")]
		public virtual string Email
		{
			get;
			set;
		}

		[MaxLength(16)]
		[RegularExpression("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}", ErrorMessage="Format must be valid phone number (444-444-4444).")]
		public virtual string FaxNumber
		{
			get;
			set;
		}

		public virtual long? Id
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

		[MaxLength(255)]
		[Required]
		public virtual string MailAddress
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string MailCity
		{
			get;
			set;
		}

		[ForeignKey("CountryId")]
		public CountryDto MailCountry
		{
			get;
			set;
		}

		[Required]
		public virtual int MailCountryId
		{
			get;
			set;
		}

		[ForeignKey("CountryRegionId")]
		public CountryRegionDto MailCountryRegion
		{
			get;
			set;
		}

		public virtual int? MailCountryRegionId
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string MailPostalCode
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string MailSecondaryAddress
		{
			get;
			set;
		}

		public virtual string Note
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

		[MaxLength(16)]
		[RegularExpression("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}", ErrorMessage="Format must be valid phone number (444-444-4444).")]
		public virtual string PhoneNumberSecondary
		{
			get;
			set;
		}

		public virtual Guid? PictureId
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

		public TenantDetailEditDto()
		{
		}
	}
}