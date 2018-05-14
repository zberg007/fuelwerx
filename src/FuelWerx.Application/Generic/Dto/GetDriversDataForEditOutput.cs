using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class GetDriversDataForEditOutput : IOutputDto, IDto
	{
		public DriversDataEditDto DriversData
		{
			get;
			set;
		}

		public string DriversDataName
		{
			get;
			set;
		}

		public GetDriversDataForEditOutput()
		{
		}
	}
}