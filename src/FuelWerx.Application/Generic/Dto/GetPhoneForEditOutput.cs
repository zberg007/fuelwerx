using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class GetPhoneForEditOutput : IOutputDto, IDto
	{
		public string OwnerName
		{
			get;
			set;
		}

		public PhoneEditDto Phone
		{
			get;
			set;
		}

		public GetPhoneForEditOutput()
		{
		}
	}
}