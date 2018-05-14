using System;

namespace FuelWerx.Emailing
{
	public interface IEmailTemplateProvider
	{
		string GetDefaultTemplate();
	}
}