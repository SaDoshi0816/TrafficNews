using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.ApiModels;
using WebApp.Models.Responses;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class NewsApiController : Controller
    {
        private NewsApiService _NewsApiService;

        public NewsApiController()
        {
            _NewsApiService = new NewsApiService();
        }

        public JsonResult GetApiDataList()
        {
            var result = _NewsApiService.GetApiDataList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetApiDataLists()
        {
            var result = new NewsApiResponse();
            string NewsApiUrl = "https://tcgbusfs.blob.core.windows.net/dotapp/news.json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    HttpResponseMessage response = await client.GetAsync(new Uri(NewsApiUrl));
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    responseBody = responseBody.Replace("\r\n", string.Empty);
                    var data = JsonConvert.DeserializeObject<NewsApiModel>(responseBody);

                    if (data.News.Count > 0)
                    {
                        result.Data = data.News;

                        data.News.ForEach(x => 
                            { 
                                x.starttime = FormatDateTimeString(x.starttime);
                                x.endtime = FormatDateTimeString(x.endtime);
                                x.updatetime = FormatDateTimeString(x.updatetime);
                            });


                        //_NewsApiService.InsertData(result.Data);
                    }
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = "存取api失敗";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public string FormatDateTimeString(string str)
        {
            string dateTime = "--";
            if (!string.IsNullOrEmpty(str) && str.Length == 14)
            {
                string date = str.Substring(0, 4) + "/" + str.Substring(4, 2) + "/" + str.Substring(6, 2);
                string time = str.Substring(8, 2) + ":" + str.Substring(10, 2) + ":" + str.Substring(12, 2);
                dateTime = date + "<br/>" + time;
            }
            return dateTime;
        }
    }
}