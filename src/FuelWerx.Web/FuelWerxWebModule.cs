using Abp.Application.Navigation;
using Abp.Collections;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.IO;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FuelWerx;
using FuelWerx.Web.Areas.Mpa.Startup;
using FuelWerx.Web.Bundling;
using FuelWerx.Web.Navigation;
using FuelWerx.Web.Routing;
using FuelWerx.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FuelWerx.Web
{
	[DependsOn(new Type[] { typeof(AbpWebMvcModule), typeof(FuelWerxDataModule), typeof(FuelWerxApplicationModule), typeof(FuelWerxWebApiModule) })]
	public class FuelWerxWebModule : AbpModule
	{
		public FuelWerxWebModule()
		{
		}

		public override void Initialize()
		{
			base.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
			base.IocManager.IocContainer.Register(new IRegistration[] { Component.For<IAuthenticationManager>().UsingFactoryMethod<IAuthenticationManager>(() => HttpContext.Current.GetOwinContext().Authentication, false).LifestyleTransient() });
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleTable.Bundles.IgnoreList.Clear();
			CommonBundleConfig.RegisterBundles(BundleTable.Bundles);
			FrontEndBundleConfig.RegisterBundles(BundleTable.Bundles);
			MpaBundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		public override void PostInitialize()
		{
			HttpServerUtility server = HttpContext.Current.Server;
			AppFolders appFolder = base.IocManager.Resolve<AppFolders>();
			appFolder.SampleProfileImagesFolder = server.MapPath("~/Common/Images/SampleProfilePics");
			appFolder.TempFileDownloadFolder = server.MapPath("~/Temp/Downloads");
			try
			{
				DirectoryHelper.CreateIfNotExists(appFolder.TempFileDownloadFolder);
			}
			catch
			{
			}
		}

		public override void PreInitialize()
		{
			base.Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();
			base.Configuration.Navigation.Providers.Add<FrontEndNavigationProvider>();
			base.Configuration.Navigation.Providers.Add<MpaNavigationProvider>();
		}
	}
}