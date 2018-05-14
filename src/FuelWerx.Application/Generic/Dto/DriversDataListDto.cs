using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new Type[] { typeof(DriversData) })]
	public class DriversDataListDto : FullAuditedEntityDto
	{
		public virtual string CDLExpiration
		{
			get;
			set;
		}

		public virtual string CDLNumber
		{
			get;
			set;
		}

		public virtual string HasHazmat
		{
			get;
			set;
		}

		public virtual bool IsActive
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

		public DriversDataListDto()
		{
		}
	}
}