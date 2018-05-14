using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Organizations.Dto
{
	public class GetOrganizationUnitUsersInput : PagedAndSortedInputDto, IShouldNormalize
	{
		[Range(1, 9.22337203685478E+18)]
		public long Id
		{
			get;
			set;
		}

		public GetOrganizationUnitUsersInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "Name,Surname";
			}
		}
	}
}