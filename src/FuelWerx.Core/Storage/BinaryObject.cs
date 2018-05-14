using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Storage
{
	[Table("AppBinaryObjects")]
	public class BinaryObject : Entity<Guid>
	{
		[Required]
		public byte[] Bytes
		{
			get;
			set;
		}

		public BinaryObject()
		{
			this.Id = Guid.NewGuid();
		}

		public BinaryObject(byte[] bytes) : this()
		{
			this.Bytes = bytes;
		}
	}
}