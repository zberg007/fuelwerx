using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Suppliers.Dto
{
	public class GetSupplierForEditOutput : IOutputDto, IDto
	{
		public Guid? LogoId
		{
			get;
			set;
		}

		public SupplierEditDto Supplier
		{
			get;
			set;
		}

		public GetSupplierForEditOutput()
		{
		}
	}
}