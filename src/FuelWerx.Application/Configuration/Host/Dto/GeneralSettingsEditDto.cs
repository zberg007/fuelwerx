using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Host.Dto
{
	public class GeneralSettingsEditDto : IValidate
	{
		[MaxLength(128)]
		public string WebSiteRootAddress
		{
			get;
			set;
		}

		public GeneralSettingsEditDto()
		{
		}
	}
}