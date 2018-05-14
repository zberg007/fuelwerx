using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class GetAddressForEditOutput : IOutputDto, IDto
	{
		public AddressEditDto Address
		{
			get;
			set;
		}

		public string OwnerName
		{
			get;
			set;
		}

		public GetAddressForEditOutput()
		{
		}
	}
}