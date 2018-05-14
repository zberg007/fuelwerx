using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Contacts.Dto
{
	public class GetContactForEditOutput : IOutputDto, IDto
	{
		public ContactEditDto Contact
		{
			get;
			set;
		}

		public byte[] Image
		{
			get;
			set;
		}

		public GetContactForEditOutput()
		{
		}
	}
}