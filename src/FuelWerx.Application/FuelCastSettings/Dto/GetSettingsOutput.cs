using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.FuelCastSettings.Dto
{
	public class GetSettingsOutput : IOutputDto, IDto
	{
		public SettingsEditDto Settings
		{
			get;
			set;
		}

		public GetSettingsOutput()
		{
		}
	}
}