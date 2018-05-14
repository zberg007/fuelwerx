using Abp;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using FuelWerx;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Dto;
using FuelWerx.Editions.Dto;
using FuelWerx.MultiTenancy.Demo;
using FuelWerx.MultiTenancy.Dto;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.MultiTenancy
{
	[AbpAuthorize(new string[] { "Pages.Tenants" })]
	public class TenantAppService : FuelWerxAppServiceBase, ITenantAppService, IApplicationService, ITransientDependency
	{
		private readonly RoleManager _roleManager;

		private readonly IUserEmailer _userEmailer;

		private readonly TenantDemoDataBuilder _demoDataBuilder;

		public TenantAppService(RoleManager roleManager, IUserEmailer userEmailer, TenantDemoDataBuilder demoDataBuilder)
		{
			this._roleManager = roleManager;
			this._userEmailer = userEmailer;
			this._demoDataBuilder = demoDataBuilder;
		}

		[AbpAuthorize(new string[] { "Pages.Tenants.Create" })]
		public async Task CreateTenant(CreateTenantInput input)
		{
			Tenant tenant = new Tenant(input.TenancyName, input.Name)
			{
				IsActive = input.IsActive,
				EditionId = input.EditionId
			};
			Tenant tenant1 = tenant;
			this.CheckErrors(await this.TenantManager.CreateAsync(tenant1));
			await this.CurrentUnitOfWork.SaveChangesAsync();
            using (CurrentUnitOfWork.SetFilterParameter(AbpDataFilters.MayHaveTenant, "tenantId", tenant1.Id))
            {
                IdentityResult identityResult = await this._roleManager.CreateStaticRoles(tenant1.Id);
                this.CheckErrors(identityResult);
                await this.CurrentUnitOfWork.SaveChangesAsync();
                IQueryable<Role> roles = this._roleManager.Roles;
                Role role = roles.Single<Role>((Role r) => r.Name == "Admin");
                await this._roleManager.GrantAllPermissionsAsync(role);
                IQueryable<Role> roles1 = this._roleManager.Roles;
                Role role1 = roles1.Single<Role>((Role r) => r.Name == "User");
                role1.IsDefault = true;
                this.CheckErrors(await this._roleManager.UpdateAsync(role1));
                if (input.AdminPassword.IsNullOrEmpty())
                {
                    input.AdminPassword = User.CreateRandomPassword();
                }
                User shouldChangePasswordOnNextLogin = User.CreateTenantAdminUser(tenant1.Id, input.AdminEmailAddress, input.AdminPassword);
                shouldChangePasswordOnNextLogin.ShouldChangePasswordOnNextLogin = input.ShouldChangePasswordOnNextLogin;
                shouldChangePasswordOnNextLogin.IsActive = input.IsActive;
                this.CheckErrors(await this.UserManager.CreateAsync(shouldChangePasswordOnNextLogin));
                await this.CurrentUnitOfWork.SaveChangesAsync();
                IdentityResult roleAsync = await this.UserManager.AddToRoleAsync(shouldChangePasswordOnNextLogin.Id, role.Name);
                this.CheckErrors(roleAsync);
                if (input.SendActivationEmail)
                {
                    shouldChangePasswordOnNextLogin.SetNewEmailConfirmationCode();
                    await this._userEmailer.SendEmailActivationLinkAsync(shouldChangePasswordOnNextLogin, input.AdminPassword);
                }
                await this.CurrentUnitOfWork.SaveChangesAsync();
                await this._demoDataBuilder.BuildForAsync(tenant1);
                role = null;
                shouldChangePasswordOnNextLogin = null;
            }
		}

		[AbpAuthorize(new string[] { "Pages.Tenants.Delete" })]
		public async Task DeleteTenant(EntityRequestInput input)
		{
			Tenant byIdAsync = await this.TenantManager.GetByIdAsync(input.Id);
			this.CheckErrors(await this.TenantManager.DeleteAsync(byIdAsync));
		}

		[AbpAuthorize(new string[] { "Pages.Tenants.ChangeFeatures" })]
		public async Task<GetTenantFeaturesForEditOutput> GetTenantFeaturesForEdit(EntityRequestInput input)
		{
			IReadOnlyList<Feature> all = this.FeatureManager.GetAll();
			IReadOnlyList<NameValue> featureValuesAsync = await this.TenantManager.GetFeatureValuesAsync(input.Id);
			GetTenantFeaturesForEditOutput getTenantFeaturesForEditOutput = new GetTenantFeaturesForEditOutput();
			List<FlatFeatureDto> flatFeatureDtos = all.MapTo<List<FlatFeatureDto>>();
			getTenantFeaturesForEditOutput.Features = (
				from f in flatFeatureDtos
				orderby f.DisplayName
				select f).ToList<FlatFeatureDto>();
			IReadOnlyList<NameValue> nameValues = featureValuesAsync;
			getTenantFeaturesForEditOutput.FeatureValues = (
				from fv in nameValues
				select new NameValueDto(fv)).ToList<NameValueDto>();
			return getTenantFeaturesForEditOutput;
		}

		[AbpAuthorize(new string[] { "Pages.Tenants.Edit" })]
		public async Task<TenantEditDto> GetTenantForEdit(EntityRequestInput input)
		{
			Tenant byIdAsync = await this.TenantManager.GetByIdAsync(input.Id);
			return byIdAsync.MapTo<TenantEditDto>();
		}

		public async Task<PagedResultOutput<TenantListDto>> GetTenants(GetTenantsInput input)
		{
			IQueryable<Tenant> tenants = this.TenantManager.Tenants;
			IQueryable<Tenant> tenants1 = System.Data.Entity.QueryableExtensions.Include<Tenant, Edition>(tenants, (Tenant t) => t.Edition);
			IQueryable<Tenant> tenants2 = tenants1.WhereIf<Tenant>(!input.Filter.IsNullOrWhiteSpace(), (Tenant t) => t.Name.Contains(input.Filter) || t.TenancyName.Contains(input.Filter));
			int num = await tenants2.CountAsync<Tenant>();
			List<Tenant> listAsync = await tenants2.OrderBy<Tenant>(input.Sorting, new object[0]).PageBy<Tenant>(input).ToListAsync<Tenant>();
			return new PagedResultOutput<TenantListDto>(num, listAsync.MapTo<List<TenantListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Tenants.ChangeFeatures" })]
		public async Task ResetTenantSpecificFeatures(EntityRequestInput input)
		{
			await this.TenantManager.ResetAllFeaturesAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenants.Edit" })]
		public async Task UpdateTenant(TenantEditDto input)
		{
			Tenant byIdAsync = await this.TenantManager.GetByIdAsync(input.Id);
			input.MapTo<TenantEditDto, Tenant>(byIdAsync);
			this.CheckErrors(await this.TenantManager.UpdateAsync(byIdAsync));
		}

		[AbpAuthorize(new string[] { "Pages.Tenants.ChangeFeatures" })]
		public async Task UpdateTenantFeatures(UpdateTenantFeaturesInput input)
		{
			FuelWerx.MultiTenancy.TenantManager tenantManager = this.TenantManager;
			int id = input.Id;
			List<NameValueDto> featureValues = input.FeatureValues;
			await tenantManager.SetFeatureValuesAsync(id, (
				from fv in featureValues
				select new NameValue(fv.Name, fv.Value)).ToArray<NameValue>());
		}
	}
}