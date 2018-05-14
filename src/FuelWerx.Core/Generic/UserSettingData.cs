using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxUserSettingData")]
	public class UserSettingData : FullAuditedEntity<long>
	{
		public const int MaxPostLoginViewTypeLength = 40;

		[Required]
		public virtual bool IsActive
		{
			get;
			set;
		}

		[MaxLength(40)]
		[Required]
		public virtual string PostLoginViewType
		{
			get;
			set;
		}

		public virtual bool? ShowScreencastAtLogin
		{
			get;
			set;
		}

		public virtual bool? StatusGoNoGo
		{
			get;
			set;
		}

		[Required]
		public virtual long UserId
		{
			get;
			set;
		}

		public UserSettingData()
		{
		}
	}
}