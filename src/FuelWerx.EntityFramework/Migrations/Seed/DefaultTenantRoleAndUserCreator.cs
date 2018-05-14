using Abp.Application.Editions;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using FuelWerx.Authorization;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.EntityFramework;
using FuelWerx.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations.Seed
{
	public class DefaultTenantRoleAndUserCreator
	{
		private readonly FuelWerxDbContext _context;

		public DefaultTenantRoleAndUserCreator(FuelWerxDbContext context)
		{
			this._context = context;
		}

		public void Create()
		{
			this.CreateHostAndUsers();
			this.CreateDefaultTenantAndUsers();
		}

		private void CreateDefaultTenantAndUsers()
		{
			Tenant tenant = this._context.Tenants.FirstOrDefault<Tenant>((Tenant t) => t.TenancyName == "Default");
			if (tenant == null)
			{
				tenant = new Tenant("Default", "Default");
				Edition edition = this._context.Editions.FirstOrDefault<Edition>((Edition e) => e.Name == "Standard");
				if (edition != null)
				{
					tenant.EditionId = new int?(edition.Id);
				}
				tenant = this._context.Tenants.Add(tenant);
				this._context.SaveChanges();
			}
			Role role = this._context.Roles.FirstOrDefault<Role>((Role r) => r.TenantId == (int?)tenant.Id && r.Name == "Admin");
			if (role == null)
			{
				role = this._context.Roles.Add(new Role(new int?(tenant.Id), "Admin", "Admin")
				{
					IsStatic = true
				});
				this._context.SaveChanges();
			}
			if (this._context.Roles.FirstOrDefault<Role>((Role r) => r.TenantId == (int?)tenant.Id && r.Name == "User") == null)
			{
				this._context.Roles.Add(new Role(new int?(tenant.Id), "User", "User")
				{
					IsStatic = true,
					IsDefault = true
				});
				this._context.SaveChanges();
			}
			if (this._context.Roles.FirstOrDefault<Role>((Role r) => r.TenantId == (int?)tenant.Id && r.Name == "Driver") == null)
			{
				this._context.Roles.Add(new Role(new int?(tenant.Id), "Driver", "Driver")
				{
					IsStatic = true,
					IsDefault = false
				});
				this._context.SaveChanges();
			}
			if (this._context.Roles.FirstOrDefault<Role>((Role r) => r.TenantId == (int?)tenant.Id && r.Name == "Customer") == null)
			{
				this._context.Roles.Add(new Role(new int?(tenant.Id), "Customer", "Customer")
				{
					IsStatic = true,
					IsDefault = true
				});
				this._context.SaveChanges();
			}
			User user = this._context.Users.FirstOrDefault<User>((User u) => u.TenantId == (int?)tenant.Id && u.UserName == "admin");
			if (user == null)
			{
				user = User.CreateTenantAdminUser(tenant.Id, "admin@defaulttenant.com", "FuelWerx!");
				user.IsEmailConfirmed = true;
				user.ShouldChangePasswordOnNextLogin = true;
				user.IsActive = true;
				this._context.Users.Add(user);
				this._context.SaveChanges();
				this._context.UserRoles.Add(new UserRole(user.Id, role.Id));
				this._context.SaveChanges();
				foreach (Permission list in (
					from p in PermissionFinder.GetAllPermissions(new AuthorizationProvider[] { new AppAuthorizationProvider() })
					where p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant)
					select p).ToList<Permission>())
				{
					if (list.IsGrantedByDefault)
					{
						continue;
					}
					this._context.Permissions.Add(new RolePermissionSetting()
					{
						Name = list.Name,
						IsGranted = true,
						RoleId = role.Id
					});
				}
				this._context.SaveChanges();
			}
		}

		private void CreateHostAndUsers()
		{
			IDbSet<Role> roles = this._context.Roles;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Role), "r");
			int? nullable = null;
			Role role = roles.FirstOrDefault<Role>(Expression.Lambda<Func<Role, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(AbpRoleBase).GetMethod("get_TenantId").MethodHandle)), Expression.Constant(nullable, typeof(int?))), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(AbpRoleBase).GetMethod("get_Name").MethodHandle)), Expression.Constant("Admin", typeof(string)))), new ParameterExpression[] { parameterExpression }));
			if (role == null)
			{
				IDbSet<Role> roles1 = this._context.Roles;
				nullable = null;
				role = roles1.Add(new Role(nullable, "Admin", "Admin")
				{
					IsStatic = true,
					IsDefault = true
				});
				this._context.SaveChanges();
			}
			IDbSet<User> users = this._context.Users;
			parameterExpression = Expression.Parameter(typeof(User), "u");
			nullable = null;
			if (users.FirstOrDefault<User>(Expression.Lambda<Func<User, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(AbpUserBase).GetMethod("get_TenantId").MethodHandle)), Expression.Constant(nullable, typeof(int?))), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(AbpUserBase).GetMethod("get_UserName").MethodHandle)), Expression.Constant("admin", typeof(string)))), new ParameterExpression[] { parameterExpression })) == null)
			{
				IDbSet<User> users1 = this._context.Users;
				User user = new User();
				nullable = null;
				user.TenantId = nullable;
				user.UserName = "admin";
				user.Name = "admin";
				user.Surname = "admin";
				user.EmailAddress = "app@fuelwerx.com";
				user.IsEmailConfirmed = true;
				user.ShouldChangePasswordOnNextLogin = true;
				user.IsActive = true;
				user.Password = "AIMBiVeqfYtuzXVVmZf5eoUFTIDJtMpVz1g+k84ahK7C/yMoFdwW2uIhTmj9czvHFg==";
				User user1 = users1.Add(user);
				this._context.SaveChanges();
				this._context.UserRoles.Add(new UserRole(user1.Id, role.Id));
				this._context.SaveChanges();
				foreach (Permission list in (
					from p in PermissionFinder.GetAllPermissions(new AuthorizationProvider[] { new AppAuthorizationProvider() })
					where p.MultiTenancySides.HasFlag(MultiTenancySides.Host)
					select p).ToList<Permission>())
				{
					if (list.IsGrantedByDefault)
					{
						continue;
					}
					this._context.Permissions.Add(new RolePermissionSetting()
					{
						Name = list.Name,
						IsGranted = true,
						RoleId = role.Id
					});
				}
				this._context.SaveChanges();
			}
		}
	}
}