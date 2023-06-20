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
                        result = _NewsApiService.InsertData(data.News);
                    }
                    else
                    {
                        result.Success = true;
                        result.Message = "Api存取成功，尚無回傳資料";
                    } 
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = "存取api失敗";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}