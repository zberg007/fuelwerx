using Abp;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.Organizations;
using FuelWerx;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using FuelWerx.Storage;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.MultiTenancy.Demo
{
    public class TenantDemoDataBuilder : FuelWerxServiceBase, ITransientDependency
    {
        private readonly OrganizationUnitManager _organizationUnitManager;

        private readonly UserManager _userManager;

        private readonly RandomUserGenerator _randomUserGenerator;

        private readonly IBinaryObjectManager _binaryObjectManager;

        private readonly IAppFolders _appFolders;

        public bool IsInDemoMode
        {
            get
            {
                return string.Equals(ConfigurationManager.AppSettings["AppDemoMode"], "true", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public TenantDemoDataBuilder(OrganizationUnitManager organizationUnitManager, UserManager userManager, RandomUserGenerator randomUserGenerator, IBinaryObjectManager binaryObjectManager, IAppFolders appFolders)
        {
            this._organizationUnitManager = organizationUnitManager;
            this._userManager = userManager;
            this._randomUserGenerator = randomUserGenerator;
            this._binaryObjectManager = binaryObjectManager;
            this._appFolders = appFolders;
        }

        public async Task BuildForAsync(Tenant tenant)
        {
            if (IsInDemoMode)
            {
                using (CurrentUnitOfWork.SetFilterParameter("MayHaveTenant", "tenantId", tenant.Id))
                {
                    await BuildForInternalAsync(tenant);
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
        }

        private async Task BuildForInternalAsync(Tenant tenant)
        {
            List<OrganizationUnit> organizationUnits = new List<OrganizationUnit>();
            OrganizationUnit organizationUnit = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Producing", null);
            OrganizationUnit organizationUnit1 = organizationUnit;
            OrganizationUnit organizationUnit2 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Research & Development", organizationUnit1);
            OrganizationUnit organizationUnit3 = organizationUnit2;
            OrganizationUnit organizationUnit4 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "IVR Related Products", organizationUnit3);
            OrganizationUnit organizationUnit5 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Voice Technologies", organizationUnit3);
            OrganizationUnit organizationUnit6 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Inhouse Projects", organizationUnit3);
            OrganizationUnit organizationUnit7 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Quality Management", organizationUnit1);
            OrganizationUnit organizationUnit8 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Testing", organizationUnit1);
            OrganizationUnit organizationUnit9 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Selling", null);
            OrganizationUnit organizationUnit10 = organizationUnit9;
            OrganizationUnit organizationUnit11 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Marketing", organizationUnit10);
            OrganizationUnit organizationUnit12 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Sales", organizationUnit10);
            OrganizationUnit organizationUnit13 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Customer Relations", organizationUnit10);
            OrganizationUnit organizationUnit14 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Supporting", null);
            OrganizationUnit organizationUnit15 = organizationUnit14;
            OrganizationUnit organizationUnit16 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Buying", organizationUnit15);
            OrganizationUnit organizationUnit17 = await this.CreateAndSaveOrganizationUnit(organizationUnits, tenant, "Human Resources", organizationUnit15);
            List<User> randomUsers = this._randomUserGenerator.GetRandomUsers(RandomHelper.GetRandom(12, 26), tenant.Id);
            foreach (User randomUser in randomUsers)
            {
                await this._userManager.CreateAsync(randomUser);
                await this.CurrentUnitOfWork.SaveChangesAsync();
                this._userManager.AddToRole<User, long>(randomUser.Id, "User");
                IEnumerable<OrganizationUnit> organizationUnits1 = MyRandomHelper.GenerateRandomizedList<OrganizationUnit>(organizationUnits).Take<OrganizationUnit>(RandomHelper.GetRandom(0, 3));
                foreach (OrganizationUnit organizationUnit18 in organizationUnits1)
                {
                    await this._userManager.AddToOrganizationUnitAsync(randomUser, organizationUnit18);
                }
                if (RandomHelper.GetRandom(100) < 70)
                {
                    await this.SetRandomProfilePictureAsync(randomUser);
                }
            }
            User user = this._userManager.FindByName<User, long>("admin");
            await this.SetRandomProfilePictureAsync(user);
        }

        private async Task<OrganizationUnit> CreateAndSaveOrganizationUnit(List<OrganizationUnit> organizationUnits, Tenant tenant, string displayName, OrganizationUnit parent = null)
        {
            long? nullable;
            int? nullable1 = new int?(tenant.Id);
            string str = displayName;
            if (parent == null)
            {
                nullable = null;
            }
            else
            {
                nullable = new long?(parent.Id);
            }
            OrganizationUnit organizationUnit = new OrganizationUnit(nullable1, str, nullable);
            await this._organizationUnitManager.CreateAsync(organizationUnit);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            organizationUnits.Add(organizationUnit);
            return organizationUnit;
        }

        private byte[] GetRandomProfilePictureBytes()
        {
            int random = RandomHelper.GetRandom(1, 11);
            string str = string.Format("sample-profile-{0}.jpg", random.ToString("00"));
            string str1 = Path.Combine(this._appFolders.SampleProfileImagesFolder, str);
            if (!File.Exists(str1))
            {
                throw new ApplicationException(string.Concat("Could not find sample profile picture on ", str1));
            }
            return File.ReadAllBytes(str1);
        }

        private async Task SetRandomProfilePictureAsync(User user)
        {
            try
            {
                var binaryObject = new BinaryObject(GetRandomProfilePictureBytes());
                await _binaryObjectManager.SaveAsync(binaryObject);
                user.ProfilePictureId = new Guid?(binaryObject.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
                binaryObject = null;
            }
            catch
            {

            }
        }
    }
}