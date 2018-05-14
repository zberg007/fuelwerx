using Abp.Configuration;
using Abp.Zero.EntityFramework;
using FuelWerx.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations.Seed
{
    public class DefaultSettingsCreator
    {
        private readonly FuelWerxDbContext _context;

        public DefaultSettingsCreator(FuelWerxDbContext context)
        {
            this._context = context;
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }
            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }

        public void Create()
        {
            int? nullable = null;
            this.AddSettingIfNotExists("Abp.Net.Mail.DefaultFromAddress", "admin@mydomain.com", nullable);
            nullable = null;
            this.AddSettingIfNotExists("Abp.Net.Mail.DefaultFromDisplayName", "mydomain.com mailer", nullable);
            nullable = null;
            this.AddSettingIfNotExists("Abp.Localization.DefaultLanguageName", "en", nullable);
        }
    }
}