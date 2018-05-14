using Abp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuelWerx.MultiTenancy.Demo
{
	public static class MyRandomHelper
	{
		public static List<T> GenerateRandomizedList<T>(IEnumerable<T> items)
		{
			List<T> ts = new List<T>(items);
			List<T> ts1 = new List<T>();
			while (ts.Any<T>())
			{
				int random = RandomHelper.GetRandom(0, ts.Count);
				ts1.Add(ts[random]);
				ts.RemoveAt(random);
			}
			return ts1;
		}
	}
}