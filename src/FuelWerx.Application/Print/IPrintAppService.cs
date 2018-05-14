using Abp.Application.Services;
using Abp.Dependency;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Print
{
	public interface IPrintAppService : IApplicationService, ITransientDependency
	{
		Task<string> GetEstimateForPrint(long input);

		Task<string> GetInvoiceForPrint(long input);

		Task<string> GetProjectForPrint(long input);

		Task<bool> SendEstimate(long input);

		Task<bool> SendInvoice(long input);

		Task<bool> SendProject(long input);

		Task<string> ViewEstimateForPDF(long input);

		Task<string> ViewInvoiceForPDF(long input);

		Task<string> ViewProjectForPDF(long input);
	}
}