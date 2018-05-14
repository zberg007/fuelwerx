using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Authorization;
using FuelWerx.Authorization.Dto;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Authorization.Users.Exporting;
using FuelWerx.Customers;
using FuelWerx.Dto;
using FuelWerx.Generic;
using FuelWerx.MultiTenancy;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Authorization.Users
{
	public class UserAppService : FuelWerxAppServiceBase, IUserAppService, IApplicationService, ITransientDependency
	{
		private readonly RoleManager _roleManager;

		private readonly IUserEmailer _userEmailer;

		private readonly IRepository<UserSettingData, long> _userSettingDataRepository;

		private readonly IRepository<Customer, long> _customerRepository;

		private readonly IUserListExcelExporter _userListExcelExporter;

		private readonly IRoleAppService _roleAppService;

		private readonly IRepository<Role> _roleRepository;

		public UserAppService(RoleManager roleManager, IUserEmailer userEmailer, IRepository<Role> roleRepository, IRepository<UserSettingData, long> userSettingDataRepository, IRepository<Customer, long> customerRepository, IUserListExcelExporter userListExcelExporter, IRoleAppService roleAppService)
		{
			this._roleManager = roleManager;
			this._userEmailer = userEmailer;
			this._userSettingDataRepository = userSettingDataRepository;
			this._customerRepository = customerRepository;
			this._userListExcelExporter = userListExcelExporter;
			this._roleAppService = roleAppService;
			this._roleRepository = roleRepository;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Users" })]
		public async Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
		{
			long? id;
			if (input.User.Id.HasValue)
			{
				await this.UpdateUserAsync(input);
			}
			else
			{
				await this.CreateUserAsync(input);
				input.User.Id = new long?(this.UserManager.FindByName<User, long>(input.User.UserName).Id);
			}
			IRepository<UserSettingData, long> repository = this._userSettingDataRepository;
			UserSettingData userSettingDatum = await repository.FirstOrDefaultAsync((UserSettingData m) => m.UserId == input.User.Id.Value);
			UserSettingData postLoginViewType = userSettingDatum;
			if (postLoginViewType == null)
			{
				UserSettingDataEditDto userSettingData = input.UserSettingData;
				id = input.User.Id;
				userSettingData.UserId = id.Value;
				await this._userSettingDataRepository.InsertAsync(input.UserSettingData.MapTo<UserSettingData>());
			}
			else
			{
				postLoginViewType.PostLoginViewType = input.UserSettingData.PostLoginViewType;
				postLoginViewType.ShowScreencastAtLogin = input.UserSettingData.ShowScreencastAtLogin;
				postLoginViewType.StatusGoNoGo = input.UserSettingData.StatusGoNoGo;
				id = input.User.Id;
				postLoginViewType.UserId = id.Value;
				await this._userSettingDataRepository.UpdateAsync(postLoginViewType);
			}
			int? convertToUserCustomerId = input.ConvertToUserCustomerId;
			if (convertToUserCustomerId.HasValue)
			{
				convertToUserCustomerId = input.ConvertToUserCustomerId;
				if (convertToUserCustomerId.Value > 0)
				{
					IRepository<Customer, long> repository1 = this._customerRepository;
					convertToUserCustomerId = input.ConvertToUserCustomerId;
					Customer async = await repository1.GetAsync((long)convertToUserCustomerId.Value);
					if (async != null)
					{
						id = input.User.Id;
						async.UserId = new long?(id.Value);
						await this._customerRepository.UpdateAsync(async);
					}
					convertToUserCustomerId = null;
					input.ConvertToUserCustomerId = convertToUserCustomerId;
				}
			}
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Users.Create" })]
		protected virtual async Task CreateUserAsync(CreateOrUpdateUserInput input)
		{
			User tenantId = input.User.MapTo<User>();
			tenantId.TenantId = this.AbpSession.TenantId;
			if (input.User.Password.IsNullOrEmpty())
			{
				input.User.Password = User.CreateRandomPassword();
			}
			else
			{
				IdentityResult identityResult = await this.UserManager.PasswordValidator.ValidateAsync(input.User.Password);
				this.CheckErrors(identityResult);
			}
			tenantId.Password = (new PasswordHasher()).HashPassword(input.User.Password);
			tenantId.ShouldChangePasswordOnNextLogin = input.User.ShouldChangePasswordOnNextLogin;
			tenantId.Roles = new Collection<UserRole>();
			bool flag = false;
			string[] assignedRoleNames = input.AssignedRoleNames;
			for (int i = 0; i < (int)assignedRoleNames.Length; i++)
			{
				string str = assignedRoleNames[i];
				Role roleByNameAsync = await this._roleManager.GetRoleByNameAsync(str);
				ICollection<UserRole> roles = tenantId.Roles;
				roles.Add(new UserRole()
				{
					RoleId = roleByNameAsync.Id
				});
				if (roleByNameAsync.DisplayName == this.L("KeyName_CustomersRole"))
				{
					flag = true;
				}
			}
			assignedRoleNames = null;
			this.CheckErrors(await this.UserManager.CreateAsync(tenantId));
			await this.CurrentUnitOfWork.SaveChangesAsync();
			if (flag)
			{
				IRepository<Customer, long> repository = this._customerRepository;
				List<Customer> allListAsync = await repository.GetAllListAsync((Customer m) => (int?)m.TenantId == tenantId.TenantId && m.UserId.HasValue && m.UserId == (long?)tenantId.Id);
				if (!allListAsync.Any<Customer>())
				{
					Customer customer = new Customer()
					{
						AllowBillPay = false,
						BusinessName = null,
						FirstName = tenantId.Name,
						LastName = tenantId.Surname,
						Email = tenantId.EmailAddress,
						IsActive = true,
						PaymentAssistanceParticipant = false,
						TitleId = null,
						UserId = new long?(tenantId.Id),
						TenantId = tenantId.TenantId.Value
					};
					await this._customerRepository.InsertAsync(customer);
					await this.CurrentUnitOfWork.SaveChangesAsync();
				}
			}
			if (input.SendActivationEmail)
			{
				tenantId.SetNewEmailConfirmationCode();
				await this._userEmailer.SendEmailActivationLinkAsync(tenantId, input.User.Password);
			}
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Users.Delete" })]
		public async Task DeleteUser(IdInput<long> input)
		{
			User userByIdAsync = await this.UserManager.GetUserByIdAsync(input.Id);
			this.CheckErrors(await this.UserManager.DeleteAsync(userByIdAsync));
		}

        // FuelWerx.Authorization.Users.UserAppService
        private async Task FillRoleNames(List<UserListDto> userListDtos)
        {
            var enumerable = userListDtos.SelectMany(u => u.Roles, (user, role) => role.RoleId);
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            foreach (var current in enumerable)
            {
                string displayName = (await _roleManager.GetRoleByIdAsync(current)).DisplayName;
                dictionary[current] = displayName;
            }
            foreach (var user in userListDtos)
            {
                foreach (var role in user.Roles)
                {
                    role.RoleName = dictionary[role.RoleId];
                }
                user.Roles = user.Roles.OrderBy(r => r.RoleName).ToList();
            }
        }


        [AbpAuthorize(new string[] { "Pages.Administration.Users.Create", "Pages.Administration.Users.Edit" })]
		public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdInput<long> input)
		{
			IQueryable<Role> roles = this._roleManager.Roles;
			IOrderedQueryable<Role> displayName = 
				from r in roles
				orderby r.DisplayName
				select r;
			UserRoleDto[] arrayAsync = await (
				from r in displayName
				select new UserRoleDto()
				{
					RoleId = r.Id,
					RoleName = r.Name,
					RoleDisplayName = r.DisplayName
				}).ToArrayAsync<UserRoleDto>();
			UserRoleDto[] userRoleDtoArray = arrayAsync;
			GetUserForEditOutput getUserForEditOutput = new GetUserForEditOutput()
			{
				Roles = userRoleDtoArray
			};
			if (input.Id.HasValue)
			{
				FuelWerx.Authorization.Users.UserManager userManager = this.UserManager;
				long? id = input.Id;
				User userByIdAsync = await userManager.GetUserByIdAsync(id.Value);
				getUserForEditOutput.User = userByIdAsync.MapTo<UserEditDto>();
				getUserForEditOutput.ProfilePictureId = userByIdAsync.ProfilePictureId;
				IRepository<UserSettingData, long> repository = this._userSettingDataRepository;
				UserSettingData userSettingDatum = await repository.FirstOrDefaultAsync((UserSettingData m) => m.UserId == userByIdAsync.Id);
				UserSettingData userSettingDatum1 = userSettingDatum;
				if (userSettingDatum1 == null)
				{
					GetUserForEditOutput getUserForEditOutput1 = getUserForEditOutput;
					UserSettingData userSettingDatum2 = new UserSettingData()
					{
						UserId = userByIdAsync.Id
					};
					getUserForEditOutput1.UserSettingData = userSettingDatum2.MapTo<UserSettingDataEditDto>();
				}
				else
				{
					getUserForEditOutput.UserSettingData = userSettingDatum1.MapTo<UserSettingDataEditDto>();
				}
				UserRoleDto[] userRoleDtoArray1 = userRoleDtoArray;
				for (int i = 0; i < (int)userRoleDtoArray1.Length; i++)
				{
					UserRoleDto userRoleDto = userRoleDtoArray1[i];
					UserRoleDto userRoleDto1 = userRoleDto;
					FuelWerx.Authorization.Users.UserManager userManager1 = this.UserManager;
					id = input.Id;
					bool flag = await userManager1.IsInRoleAsync(id.Value, userRoleDto.RoleName);
					userRoleDto1.IsAssigned = flag;
					userRoleDto1 = null;
				}
				userRoleDtoArray1 = null;
			}
			else
			{
				GetUserForEditOutput getUserForEditOutput2 = getUserForEditOutput;
				UserEditDto userEditDto = new UserEditDto()
				{
					IsActive = true,
					ShouldChangePasswordOnNextLogin = true
				};
				getUserForEditOutput2.User = userEditDto;
				getUserForEditOutput.UserSettingData = new UserSettingDataEditDto()
				{
					UserId = (long)0
				};
				IQueryable<Role> roles1 = this._roleManager.Roles;
				List<Role> listAsync = await (
					from r in roles1
					where r.IsDefault
					select r).ToListAsync<Role>();
				foreach (Role role in listAsync)
				{
					UserRoleDto userRoleDto2 = userRoleDtoArray.FirstOrDefault<UserRoleDto>((UserRoleDto ur) => ur.RoleName == role.Name);
					if (userRoleDto2 == null)
					{
						continue;
					}
					userRoleDto2.IsAssigned = true;
				}
			}
			return getUserForEditOutput;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Users.ChangePermissions" })]
		public async Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(IdInput<long> input)
		{
			User userByIdAsync = await this.UserManager.GetUserByIdAsync(input.Id);
			IReadOnlyList<Permission> allPermissions = this.PermissionManager.GetAllPermissions(true);
			IReadOnlyList<Permission> grantedPermissionsAsync = await this.UserManager.GetGrantedPermissionsAsync(userByIdAsync);
			GetUserPermissionsForEditOutput getUserPermissionsForEditOutput = new GetUserPermissionsForEditOutput();
			List<FlatPermissionDto> flatPermissionDtos = allPermissions.MapTo<List<FlatPermissionDto>>();
			getUserPermissionsForEditOutput.Permissions = (
				from p in flatPermissionDtos
				orderby p.DisplayName
				select p).ToList<FlatPermissionDto>();
			IReadOnlyList<Permission> permissions = grantedPermissionsAsync;
			getUserPermissionsForEditOutput.GrantedPermissionNames = (
				from p in permissions
				select p.Name).ToList<string>();
			return getUserPermissionsForEditOutput;
		}

		public async Task<string> GetUserPostLoginViewType(long userId)
		{
			IRepository<UserSettingData, long> repository = this._userSettingDataRepository;
			UserSettingData userSettingDatum = await repository.FirstOrDefaultAsync((UserSettingData m) => m.UserId == userId);
			UserSettingData userSettingDatum1 = userSettingDatum;
			return (userSettingDatum1 == null ? string.Empty : userSettingDatum1.PostLoginViewType);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Users" })]
		public async Task<PagedResultOutput<UserListDto>> GetUsers(GetUsersInput input)
		{
			IQueryable<User> users = this.UserManager.Users;
			IQueryable<User> users1 = System.Data.Entity.QueryableExtensions.Include<User, ICollection<UserRole>>(users, (User u) => u.Roles);
			IQueryable<User> users2 = users1.WhereIf<User>(!input.Filter.IsNullOrWhiteSpace(), (User u) => u.Name.Contains(input.Filter) || u.Surname.Contains(input.Filter) || u.UserName.Contains(input.Filter) || u.EmailAddress.Contains(input.Filter));
			int num = await users2.CountAsync<User>();
			List<User> listAsync = await users2.OrderBy<User>(input.Sorting, new object[0]).PageBy<User>(input).ToListAsync<User>();
			List<UserListDto> userListDtos = listAsync.MapTo<List<UserListDto>>();
			await this.FillRoleNames(userListDtos);
			return new PagedResultOutput<UserListDto>(num, userListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Users" })]
		public async Task<FileDto> GetUsersToExcel()
		{
			IQueryable<User> users = this.UserManager.Users;
			List<User> listAsync = await System.Data.Entity.QueryableExtensions.Include<User, ICollection<UserRole>>(users, (User u) => u.Roles).ToListAsync<User>();
			List<UserListDto> userListDtos = listAsync.MapTo<List<UserListDto>>();
			await this.FillRoleNames(userListDtos);
			return this._userListExcelExporter.ExportToFile(userListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Users.ChangePermissions" })]
		public async Task ResetUserSpecificPermissions(IdInput<long> input)
		{
			User userByIdAsync = await this.UserManager.GetUserByIdAsync(input.Id);
			await this.UserManager.ResetAllPermissionsAsync(userByIdAsync);
		}

		public async Task<bool> ShowScreencastAtLogin(long userId)
		{
			bool flag;
			bool flag1;
			IRepository<UserSettingData, long> repository = this._userSettingDataRepository;
			UserSettingData userSettingDatum = await repository.FirstOrDefaultAsync((UserSettingData m) => m.UserId == userId);
			UserSettingData userSettingDatum1 = userSettingDatum;
			if (userSettingDatum1 == null)
			{
				flag = false;
			}
			else
			{
				flag1 = (userSettingDatum1.ShowScreencastAtLogin.HasValue ? userSettingDatum1.ShowScreencastAtLogin.Value : false);
				flag = flag1;
			}
			return flag;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Users.Edit" })]
		protected virtual async Task UpdateUserAsync(CreateOrUpdateUserInput input)
		{
			FuelWerx.Authorization.Users.UserManager userManager = this.UserManager;
			long? id = input.User.Id;
			User user = await userManager.FindByIdAsync(id.Value);
			input.User.MapTo<UserEditDto, User>(user);
			if (!input.User.Password.IsNullOrEmpty())
			{
				IdentityResult identityResult = await this.UserManager.ChangePasswordAsync(user, input.User.Password);
				this.CheckErrors(identityResult);
			}
			this.CheckErrors(await this.UserManager.UpdateAsync(user));
			IRepository<Customer, long> repository = this._customerRepository;
			List<Customer> allListAsync = await repository.GetAllListAsync((Customer m) => (int?)m.TenantId == user.TenantId && m.UserId.HasValue && m.UserId == (long?)user.Id);
			bool flag = allListAsync.Any<Customer>();
			string str = this.L("KeyName_CustomersRole");
			IRepository<Role> repository1 = this._roleRepository;
			List<Role> roles = await repository1.GetAllListAsync((Role m) => m.TenantId == (int?)((this.AbpSession.ImpersonatorTenantId.HasValue ? this.AbpSession.ImpersonatorTenantId.Value : this.AbpSession.TenantId.Value)) && m.DisplayName == str);
			List<Role> roles1 = roles;
			IdentityResult identityResult1 = await this.UserManager.SetRoles(user, input.AssignedRoleNames);
			this.CheckErrors(identityResult1);
			if (!flag && roles1.Count == 1 && input.AssignedRoleNames.Contains<string>(roles1[0].Name))
			{
				Customer customer = new Customer()
				{
					AllowBillPay = false,
					BusinessName = null,
					FirstName = user.Name,
					LastName = user.Surname,
					Email = user.EmailAddress,
					IsActive = true,
					PaymentAssistanceParticipant = false
				};
				id = null;
				customer.TitleId = id;
				customer.UserId = new long?(user.Id);
				customer.TenantId = user.TenantId.Value;
				await this._customerRepository.InsertAsync(customer);
				await this.CurrentUnitOfWork.SaveChangesAsync();
			}
			if (input.SendActivationEmail)
			{
				user.SetNewEmailConfirmationCode();
				await this._userEmailer.SendEmailActivationLinkAsync(user, input.User.Password);
			}
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Users.ChangePermissions" })]
		public async Task UpdateUserPermissions(UpdateUserPermissionsInput input)
		{
			User userByIdAsync = await this.UserManager.GetUserByIdAsync(input.Id);
			IEnumerable<Permission> permissionsFromNamesByValidating = this.PermissionManager.GetPermissionsFromNamesByValidating(input.GrantedPermissionNames);
			await this.UserManager.SetGrantedPermissionsAsync(userByIdAsync, permissionsFromNamesByValidating);
		}
	}
}