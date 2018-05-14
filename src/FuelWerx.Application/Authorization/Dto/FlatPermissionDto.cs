using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Dto
{
	[AutoMapFrom(new Type[] { typeof(Permission) })]
	public class FlatPermissionDto : IDto
	{
		public string Description
		{
			get;
			set;
		}

		public string DisplayName
		{
			get;
			set;
		}

		public bool IsGrantedByDefault
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string ParentName
		{
			get;
			set;
		}

		public FlatPermissionDto()
		{
		}
	}
}