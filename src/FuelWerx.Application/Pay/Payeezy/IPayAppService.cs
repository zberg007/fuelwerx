using Abp.Application.Services;
using Abp.Dependency;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Pay.Payeezy
{
	public interface IPayAppService : IApplicationService, ITransientDependency
	{
		Task<bool> EndPayment(long input);

		Task<bool> StartPayment(long input);
	}
}