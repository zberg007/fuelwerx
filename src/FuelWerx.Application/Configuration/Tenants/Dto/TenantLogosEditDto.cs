using Abp.AutoMapper;
using Abp.Runtime.Validation;
using FuelWerx.Tenants;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	[AutoMapTo(new Type[] { typeof(TenantLogos) })]
	public class TenantLogosEditDto : IValidate
	{
		public virtual bool? ClearHeaderImageId
		{
			get;
			set;
		}

		public virtual bool? ClearHeaderMobileImageId
		{
			get;
			set;
		}

		public virtual bool? ClearInvoiceImageId
		{
			get;
			set;
		}

		public virtual bool? ClearMailImageId
		{
			get;
			set;
		}

		public virtual Guid? HeaderImageId
		{
			get;
			set;
		}

		public virtual Guid? HeaderMobileImageId
		{
			get;
			set;
		}

		public virtual Guid? InvoiceImageId
		{
			get;
			set;
		}

		public virtual Guid? MailImageId
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public TenantLogosEditDto()
		{
		}
	}
}