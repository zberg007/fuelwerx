using Abp.Localization;
using Abp.Zero.EntityFramework;
using FuelWerx.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations.Seed
{
	public class DefaultLanguagesCreator
	{
		private readonly FuelWerxDbContext _context;

		public static List<ApplicationLanguage> InitialLanguages
		{
			get;
			private set;
		}

		static DefaultLanguagesCreator()
		{
			List<ApplicationLanguage> applicationLanguages = new List<ApplicationLanguage>();
			int? nullable = null;
			applicationLanguages.Add(new ApplicationLanguage(nullable, "en", "English", "famfamfam-flag-us"));
			nullable = null;
			applicationLanguages.Add(new ApplicationLanguage(nullable, "ar", "العربية", "famfamfam-flag-sa"));
			nullable = null;
			applicationLanguages.Add(new ApplicationLanguage(nullable, "de", "German", "famfamfam-flag-de"));
			nullable = null;
			applicationLanguages.Add(new ApplicationLanguage(nullable, "pt-BR", "Portuguese", "famfamfam-flag-br"));
			nullable = null;
			applicationLanguages.Add(new ApplicationLanguage(nullable, "tr", "Türkçe", "famfamfam-flag-tr"));
			nullable = null;
			applicationLanguages.Add(new ApplicationLanguage(nullable, "zh-CN", "简体中文", "famfamfam-flag-cn"));
			DefaultLanguagesCreator.InitialLanguages = applicationLanguages;
		}

		public DefaultLanguagesCreator(FuelWerxDbContext context)
		{
			this._context = context;
		}

		private void AddLanguageIfNotExists(ApplicationLanguage language)
		{
			if (this._context.Languages.Any<ApplicationLanguage>((ApplicationLanguage l) => l.TenantId == language.TenantId && l.Name == language.Name))
			{
				return;
			}
			this._context.Languages.Add(language);
			this._context.SaveChanges();
		}

		public void Create()
		{
			this.CreateLanguages();
		}

		private void CreateLanguages()
		{
			foreach (ApplicationLanguage initialLanguage in DefaultLanguagesCreator.InitialLanguages)
			{
				this.AddLanguageIfNotExists(initialLanguage);
			}
		}
	}
}