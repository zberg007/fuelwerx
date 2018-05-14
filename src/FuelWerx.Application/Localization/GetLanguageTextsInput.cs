using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Localization
{
	public class GetLanguageTextsInput : IInputDto, IDto, IValidate, IPagedResultRequest, ILimitedResultRequest, ISortedResultRequest, IShouldNormalize
	{
		[StringLength(10)]
		public string BaseLanguageName
		{
			get;
			set;
		}

		public string FilterText
		{
			get;
			set;
		}

		[Range(0, 2147483647)]
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

		[MaxLength(128)]
		[Required]
		public string SourceName
		{
			get;
			set;
		}

		[Required]
		[StringLength(10, MinimumLength=2)]
		public string TargetLanguageName
		{
			get;
			set;
		}

		public string TargetValueFilter
		{
			get;
			set;
		}

		public GetLanguageTextsInput()
		{
		}

		public void Normalize()
		{
			if (this.TargetValueFilter.IsNullOrEmpty())
			{
				this.TargetValueFilter = "ALL";
			}
		}
	}
}