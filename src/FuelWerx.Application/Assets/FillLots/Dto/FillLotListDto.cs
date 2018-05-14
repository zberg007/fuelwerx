using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.FillLots;
using FuelWerx.Generic.Dto;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.FillLots.Dto
{
	[AutoMapFrom(new Type[] { typeof(FillLot) })]
	public class FillLotListDto : FullAuditedEntityDto
	{
		[ForeignKey("AddressId")]
		public virtual AddressDto Address
		{
			get;
			set;
		}

		public virtual long AddressId
		{
			get;
			set;
		}

		public virtual string Description
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual string Label
		{
			get;
			set;
		}

		public virtual string ShortLabel
		{
			get;
			set;
		}

		public virtual int TankTotal
		{
			get;
			set;
		}

		public FillLotListDto()
		{
		}
	}
}