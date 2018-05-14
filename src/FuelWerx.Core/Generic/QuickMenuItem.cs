using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxQuickMenuItems")]
	public class QuickMenuItem : Entity<long>
	{
		public const int MaxLabelLength = 120;

		public const int MaxUrlLength = 400;

		public const int MaxFaviconUrlLength = 400;

		[StringLength(400)]
		public virtual string FaviconUrl
		{
			get;
			set;
		}

		[StringLength(120)]
		public virtual string Label
		{
			get;
			set;
		}

		public virtual bool LoadInNewWindow
		{
			get;
			set;
		}

		public virtual long OwnerId
		{
			get;
			set;
		}

		[StringLength(400)]
		public virtual string Url
		{
			get;
			set;
		}

		public QuickMenuItem()
		{
		}
	}
}