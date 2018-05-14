using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Trucks;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Assets.Trucks.Dto
{
	[AutoMapTo(new Type[] { typeof(Truck) })]
	public class TruckEditDto : IValidate, IPassivable
	{
		[Required]
		public virtual decimal Capacity
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

		public virtual Guid? ImageId
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

		[Required]
		[StringLength(255)]
		public virtual string Name
		{
			get;
			set;
		}

		[Required]
		[StringLength(16)]
		public virtual string Number
		{
			get;
			set;
		}

		public TruckEditDto()
		{
		}
	}
}