using AutoMapper;
using FuelWerx.Estimates;
using FuelWerx.Projects;
using System;

namespace FuelWerx.Estimates.Dto
{
	internal static class EstimateProjectMapper
	{
		private static volatile bool _mappedBefore;

		private readonly static object SyncObj;

		static EstimateProjectMapper()
		{
			EstimateProjectMapper.SyncObj = new object();
		}

		public static void CreateMappings()
		{
			lock (EstimateProjectMapper.SyncObj)
			{
				if (!EstimateProjectMapper._mappedBefore)
				{
					EstimateProjectMapper.CreateMappingsInternal();
					EstimateProjectMapper._mappedBefore = true;
				}
			}
		}

		private static void CreateMappingsInternal()
		{
			Mapper.CreateMap<Estimate, Project>().ReverseMap();
		}
	}
}