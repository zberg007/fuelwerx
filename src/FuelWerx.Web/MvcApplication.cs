using Abp;
using Abp.Dependency;
using Abp.Reflection;
using Abp.Web;
using Castle.Facilities.Logging;
using Castle.Windsor;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web
{
	public class MvcApplication : AbpWebApplication
	{
		public MvcApplication()
		{
		}

		protected override void Application_Start(object sender, EventArgs e)
		{
			base.AbpBootstrapper.IocManager.RegisterIfNot<IAssemblyFinder, CurrentDomainAssemblyFinder>(DependencyLifeStyle.Singleton);
			base.AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>((LoggingFacility f) => f.UseLog4Net().WithConfig("log4net.config"));
			base.Application_Start(sender, e);
		}
	}
}