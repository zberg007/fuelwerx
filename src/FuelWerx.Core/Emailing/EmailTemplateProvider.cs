using Abp.Dependency;
using Abp.IO.Extensions;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace FuelWerx.Emailing
{
	public class EmailTemplateProvider : IEmailTemplateProvider, ITransientDependency
	{
		public EmailTemplateProvider()
		{
		}

		public string GetDefaultTemplate()
		{
			string str;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FuelWerx.Emailing.EmailTemplates.default.html"))
			{
				byte[] allBytes = manifestResourceStream.GetAllBytes();
				str = Encoding.UTF8.GetString(allBytes, 3, (int)allBytes.Length - 3);
			}
			return str;
		}
	}
}