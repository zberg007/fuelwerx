using Abp.Application.Editions;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Editions.Dto
{
	[AutoMapFrom(new Type[] { typeof(Edition) })]
	public class EditionListDto : EntityDto, IHasCreationTime
	{
		public DateTime CreationTime
		{
			get;
			set;
		}

		public string DisplayName
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public EditionListDto()
		{
		}
	}
}