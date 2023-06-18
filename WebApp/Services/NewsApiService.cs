using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models.ApiModels;
using WebApp.Models.Responses;
using WebApp.Repositories;

namespace WebApp.Services
{
    public class NewsApiService
    {
        private NewsRepository _NewsRepository;
        private string NewsApiUrl; 


        public NewsApiService() 
        {
            _NewsRepository = new NewsRepository();
            NewsApiUrl = "https://tcgbusfs.blob.core.windows.net/dotapp/news.json";
        }

        public bool InsertData(List<NewsDataModel> dataList)
        {
            return _NewsRepository.InsertNewsData(dataList);
        }

        public async Task<NewsApiResponse> GetApiDataList()
        {
            var result = new NewsApiResponse();

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
                    }
                    result.Success = true;
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Message = "存取api失敗";
                }
            }

            return result;
        }

    }
}