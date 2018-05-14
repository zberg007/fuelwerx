using System;
using System.Threading.Tasks;

namespace FuelWerx.Authorization.Users
{
	public interface IUserEmailer
	{
		Task SendEmailActivationLinkAsync(User user, string plainPassword = null);

		Task SendPasswordResetLinkAsync(User user);
	}
}