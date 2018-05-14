using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Titles.Dto
{
	public class GetTitleForEditOutput : IOutputDto, IDto
	{
		public byte[] Image
		{
			get;
			set;
		}

		public TitleEditDto Title
		{
			get;
			set;
		}

		public GetTitleForEditOutput()
		{
		}
	}
}