using Abp;
using Abp.Dependency;
using FuelWerx.Authorization.Users;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FuelWerx.MultiTenancy.Demo
{
	public class RandomUserGenerator : ITransientDependency
	{
		public static string[] Names;

		public static string[] EmailProviders;

		static RandomUserGenerator()
		{
			RandomUserGenerator.Names = new string[] { "Agatha Christie", "Albert Einstein", "Aldous Huxley", "Amin Maalouf", "Andrew Andrewus", "Arda Turan", "Audrey Naulin", "Biff Tannen", "Bruce Wayne", "Butch Coolidge", "Carl Sagan", "Charles Quint", "Christophe Grange", "Christopher Nolan", "Christopher Lloyd", "Clara Clayton", "Clarice Starling", "Dan Brown", "Daniel Radcliffe", "Douglas Hall", "David Wells", "Emmett Brown", "Friedrich Hegel", "Forrest Gump", "Franz Kafka", "Gabriel Marquez", "Galileo Galilei", "Georghe Hagi", "Georghe Orwell", "Georghe Richards", "Gottfried Leibniz", "Hannibal Lecter", "Hercules Poirot", "Isaac Asimov", "Jane Fuller", "Jean Reno", "Jeniffer Parker", "Johan Elmander", "Jules Winnfield", "Kurt Vonnegut", "Laurence Fishburne", "Leo Tolstoy", "Lorraine Baines", "Marsellus Wallace", "Marty Mcfly", "Michael Corleone", "Oktay Anar", "Omer Hayyam", "Paulho Coelho", "Quentin Tarantino", "Rene Descartes", "Robert Lafore", "Stanislaw Lem", "Stefan Zweig", "Stephenie Mayer", "Stephen Hawking", "Thomas More", "Vincent Vega", "Vladimir Nabokov", "William Faulkner" };
			RandomUserGenerator.EmailProviders = new string[] { "yahoo.com", "gmail.com", "hotmail.com", "outlook.com", "live.com", "yandex.com" };
		}

		public RandomUserGenerator()
		{
		}

		private static User CreateUser(int? tenantId, string nameSurname)
		{
			User user = new User()
			{
				TenantId = tenantId,
				UserName = RandomUserGenerator.GenerateUsername(nameSurname),
				EmailAddress = RandomUserGenerator.GenerateEmail(nameSurname),
				Password = (new PasswordHasher()).HashPassword("123456"),
				Name = nameSurname.Split(new char[] { ' ' })[0],
				Surname = nameSurname.Split(new char[] { ' ' })[1],
				ShouldChangePasswordOnNextLogin = false,
				IsActive = RandomHelper.GetRandom(0, 100) < 80,
				IsEmailConfirmed = true
			};
			return user;
		}

		private static string GenerateEmail(string nameSurname)
		{
			return string.Concat(RandomUserGenerator.GenerateUsername(nameSurname), "@", RandomHelper.GetRandomOf<string>(RandomUserGenerator.EmailProviders));
		}

		private static string GenerateUsername(string nameSurname)
		{
			return nameSurname.Replace(" ", ".").ToLower(CultureInfo.InvariantCulture);
		}

		public List<User> GetRandomUsers(int userCount, int tenantId)
		{
			List<User> users = new List<User>();
			List<string> strs = MyRandomHelper.GenerateRandomizedList<string>(RandomUserGenerator.Names);
			for (int i = 0; i < userCount && i < strs.Count; i++)
			{
				users.Add(RandomUserGenerator.CreateUser(new int?(tenantId), strs[i]));
			}
			return users;
		}
	}
}