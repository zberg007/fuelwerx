using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using FuelWerx;
using FuelWerx.Auditing.Dto;
using FuelWerx.Auditing.Exporting;
using FuelWerx.Authorization.Users;
using FuelWerx.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Auditing
{
	[AbpAuthorize(new string[] { "Pages.Administration.AuditLogs" })]
	[DisableAuditing]
	public class AuditLogAppService : FuelWerxAppServiceBase, IAuditLogAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<AuditLog, long> _auditLogRepository;

		private readonly IRepository<User, long> _userRepository;

		private readonly IAuditLogListExcelExporter _auditLogListExcelExporter;

		public AuditLogAppService(IRepository<AuditLog, long> auditLogRepository, IRepository<User, long> userRepository, IAuditLogListExcelExporter auditLogListExcelExporter)
		{
			this._auditLogRepository = auditLogRepository;
			this._userRepository = userRepository;
			this._auditLogListExcelExporter = auditLogListExcelExporter;
		}

		private static List<AuditLogListDto> ConvertToAuditLogListDtos(List<AuditLogAndUser> results)
		{
			return results.Select<AuditLogAndUser, AuditLogListDto>((AuditLogAndUser result) => {
				AuditLogListDto auditLogListDto = result.AuditLog.MapTo<AuditLogListDto>();
				auditLogListDto.UserName = (result.User == null ? null : result.User.UserName);
				auditLogListDto.ServiceName = AuditLogAppService.StripNameSpace(auditLogListDto.ServiceName);
				return auditLogListDto;
			}).ToList<AuditLogListDto>();
		}


        private IQueryable<AuditLogAndUser> CreateAuditLogAndUsersQuery(GetAuditLogsInput input)
        {
            var auditLogs = from auditLog in this._auditLogRepository.GetAll()
                            join user in this._userRepository.GetAll() on auditLog.UserId equals (long?)user.Id into userJoin
                            from joinedUser in userJoin.DefaultIfEmpty<User>()
                            where auditLog.ExecutionTime >= input.StartDate && auditLog.ExecutionTime < input.EndDate
                            select new AuditLogAndUser
                            {
                                AuditLog = auditLog,
                                User = joinedUser
                            };
            return auditLogs
                .WhereIf(!input.UserName.IsNullOrWhiteSpace(), i => i.User.UserName.Contains(input.UserName))
                .WhereIf(!input.ServiceName.IsNullOrWhiteSpace(), i => i.AuditLog.ServiceName.Contains(input.ServiceName))
                .WhereIf(!input.MethodName.IsNullOrWhiteSpace(), i => i.AuditLog.MethodName.Contains(input.MethodName))
                .WhereIf(!input.BrowserInfo.IsNullOrWhiteSpace(), i => i.AuditLog.BrowserInfo.Contains(input.BrowserInfo))
                .WhereIf(input.MinExecutionDuration.HasValue && input.MinExecutionDuration > 0, i => i.AuditLog.ExecutionDuration >= input.MinExecutionDuration.Value)
                .WhereIf(input.MaxExecutionDuration.HasValue && input.MaxExecutionDuration < 2147483647, i => i.AuditLog.ExecutionDuration <= input.MaxExecutionDuration.Value)
                .WhereIf(input.HasException == true, i => i.AuditLog.Exception != null && i.AuditLog.Exception != "")
                .WhereIf(input.HasException == false, i => i.AuditLog.Exception == null || i.AuditLog.Exception == "");
        }

        public async Task<PagedResultOutput<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input)
		{
			IQueryable<AuditLogAndUser> auditLogAndUsers = this.CreateAuditLogAndUsersQuery(input);
			int num = await auditLogAndUsers.CountAsync<AuditLogAndUser>();
			List<AuditLogAndUser> listAsync = await auditLogAndUsers.AsNoTracking<AuditLogAndUser>().OrderBy<AuditLogAndUser>(input.Sorting, new object[0]).PageBy<AuditLogAndUser>(input).ToListAsync<AuditLogAndUser>();
			return new PagedResultOutput<AuditLogListDto>(num, AuditLogAppService.ConvertToAuditLogListDtos(listAsync));
		}

		public async Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input)
		{
			IQueryable<AuditLogAndUser> auditLogAndUsers = this.CreateAuditLogAndUsersQuery(input).AsNoTracking<AuditLogAndUser>();
			List<AuditLogAndUser> listAsync = await (
				from al in auditLogAndUsers
				orderby al.AuditLog.ExecutionTime descending
				select al).ToListAsync<AuditLogAndUser>();
			List<AuditLogListDto> auditLogListDtos = AuditLogAppService.ConvertToAuditLogListDtos(listAsync);
			return this._auditLogListExcelExporter.ExportToFile(auditLogListDtos);
		}

		private static string StripNameSpace(string serviceName)
		{
			if (serviceName.IsNullOrEmpty() || !serviceName.Contains("."))
			{
				return serviceName;
			}
			return serviceName.Substring(serviceName.LastIndexOf('.') + 1);
		}
	}
}