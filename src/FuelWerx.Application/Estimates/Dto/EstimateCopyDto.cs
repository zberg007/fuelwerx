using FuelWerx.Customers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	public class EstimateCopyDto
	{
		[ForeignKey("CustomerId")]
		public virtual FuelWerx.Customers.Customer Customer
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string CustomerEmail
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string CustomerFirstName
		{
			get;
			set;
		}

		[Required]
		public virtual long CustomerId
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string CustomerLastName
		{
			get;
			set;
		}

		[Required]
		public long EstimateId
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

		[MaxLength(38)]
		[Required]
		public virtual string Number
		{
			get;
			set;
		}

		public EstimateCopyDto()
		{
		}
	}
}