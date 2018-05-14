using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Dto
{
	public class PagedAndSortedInputDto : IInputDto, IDto, IValidate, IPagedResultRequest, ILimitedResultRequest, ISortedResultRequest
	{
		[Range(1, 1000)]
		public int MaxResultCount
		{
			get;
			set;
		}

		[Range(0, 2147483647)]
		public int SkipCount
		{
			get;
			set;
		}

		public string Sorting
		{
			get;
			set;
		}

		public PagedAndSortedInputDto()
		{
			this.MaxResultCount = 20;
		}
	}
}