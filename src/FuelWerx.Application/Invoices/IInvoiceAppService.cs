using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Dto;
using FuelWerx.Invoices.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelWerx.Invoices
{
	public interface IInvoiceAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateInvoice(CreateOrUpdateInvoiceInput input);

		Task CreateOrUpdateInvoiceTeamMembers(CreateOrUpdateInvoiceTeamMemberInput input);

		Task DeleteInvoice(IdInput<long> input);

		Task DeleteInvoiceResource(IdInput<long> input);

		Task<Invoice> GetInvoice(long invoiceId);

		Task<GetInvoiceForCopyOutput> GetInvoiceForCopy(IdInput<long> input);

		Task<GetInvoiceForEditOutput> GetInvoiceForEdit(NullableIdInput<long> input);

		Task<GetInvoicePaymentForAddOutput> GetInvoicePaymentForCreateOrView(NullableIdInput<long> input);

		Task<PagedResultOutput<InvoicePaymentListDto>> GetInvoicePayments(GetInvoicePaymentsInput input);

		Task<List<InvoicePaymentListDto>> GetInvoicePaymentsByInvoiceId(long invoiceId);

		Task<InvoiceResourceEditDto> GetInvoiceResourceDetailsByBinaryObjectId(Guid resourceId);

		Task<GetInvoiceResourceForEditOutput> GetInvoiceResourcesForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<InvoiceListDto>> GetInvoices(GetInvoicesInput input);

		Task<PagedResultOutput<InvoiceListDto>> GetInvoicesForAddress(GetCustomerInvoicesInput input);

		Task<FileDto> GetInvoicesToExcel();

		Task<GetInvoiceTeamMembersForEditOutput> GetInvoiceTeamMembersForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<InvoicePaymentListDto>> GetPayments(GetInvoicePaymentsInput input);

		Task<FileDto> GetPaymentsToExcel();

		Task<ListResultDto<UserListDto>> GetTeamMembersByTenantId(int tenantId, bool active);

		Task SaveInvoiceResourceAsync(UpdateInvoiceResourceInput updateInvoiceResourceInput);

		Task SaveInvoiceResourceDetails(long estimateResourceId, string name, string description, bool isActive);
	}
}