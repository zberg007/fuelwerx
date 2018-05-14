using Abp.Application.Services.Dto;
using Abp.Runtime.Caching;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Administrative;
using FuelWerx.WebApi;
using FuelWerx.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FuelWerx.WebApi.Controllers
{
    [AllowAnonymous]
    public class GenericController : FuelWerxApiControllerBase
    {
        private readonly ICacheManager _cacheManager;

        private readonly IGenericAppService _genericAppService;

        static GenericController()
        {
        }

        public GenericController(IGenericAppService genericAppService, ICacheManager cacheManager)
        {
            this._genericAppService = genericAppService;
            this._cacheManager = cacheManager;
        }

        [ActionName("GetCountriesAsSelectListItems")]
        [HttpGet]
        [HttpPost]
        public async Task<HttpResponseMessage> GetCountriesAsSelectListItems(int? countryId = -1, int? selectedCountryId = null, bool jTable = false)
        {
            HttpResponseMessage httpResponseMessage;
            int? nullable;
            bool flag;
            string empty = string.Empty;
            object orDefaultAsync = await this._cacheManager.GetCache("_WebAppExtendedCache").GetOrDefaultAsync("Countries");
            object obj = orDefaultAsync;
            if (obj != null)
            {
                empty = obj.ToString();
            }
            else
            {
                nullable = null;
                ListResultOutput<CountriesListDto> countries = this._genericAppService.GetCountries(nullable);
                if (countries.Items.Count > 0)
                {
                    empty = JsonConvert.SerializeObject(countries);
                    await this._cacheManager.GetCache("_WebAppExtendedCache").SetAsync("Countries", empty, new TimeSpan?(TimeSpan.FromMinutes(2400)));
                }
            }
            if (empty.Length <= 0)
            {
                httpResponseMessage = null;
            }
            else
            {
                ListResultOutput<CountriesListDto> listResultOutput = JsonConvert.DeserializeObject<ListResultOutput<CountriesListDto>>(empty);
                List<GenericSelectListItemModel> genericSelectListItemModels = new List<GenericSelectListItemModel>(listResultOutput.Items.Count);
                bool flag1 = false;
                foreach (CountriesListDto item in listResultOutput.Items)
                {
                    nullable = selectedCountryId;
                    int id = item.Id;
                    flag = (nullable.GetValueOrDefault() == id ? nullable.HasValue : false);
                    if (flag)
                    {
                        flag1 = true;
                    }
                    GenericSelectListItemModel genericSelectListItemModel = new GenericSelectListItemModel()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = flag1,
                        Group = "",
                        Disabled = false
                    };
                    genericSelectListItemModels.Add(genericSelectListItemModel);
                    flag1 = false;
                }
                empty = JsonConvert.SerializeObject(genericSelectListItemModels);
                if (!jTable)
                {
                    HttpResponseMessage stringContent = this.Request.CreateResponse<string>(HttpStatusCode.OK, "value");
                    stringContent.Content = new StringContent(empty, Encoding.UTF8);
                    stringContent.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseHeaders headers = stringContent.Headers;
                    CacheControlHeaderValue cacheControlHeaderValue = new CacheControlHeaderValue()
                    {
                        MaxAge = new TimeSpan?(TimeSpan.FromMinutes(240))
                    };
                    headers.CacheControl = cacheControlHeaderValue;
                    httpResponseMessage = stringContent;
                }
                else
                {
                    List<GenericSelectListItemModel> genericSelectListItemModels1 = JsonConvert.DeserializeObject<List<GenericSelectListItemModel>>(empty);
                    List<JTableOption> list = (
                        from x in genericSelectListItemModels1
                        select new JTableOption()
                        {
                            DisplayText = x.Text,
                            Value = x.Value
                        }).ToList<JTableOption>();
                    JTableOptionResponse jTableOptionResponse = new JTableOptionResponse()
                    {
                        Result = "OK",
                        Options = list
                    };
                    string str = JsonConvert.SerializeObject(jTableOptionResponse);
                    HttpResponseMessage mediaTypeHeaderValue = this.Request.CreateResponse<string>(HttpStatusCode.OK, "OK");
                    mediaTypeHeaderValue.Content = new StringContent(str, Encoding.UTF8);
                    mediaTypeHeaderValue.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseHeaders httpResponseHeader = mediaTypeHeaderValue.Headers;
                    CacheControlHeaderValue cacheControlHeaderValue1 = new CacheControlHeaderValue()
                    {
                        MaxAge = new TimeSpan?(TimeSpan.FromMinutes(240))
                    };
                    httpResponseHeader.CacheControl = cacheControlHeaderValue1;
                    httpResponseMessage = mediaTypeHeaderValue;
                }
            }
            return httpResponseMessage;
        }

        [ActionName("GetCountryRegionsAsSelectListItems"), HttpGet, HttpPost]
        public async Task<HttpResponseMessage> GetCountryRegionsAsSelectListItems(int? countryRegionId = null, int? countryId = null, int? selectedCountryRegionId = null, bool jTable = false)
        {
            string text = string.Empty;
            object cachedRegionsJson = await _cacheManager.GetCache("_WebAppExtendedCache").GetOrDefaultAsync("CountryRegions");
            if (cachedRegionsJson == null)
            {
                ListResultOutput<CountryRegionInCountryListDto> countryRegions = this._genericAppService.GetCountryRegions(null, null);
                if (countryRegions.Items.Count > 0)
                {
                    text = JsonConvert.SerializeObject(countryRegions);
                    await _cacheManager.GetCache("_WebAppExtendedCache").SetAsync("CountryRegions", text, new TimeSpan?(TimeSpan.FromMinutes(2400.0)));
                }
            }
            else
            {
                text = cachedRegionsJson.ToString();
            }
            HttpResponseMessage result;
            if (text.Length > 0)
            {
                var deserializedListResult = JsonConvert.DeserializeObject<ListResultOutput<CountryRegionInCountryListDto>>(text);
                var selectListItems = new List<GenericSelectListItemModel>(deserializedListResult.Items.Count);
                bool selectedFlag = false;
                if (countryRegionId.HasValue && countryRegionId > 0)
                {
                    foreach (var item in deserializedListResult.Items)
                    {
                        if (item.Id == countryRegionId)
                        {
                            if (selectedCountryRegionId == item.Id)
                            {
                                selectedFlag = true;
                            }
                            selectListItems.Add(new GenericSelectListItemModel
                            {
                                Text = item.Name,
                                Value = item.Id.ToString(),
                                Selected = selectedFlag,
                                Group = "",
                                Disabled = false
                            });
                            break;
                        }
                    }
                    text = JsonConvert.SerializeObject(selectListItems);
                }
                else if (countryId.HasValue && countryId > 0)
                {
                    foreach (var item in deserializedListResult.Items)
                    {
                        if (item.CountryId == countryId)
                        {
                            if (selectedCountryRegionId == item.Id)
                            {
                                selectedFlag = true;
                            }
                            selectListItems.Add(new GenericSelectListItemModel
                            {
                                Text = item.Name,
                                Value = item.Id.ToString(),
                                Selected = selectedFlag,
                                Group = "",
                                Disabled = false
                            });
                        }
                        selectedFlag = false;
                    }
                    text = JsonConvert.SerializeObject(selectListItems);
                }
                else
                {
                    foreach (var item in deserializedListResult.Items)
                    {
                        if (selectedCountryRegionId == item.Id)
                        {
                            selectedFlag = true;
                        }
                        selectListItems.Add(new GenericSelectListItemModel
                        {
                            Text = item.Name,
                            Value = item.Id.ToString(),
                            Selected = selectedFlag,
                            Group = "",
                            Disabled = false
                        });
                        selectedFlag = false;
                    }
                    text = JsonConvert.SerializeObject(selectListItems);
                }
                if (jTable)
                {
                    var deserializedList = JsonConvert.DeserializeObject<List<GenericSelectListItemModel>>(text);
                    List<JTableOption> listOfJTableOptions = deserializedList.Select(i => new JTableOption
                    {
                        DisplayText = i.Text,
                        Value = i.Value
                    }).ToList<JTableOption>();
                    string jTableOptionResponseJson = JsonConvert.SerializeObject(new JTableOptionResponse
                    {
                        Result = "OK",
                        Options = listOfJTableOptions
                    });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "OK");
                    response.Content = new StringContent(jTableOptionResponseJson, Encoding.UTF8);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response.Headers.CacheControl = new CacheControlHeaderValue
                    {
                        MaxAge = new TimeSpan?(TimeSpan.FromMinutes(240.0))
                    };
                    result = response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
                    response.Content = new StringContent(text, Encoding.UTF8);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response.Headers.CacheControl = new CacheControlHeaderValue
                    {
                        MaxAge = new TimeSpan?(TimeSpan.FromMinutes(240.0))
                    };
                    result = response;
                }
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}