using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Sessions.Dto
{
	public class GetCurrentLoginInformationsOutput : IOutputDto, IDto
	{
		public TenantLoginInfoDto Tenant
		{
			get;
			set;
		}

		public UserLoginInfoDto User
		{
			get;
			set;
		}

		public GetCurrentLoginInformationsOutput()
		{
		}
	}
}