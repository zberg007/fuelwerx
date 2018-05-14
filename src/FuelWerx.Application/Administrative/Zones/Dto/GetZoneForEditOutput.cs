using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Zones.Dto
{
	public class GetZoneForEditOutput : IOutputDto, IDto
	{
		public ZoneEditDto Zone
		{
			get;
			set;
		}

		public GetZoneForEditOutput()
		{
		}
	}
}