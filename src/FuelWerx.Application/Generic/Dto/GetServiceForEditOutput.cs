using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class GetServiceForEditOutput : IOutputDto, IDto
	{
		public string OwnerName
		{
			get;
			set;
		}

		public ServiceEditDto Service
		{
			get;
			set;
		}

		public GetServiceForEditOutput()
		{
		}
	}
}