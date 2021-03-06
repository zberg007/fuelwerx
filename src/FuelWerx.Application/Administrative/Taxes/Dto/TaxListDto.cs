using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Taxes.Dto
{
	[AutoMapFrom(new Type[] { typeof(Tax) })]
	public class TaxListDto : FullAuditedEntityDto
	{
		public virtual string Caption
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual decimal Rate
		{
			get;
			set;
		}

		public TaxListDto()
		{
		}
	}
}