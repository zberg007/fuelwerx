using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.Organizations;
using Abp.Runtime.Session;
using FuelWerx;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Customers;
using FuelWerx.Dto;
using FuelWerx.MultiTenancy;
using FuelWerx.Organizations.Dto;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Organizations
{
    [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits" })]
    public class OrganizationUnitAppService : FuelWerxAppServiceBase, IOrganizationUnitAppService, IApplicationService, ITransientDependency
    {
        private readonly OrganizationUnitManager _organizationUnitManager;

        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;

        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;

        private readonly IRepository<OrganizationUnitProperties, long> _organizationUnitPropertiesRepository;

        private readonly IRepository<Customer, long> _customerRepository;

        private readonly FuelWerx.Authorization.Users.UserManager _userManager;

        public OrganizationUnitAppService(OrganizationUnitManager organizationUnitManager, IRepository<OrganizationUnit, long> organizationUnitRepository, IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository, IRepository<OrganizationUnitProperties, long> organizationUnitPropertiesRepository, IRepository<Customer, long> customerRepository, FuelWerx.Authorization.Users.UserManager userManager)
        {
            this._organizationUnitManager = organizationUnitManager;
            this._organizationUnitRepository = organizationUnitRepository;
            this._userOrganizationUnitRepository = userOrganizationUnitRepository;
            this._organizationUnitPropertiesRepository = organizationUnitPropertiesRepository;
            this._customerRepository = customerRepository;
            this._userManager = userManager;
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageMembers" })]
        public async Task AddUserToOrganizationUnit(UserToOrganizationUnitInput input)
        {
            await this.UserManager.AddToOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageOrganizationTree" })]
        public async Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {
            OrganizationUnit organizationUnit = new OrganizationUnit(this.AbpSession.TenantId, input.DisplayName, input.ParentId);
            await this._organizationUnitManager.CreateAsync(organizationUnit);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return organizationUnit.MapTo<OrganizationUnitDto>();
        }

        private async Task<OrganizationUnitDto> CreateOrganizationUnitDto(OrganizationUnit organizationUnit)
        {
            OrganizationUnitDto organizationUnitDto = organizationUnit.MapTo<OrganizationUnitDto>();
            OrganizationUnitDto organizationUnitDto1 = organizationUnitDto;
            IRepository<UserOrganizationUnit, long> repository = this._userOrganizationUnitRepository;
            int num = await repository.CountAsync((UserOrganizationUnit uou) => uou.OrganizationUnitId == organizationUnit.Id);
            organizationUnitDto1.MemberCount = num;
            organizationUnitDto1 = null;
            return organizationUnitDto;
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageOrganizationTree" })]
        public async Task DeleteOrganizationUnit(IdInput<long> input)
        {
            IQueryable<OrganizationUnitProperties> all = this._organizationUnitPropertiesRepository.GetAll();
            OrganizationUnitProperties organizationUnitProperty = await (
                from oup in all
                where oup.OrganizationUnitId == input.Id
                select oup).FirstOrDefaultAsync<OrganizationUnitProperties>();
            OrganizationUnitProperties organizationUnitProperty1 = organizationUnitProperty;
            if (organizationUnitProperty1 != null)
            {
                await this._organizationUnitPropertiesRepository.DeleteAsync(organizationUnitProperty1.Id);
            }
            await this._organizationUnitManager.DeleteAsync(input.Id);
        }

        public async Task<List<string>> GetDiscountForUsersOrganizationUnits(long customerId)
        {
            decimal? nullable;
            decimal num;
            bool flag;
            bool flag1;
            List<string> strs = new List<string>();
            decimal? nullable1 = null;
            decimal? nullable2 = null;
            Customer customer = await this._customerRepository.FirstOrDefaultAsync(customerId);
            if (customer != null && customer.UserId.HasValue)
            {
                FuelWerx.Authorization.Users.UserManager userManager = this._userManager;
                User userByIdAsync = await userManager.GetUserByIdAsync(customer.UserId.Value);
                List<OrganizationUnit> organizationUnitsAsync = await this._userManager.GetOrganizationUnitsAsync(userByIdAsync);
                List<long> list = (
                    from s in organizationUnitsAsync
                    select s.Id).ToList<long>();
                if (list.Any<long>())
                {
                    IRepository<OrganizationUnitProperties, long> repository = this._organizationUnitPropertiesRepository;
                    List<OrganizationUnitProperties> allListAsync = await repository.GetAllListAsync((OrganizationUnitProperties x) => list.Contains(x.OrganizationUnitId));
                    List<OrganizationUnitProperties> organizationUnitProperties = allListAsync;
                    if (organizationUnitProperties.Any<OrganizationUnitProperties>())
                    {
                        List<OrganizationUnitProperties> organizationUnitProperties1 = organizationUnitProperties;
                        IEnumerable<OrganizationUnitProperties> organizationUnitProperties2 = organizationUnitProperties1.Where<OrganizationUnitProperties>((OrganizationUnitProperties oup) =>
                        {
                            if (!oup.Discount.HasValue)
                            {
                                return false;
                            }
                            if (oup.Discount.Value <= decimal.Zero)
                            {
                                return false;
                            }
                            return oup.Discount.Value <= new decimal(0.999);
                        });
                        nullable1 = (
                            from oup in organizationUnitProperties2
                            select oup.Discount).Sum();
                        List<OrganizationUnitProperties> organizationUnitProperties3 = organizationUnitProperties;
                        IEnumerable<OrganizationUnitProperties> organizationUnitProperties4 = organizationUnitProperties3.Where<OrganizationUnitProperties>((OrganizationUnitProperties oup) =>
                        {
                            if (!oup.Discount.HasValue)
                            {
                                return false;
                            }
                            if (oup.Discount.Value <= decimal.Zero)
                            {
                                return false;
                            }
                            return oup.Discount.Value > new decimal(0.999);
                        });
                        nullable2 = (
                            from oup in organizationUnitProperties4
                            select oup.Discount).Sum();
                    }
                }
            }
            if (nullable1.HasValue)
            {
                nullable = nullable1;
                num = new decimal();
                flag1 = (nullable.GetValueOrDefault() > num ? nullable.HasValue : false);
                if (flag1)
                {
                    strs.Add(string.Concat(nullable1.ToString(), "%"));
                }
            }
            if (nullable2.HasValue)
            {
                nullable = nullable2;
                num = new decimal();
                flag = (nullable.GetValueOrDefault() > num ? nullable.HasValue : false);
                if (flag)
                {
                    strs.Add(string.Concat("$", nullable2.ToString()));
                }
            }
            return strs;
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageProperties" })]
        public async Task<OrganizationUnitPropertiesDto> GetOrganizationUnitProperties(long organizationUnitId)
        {
            IQueryable<OrganizationUnitProperties> all = this._organizationUnitPropertiesRepository.GetAll();
            OrganizationUnitProperties organizationUnitProperty = await (
                from ou in all
                where ou.OrganizationUnitId == organizationUnitId
                select ou).FirstOrDefaultAsync<OrganizationUnitProperties>();
            return organizationUnitProperty.MapTo<OrganizationUnitPropertiesDto>();
        }

        public async Task<ListResultOutput<OrganizationUnitDto>> GetOrganizationUnits()
        {
            var result = await (from ou in _organizationUnitRepository.GetAll()
                                join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
                                select new
                                {
                                    ou = ou,
                                    memberCount = g.Count()
                                }).ToListAsync();
            ListResultOutput<OrganizationUnitDto> listResultOutput = new ListResultOutput<OrganizationUnitDto>(result.Select((item) =>
            {
                OrganizationUnitDto organizationUnitDto = item.ou.MapTo<OrganizationUnitDto>();
                organizationUnitDto.MemberCount = item.memberCount;
                return organizationUnitDto;
            }).ToList());
            return listResultOutput;
        }

        public async Task<List<OrganizationUnitDto>> GetOrganizationUnitsByProperty(string propertyName, string propertyValue)
        {
            List<OrganizationUnitDto> organizationUnitDtos = new List<OrganizationUnitDto>();
            foreach (OrganizationUnitDto item in (await GetOrganizationUnits()).Items)
            {
                OrganizationUnitPropertiesDto organizationUnitProperties = await this.GetOrganizationUnitProperties(item.Id);
                if (organizationUnitProperties != null && organizationUnitProperties.Id > (long)0)
                {
                    string empty = string.Empty;
                    string str = propertyName;
                    if (str == "ShowPrice")
                    {
                        empty = organizationUnitProperties.ShowPrice.ToString();
                    }
                    else if (str == "SpecificPricesEnabled")
                    {
                        empty = organizationUnitProperties.SpecificPricesEnabled.ToString();
                    }
                    else if (str == "Upcharge")
                    {
                        empty = organizationUnitProperties.Upcharge.ToString();
                    }
                    else if (str == "Discount")
                    {
                        empty = organizationUnitProperties.Discount.ToString();
                    }
                    if (empty.ToLower() == propertyValue.ToLower())
                    {
                        organizationUnitDtos.Add(item);
                    }
                }
            }
            List<OrganizationUnitDto> organizationUnitDtos1 = organizationUnitDtos;
            List<OrganizationUnitDto> organizationUnitDtos2 = (
                from x in organizationUnitDtos1
                orderby x.DisplayName
                select x).ToList<OrganizationUnitDto>().MapTo<List<OrganizationUnitDto>>();
            return organizationUnitDtos2;
        }

        public async Task<List<OrganizationUnitDto>> GetOrganizationUnitsForUser(long userId)
        {
            User userByIdAsync = await this._userManager.GetUserByIdAsync(userId);
            List<OrganizationUnit> organizationUnitsAsync = await this._userManager.GetOrganizationUnitsAsync(userByIdAsync);
            return organizationUnitsAsync.MapTo<List<OrganizationUnitDto>>();
        }

        public async Task<PagedResultOutput<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input)
        {
            var result = from uou in _userOrganizationUnitRepository.GetAll()
                         join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                         join user in UserManager.Users on uou.UserId equals user.Id
                         where uou.OrganizationUnitId == input.Id
                         orderby input.Sorting
                         select new
                         {
                             uou,
                             user
                         };
            int resultCount = await result.CountAsync();
            var pagedResult = await result.PageBy(input).ToListAsync();
            PagedResultOutput<OrganizationUnitUserListDto> pagedResultOutput = new PagedResultOutput<OrganizationUnitUserListDto>(resultCount, pagedResult.Select((item) =>
            {
                OrganizationUnitUserListDto creationTime = item.user.MapTo<OrganizationUnitUserListDto>();
                creationTime.AddedTime = item.uou.CreationTime;
                return creationTime;
            }).ToList());
            return pagedResultOutput;
        }

        public async Task<List<string>> GetUpchargeForUsersOrganizationUnits(long customerId)
        {
            decimal? nullable;
            decimal num;
            bool flag;
            bool flag1;
            List<string> strs = new List<string>();
            decimal? nullable1 = null;
            decimal? nullable2 = null;
            Customer customer = await this._customerRepository.FirstOrDefaultAsync(customerId);
            if (customer != null && customer.UserId.HasValue)
            {
                FuelWerx.Authorization.Users.UserManager userManager = this._userManager;
                User userByIdAsync = await userManager.GetUserByIdAsync(customer.UserId.Value);
                List<OrganizationUnit> organizationUnitsAsync = await this._userManager.GetOrganizationUnitsAsync(userByIdAsync);
                List<long> list = (
                    from s in organizationUnitsAsync
                    select s.Id).ToList<long>();
                if (list.Any<long>())
                {
                    IRepository<OrganizationUnitProperties, long> repository = this._organizationUnitPropertiesRepository;
                    List<OrganizationUnitProperties> allListAsync = await repository.GetAllListAsync((OrganizationUnitProperties x) => list.Contains(x.OrganizationUnitId));
                    List<OrganizationUnitProperties> organizationUnitProperties = allListAsync;
                    if (organizationUnitProperties.Any<OrganizationUnitProperties>())
                    {
                        List<OrganizationUnitProperties> organizationUnitProperties1 = organizationUnitProperties;
                        IEnumerable<OrganizationUnitProperties> organizationUnitProperties2 = organizationUnitProperties1.Where<OrganizationUnitProperties>((OrganizationUnitProperties oup) =>
                        {
                            if (!oup.Upcharge.HasValue)
                            {
                                return false;
                            }
                            if (oup.Upcharge.Value <= decimal.Zero)
                            {
                                return false;
                            }
                            return oup.Upcharge.Value <= new decimal(0.999);
                        });
                        nullable1 = (
                            from oup in organizationUnitProperties2
                            select oup.Upcharge).Sum();
                        List<OrganizationUnitProperties> organizationUnitProperties3 = organizationUnitProperties;
                        IEnumerable<OrganizationUnitProperties> organizationUnitProperties4 = organizationUnitProperties3.Where<OrganizationUnitProperties>((OrganizationUnitProperties oup) =>
                        {
                            if (!oup.Upcharge.HasValue)
                            {
                                return false;
                            }
                            if (oup.Upcharge.Value <= decimal.Zero)
                            {
                                return false;
                            }
                            return oup.Upcharge.Value > new decimal(0.999);
                        });
                        nullable2 = (
                            from oup in organizationUnitProperties4
                            select oup.Upcharge).Sum();
                    }
                }
            }
            if (nullable1.HasValue)
            {
                nullable = nullable1;
                num = new decimal();
                flag1 = (nullable.GetValueOrDefault() > num ? nullable.HasValue : false);
                if (flag1)
                {
                    strs.Add(string.Concat(nullable1.ToString(), "%"));
                }
            }
            if (nullable2.HasValue)
            {
                nullable = nullable2;
                num = new decimal();
                flag = (nullable.GetValueOrDefault() > num ? nullable.HasValue : false);
                if (flag)
                {
                    strs.Add(string.Concat("$", nullable2.ToString()));
                }
            }
            return strs;
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageMembers" })]
        public async Task<bool> IsInOrganizationUnit(UserToOrganizationUnitInput input)
        {
            bool flag = await this.UserManager.IsInOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
            return flag;
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageProperties" })]
        public bool IsTenantOrganizationUnit()
        {
            if (base.AbpSession.MultiTenancySide != MultiTenancySides.Tenant)
            {
                return false;
            }
            return true;
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageOrganizationTree" })]
        public async Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input)
        {
            await this._organizationUnitManager.MoveAsync(input.Id, input.NewParentId);
            OrganizationUnit async = await this._organizationUnitRepository.GetAsync(input.Id);
            return await this.CreateOrganizationUnitDto(async);
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageMembers" })]
        public async Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input)
        {
            await this.UserManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageOrganizationTree" })]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input)
        {
            OrganizationUnit async = await this._organizationUnitRepository.GetAsync(input.Id);
            async.DisplayName = input.DisplayName;
            await this._organizationUnitManager.UpdateAsync(async);
            return await this.CreateOrganizationUnitDto(async);
        }

        [AbpAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageProperties" })]
        public async Task<OrganizationUnitPropertiesDto> UpdateOrganizationUnitProperties(UpdateOrganizationUnitPropertiesInput input)
        {
            OrganizationUnitProperties organizationUnitProperty = new OrganizationUnitProperties()
            {
                OrganizationUnitId = input.OrganizationUnitId,
                Discount = input.Discount,
                Upcharge = input.Upcharge,
                ShowPrice = input.ShowPrice,
                SpecificPricesEnabled = input.SpecificPricesEnabled,
                TenantId = this.AbpSession.GetTenantId()
            };
            if (input.Id.HasValue)
            {
                organizationUnitProperty.Id = input.Id.Value;
            }
            organizationUnitProperty.Id = await this._organizationUnitPropertiesRepository.InsertOrUpdateAndGetIdAsync(organizationUnitProperty);
            OrganizationUnitPropertiesDto organizationUnitPropertiesDto = organizationUnitProperty.MapTo<OrganizationUnitPropertiesDto>();
            OrganizationUnitPropertiesDto organizationUnitPropertiesDto1 = new OrganizationUnitPropertiesDto()
            {
                CreationTime = organizationUnitPropertiesDto.CreationTime,
                CreatorUserId = organizationUnitPropertiesDto.CreatorUserId,
                Discount = organizationUnitPropertiesDto.Discount,
                Id = organizationUnitPropertiesDto.Id,
                OrganizationUnitId = organizationUnitPropertiesDto.OrganizationUnitId,
                ShowPrice = organizationUnitPropertiesDto.ShowPrice,
                Upcharge = organizationUnitPropertiesDto.Upcharge,
                SpecificPricesEnabled = organizationUnitPropertiesDto.SpecificPricesEnabled,
                LastModificationTime = organizationUnitPropertiesDto.LastModificationTime,
                LastModifierUserId = organizationUnitPropertiesDto.LastModifierUserId
            };
            return organizationUnitPropertiesDto1;
        }
    }
}