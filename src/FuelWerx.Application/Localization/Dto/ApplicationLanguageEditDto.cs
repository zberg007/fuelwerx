using Abp.AutoMapper;
using Abp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Localization.Dto
{
	[AutoMapFrom(new Type[] { typeof(ApplicationLanguage) })]
	public class ApplicationLanguageEditDto
	{
		[StringLength(128)]
		public virtual string Icon
		{
			get;
			set;
		}

		public virtual int? Id
		{
			get;
			set;
		}

		[Required]
		[StringLength(10)]
		public virtual string Name
		{
			get;
			set;
		}

		public ApplicationLanguageEditDto()
		{
		}
	}
}