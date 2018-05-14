using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Localization;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Localization.Dto
{
	[AutoMapFrom(new Type[] { typeof(ApplicationLanguage) })]
	public class ApplicationLanguageListDto : FullAuditedEntityDto
	{
		public virtual string DisplayName
		{
			get;
			set;
		}

		public virtual string Icon
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual int? TenantId
		{
			get;
			set;
		}

		public ApplicationLanguageListDto()
		{
		}
	}
}