using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Dto
{
	[AutoMapTo(new Type[] { typeof(UserSettingData) })]
	public class UserSettingDataEditDto : IValidate, IPassivable
	{
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

		[Required]
		[StringLength(40)]
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

		public UserSettingDataEditDto()
		{
		}
	}
}