using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using System;
using System.Reflection;

namespace FuelWerx
{
	[DependsOn(new Type[] { typeof(AbpZeroEntityFrameworkModule), typeof(FuelWerxCoreModule) })]
	public class FuelWerxDataModule : AbpModule
	{
		public FuelWerxDataModule()
		{
		}

		public override void Initialize()
		{
			base.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
		}

		public override void PreInitialize()
		{
			base.Configuration.DefaultNameOrConnectionString = "Default";
		}
	}
}