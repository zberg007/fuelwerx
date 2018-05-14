using Abp.Application.Features;
using Abp.Collections;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap;
using FuelWerx.Authorization.Roles;
using FuelWerx.Configuration;
using FuelWerx.Features;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FuelWerx
{
	[DependsOn(new Type[] { typeof(AbpZeroCoreModule), typeof(AbpZeroLdapModule) })]
	public class FuelWerxCoreModule : AbpModule
	{
		public FuelWerxCoreModule()
		{
		}

		public override void Initialize()
		{
			base.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
		}

		public override void PreInitialize()
		{
			base.Configuration.Localization.Sources.Add(new DictionaryBasedLocalizationSource("FuelWerx", new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(), "FuelWerx.Localization.FuelWerx")));
			base.Configuration.Features.Providers.Add<AppFeatureProvider>();
			base.Configuration.Settings.Providers.Add<AppSettingProvider>();
			base.Configuration.MultiTenancy.IsEnabled = true;
			AppRoleConfig.Configure(base.Configuration.Modules.Zero().RoleManagement);
		}
	}
}