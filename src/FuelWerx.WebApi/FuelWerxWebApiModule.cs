using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Configuration;
using Abp.WebApi.Controllers.Dynamic.Builders;
using FuelWerx;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Filters;

namespace FuelWerx.WebApi
{
	[DependsOn(new Type[] { typeof(AbpWebApiModule), typeof(FuelWerxApplicationModule) })]
	public class FuelWerxWebApiModule : AbpModule
	{
		public FuelWerxWebApiModule()
		{
		}

		public override void Initialize()
		{
			base.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
			DynamicApiControllerBuilder.ForAll<IApplicationService>(typeof(FuelWerxApplicationModule).Assembly, "app").Build();
			base.Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
		}
	}
}