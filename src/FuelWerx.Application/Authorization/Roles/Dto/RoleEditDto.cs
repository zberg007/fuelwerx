using Abp.AutoMapper;
using FuelWerx.Authorization.Roles;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Roles.Dto
{
	[AutoMap(new Type[] { typeof(Role) })]
	public class RoleEditDto
	{
		[Required]
		public string DisplayName
		{
			get;
			set;
		}

		public int? Id
		{
			get;
			set;
		}

		public bool IsDefault
		{
			get;
			set;
		}

		public RoleEditDto()
		{
		}
	}
}