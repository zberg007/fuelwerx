using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Trucks;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.Trucks.Dto
{
	[AutoMapFrom(new Type[] { typeof(Truck) })]
	public class TruckListDto : FullAuditedEntityDto
	{
		public virtual decimal Capacity
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public Guid? ImageId
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public virtual string Number
		{
			get;
			set;
		}

		public virtual decimal RemainingCapacity
		{
			get;
			set;
		}

		public TruckListDto()
		{
		}
	}
}