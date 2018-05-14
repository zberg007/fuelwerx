using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Runtime.Caching;
using Abp.Runtime.Caching.Configuration;
using FuelWerx.Authorization;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FuelWerx
{
	[DependsOn(new Type[] { typeof(FuelWerxCoreModule), typeof(AbpAutoMapperModule) })]
	public class FuelWerxApplicationModule : AbpModule
	{
		public FuelWerxApplicationModule()
		{
		}

		public override void Initialize()
		{
			base.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
			CustomDtoMapper.CreateMappings();
		}

		public override void PreInitialize()
		{
			base.Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();
			base.Configuration.Caching.Configure("_WebAppExtendedCache", (ICache cache) => cache.DefaultSlidingExpireTime = TimeSpan.FromHours(24));
		}
	}
}