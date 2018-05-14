using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Suppliers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Suppliers.Dto
{
	[AutoMapTo(new Type[] { typeof(Supplier) })]
	public class SupplierEditDto : IValidate, IPassivable
	{
		[Required]
		[StringLength(255)]
		public virtual string Address
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public virtual string City
		{
			get;
			set;
		}

		[RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage="Format must be valid email address (user@domain.tld).")]
		[StringLength(255)]
		public virtual string ContactEmail
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

		public virtual string Description
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

		public virtual Guid? LogoId
		{
			get;
			set;
		}

		[RegularExpression("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}", ErrorMessage="Format must be valid phone number (444-444-4444).")]
		[StringLength(16)]
		public virtual string MobilePhoneNumber
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public virtual string Name
		{
			get;
			set;
		}

		[RegularExpression("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}", ErrorMessage="Format must be valid phone number (555-555-5555).")]
		[StringLength(16)]
		public virtual string PhoneNumber
		{
			get;
			set;
		}

		[StringLength(255)]
		public virtual string PostalCode
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

		public SupplierEditDto()
		{
		}
	}
}