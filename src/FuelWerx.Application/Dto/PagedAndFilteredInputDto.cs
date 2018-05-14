using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Dto
{
	public class PagedAndFilteredInputDto : IInputDto, IDto, IValidate, IPagedResultRequest, ILimitedResultRequest
	{
		public string Filter
		{
			get;
			set;
		}

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

		public PagedAndFilteredInputDto()
		{
			this.MaxResultCount = 20;
		}
	}
}