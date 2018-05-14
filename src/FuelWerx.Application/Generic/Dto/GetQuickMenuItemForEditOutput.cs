using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class GetQuickMenuItemForEditOutput : IOutputDto, IDto
	{
		public string OwnerName
		{
			get;
			set;
		}

		public QuickMenuItemEditDto QuickMenuItem
		{
			get;
			set;
		}

		public GetQuickMenuItemForEditOutput()
		{
		}
	}
}