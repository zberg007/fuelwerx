using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.FillLots;
using FuelWerx.FillLotTanks;
using FuelWerx.Generic.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.FillLots.Dto
{
	[AutoMapTo(new Type[] { typeof(FillLot) })]
	public class FillLotEditDto : IValidate, IPassivable
	{
		[ForeignKey("AddressId")]
		public virtual AddressDto Address
		{
			get;
			set;
		}

		[Required]
		public virtual long? AddressId
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

		[MaxLength(255)]
		[Required]
		public virtual string Label
		{
			get;
			set;
		}

		[MaxLength(12)]
		[Required]
		public virtual string ShortLabel
		{
			get;
			set;
		}

		public ICollection<FillLotTank> Tanks
		{
			get;
			set;
		}

		public virtual int? TenantId
		{
			get;
			set;
		}

		public FillLotEditDto()
		{
		}
	}
}