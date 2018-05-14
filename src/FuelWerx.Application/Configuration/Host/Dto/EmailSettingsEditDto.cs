using Abp.Runtime.Validation;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Host.Dto
{
	public class EmailSettingsEditDto : IValidate
	{
		public string DefaultFromAddress
		{
			get;
			set;
		}

		public string DefaultFromDisplayName
		{
			get;
			set;
		}

		public string SmtpDomain
		{
			get;
			set;
		}

		public bool SmtpEnableSsl
		{
			get;
			set;
		}

		public string SmtpHost
		{
			get;
			set;
		}

		public string SmtpPassword
		{
			get;
			set;
		}

		public int SmtpPort
		{
			get;
			set;
		}

		public bool SmtpUseDefaultCredentials
		{
			get;
			set;
		}

		public string SmtpUserName
		{
			get;
			set;
		}

		public EmailSettingsEditDto()
		{
		}
	}
}