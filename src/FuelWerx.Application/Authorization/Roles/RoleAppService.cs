using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Authorization;
using FuelWerx.Authorization.Dto;
using FuelWerx.Authorization.Roles.Dto;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Authorization.Roles
{
	[AbpAuthorize(new string[] { "Pages.Administration.Roles" })]
	public class RoleAppService : FuelWerxAppServiceBase, IRoleAppService, IApplicationService, ITransientDependency
	{
		private readonly RoleManager _roleManager;

		public RoleAppService(RoleManager roleManager)
		{
			this._roleManager = roleManager;
		}

		public async Task CreateOrUpdateRole(CreateOrUpdateRoleInput input)
		{
			if (!input.Role.Id.HasValue)
			{
				await this.CreateRoleAsync(input);
			}
			else
			{
				await this.UpdateRoleAsync(input);
			}
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Roles.Create" })]
		protected virtual async Task CreateRoleAsync(CreateOrUpdateRoleInput input)
		{
			Role role = new Role(this.AbpSession.TenantId, input.Role.DisplayName)
			{
				IsDefault = input.Role.IsDefault
			};
			Role role1 = role;
			this.CheckErrors(await this._roleManager.CreateAsync(role1));
			await this.CurrentUnitOfWork.SaveChangesAsync();
			await this.UpdateGrantedPermissionsAsync(role1, input.GrantedPermissionNames);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Roles.Delete" })]
		public async Task DeleteRole(EntityRequestInput input)
		{
			Role roleByIdAsync = await this._roleManager.GetRoleByIdAsync(input.Id);
			this.CheckErrors(await this._roleManager.DeleteAsync(roleByIdAsync));
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Roles.Create", "Pages.Administration.Roles.Edit" })]
		public async Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdInput input)
		{
			RoleEditDto roleEditDto;
			IReadOnlyList<Permission> allPermissions = this.PermissionManager.GetAllPermissions(true);
			Permission[] array = new Permission[0];
			if (!input.Id.HasValue)
			{
				roleEditDto = new RoleEditDto();
			}
			else
			{
				RoleManager roleManager = this._roleManager;
				Role roleByIdAsync = await roleManager.GetRoleByIdAsync(input.Id.Value);
				IReadOnlyList<Permission> grantedPermissionsAsync = await this._roleManager.GetGrantedPermissionsAsync(roleByIdAsync);
				array = grantedPermissionsAsync.ToArray<Permission>();
				roleEditDto = roleByIdAsync.MapTo<RoleEditDto>();
				roleByIdAsync = null;
			}
			GetRoleForEditOutput getRoleForEditOutput = new GetRoleForEditOutput()
			{
				Role = roleEditDto
			};
			List<FlatPermissionDto> flatPermissionDtos = allPermissions.MapTo<List<FlatPermissionDto>>();
			getRoleForEditOutput.Permissions = (
				from p in flatPermissionDtos
				orderby p.DisplayName
				select p).ToList<FlatPermissionDto>();
			Permission[] permissionArray = array;
			getRoleForEditOutput.GrantedPermissionNames = (
				from p in (IEnumerable<Permission>)permissionArray
				select p.Name).ToList<string>();
			return getRoleForEditOutput;
		}

		public async Task<ListResultOutput<RoleListDto>> GetRoles()
		{
			List<Role> listAsync = await this._roleManager.Roles.ToListAsync<Role>();
			return new ListResultOutput<RoleListDto>(listAsync.MapTo<List<RoleListDto>>());
		}

		private async Task UpdateGrantedPermissionsAsync(Role role, List<string> grantedPermissionNames)
		{
			IEnumerable<Permission> permissionsFromNamesByValidating = this.PermissionManager.GetPermissionsFromNamesByValidating(grantedPermissionNames);
			await this._roleManager.SetGrantedPermissionsAsync(role, permissionsFromNamesByValidating);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Roles.Edit" })]
		protected virtual async Task UpdateRoleAsync(CreateOrUpdateRoleInput input)
		{
			RoleManager roleManager = this._roleManager;
			int? id = input.Role.Id;
			Role roleByIdAsync = await roleManager.GetRoleByIdAsync(id.Value);
			roleByIdAsync.DisplayName = input.Role.DisplayName;
			roleByIdAsync.IsDefault = input.Role.IsDefault;
			await this.UpdateGrantedPermissionsAsync(roleByIdAsync, input.GrantedPermissionNames);
		}
	}
}