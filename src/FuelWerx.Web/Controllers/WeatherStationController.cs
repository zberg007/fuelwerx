using Abp.Web.Models;
using Abp.Web.Mvc.Models;
using DegreeDays.Api;
using DegreeDays.Api.Data;
using DegreeDays.Geo;
using System;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class WeatherStationController : FuelWerxControllerBase
	{
		public WeatherStationController()
		{
		}

		[HttpPost]
		public ActionResult GetSuggestedWeatherStationByGeocoordinates(double latitude, double longitude)
		{
			ActionResult actionResult;
			DegreeDaysApi degreeDaysApi = new DegreeDaysApi(new AccountKey(this.apiKey_degreeDaysAccountKey), new SecurityKey(this.apiKey_degreeDaysSecurityKey));
			DatedDataSpec datedDataSpec = DataSpec.Dated(Calculation.HeatingDegreeDays(Temperature.Fahrenheit(65)), DatedBreakdown.Daily(Period.LatestValues(10)));
			LocationDataRequest locationDataRequest = new LocationDataRequest(Location.LongLat(new LongLat(longitude, latitude)), new DataSpecs(new DataSpec[] { datedDataSpec }));
			LocationDataResponse locationData = null;
			try
			{
				locationData = degreeDaysApi.DataApi.GetLocationData(locationDataRequest);
				if (locationData == null)
				{
					return base.Json(new MvcAjaxResponse(new ErrorInfo("We're sorry we could not find the suggested weather station for the coordinates provided."), false));
				}
				if (locationData.StationId.ToString().Length > 0)
				{
					return base.Json(new MvcAjaxResponse(locationData.StationId.ToString()));
				}
				return base.Json(new MvcAjaxResponse(new ErrorInfo("We're sorry we could not find the suggested weather station for the coordinates provided (response was blank)."), false));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				actionResult = base.Json(new MvcAjaxResponse(new ErrorInfo(exception.Message), false));
			}
			return actionResult;
		}

		[HttpPost]
		public ActionResult ValidateWeatherStationByStationId(string stationId)
		{
			ActionResult actionResult;
			DegreeDaysApi degreeDaysApi = new DegreeDaysApi(new AccountKey(this.apiKey_degreeDaysAccountKey), new SecurityKey(this.apiKey_degreeDaysSecurityKey));
			DatedDataSpec datedDataSpec = DataSpec.Dated(Calculation.HeatingDegreeDays(Temperature.Fahrenheit(65)), DatedBreakdown.Daily(Period.LatestValues(10)));
			LocationDataRequest locationDataRequest = new LocationDataRequest(Location.StationId(stationId), new DataSpecs(new DataSpec[] { datedDataSpec }));
			LocationDataResponse locationData = null;
			try
			{
				locationData = degreeDaysApi.DataApi.GetLocationData(locationDataRequest);
				if (locationData == null)
				{
					return base.Json(new MvcAjaxResponse(new ErrorInfo(string.Concat("We're sorry we could not validate the weather station ", stationId, ".")), false));
				}
				if (locationData.StationId.ToString().Length > 0)
				{
					return base.Json(new MvcAjaxResponse(locationData.StationId.ToString()));
				}
				return base.Json(new MvcAjaxResponse(new ErrorInfo(string.Concat("We're sorry we could not validate the weather station ", stationId, " (response was blank).")), false));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				actionResult = base.Json(new MvcAjaxResponse(new ErrorInfo(exception.Message), false));
			}
			return actionResult;
		}
	}
}