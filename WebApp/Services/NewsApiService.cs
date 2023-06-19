using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
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

        public NewsApiResponse InsertData(List<NewsDataModel> dataList)
        {
            var result = new NewsApiResponse();
            if (_NewsRepository.InsertNewsData(dataList))
            {
                result.Success = true;
                result.Message = "儲存成功";                
            }
            else
            {
                result.Success = false;
                result.Message = "Api存取成功，但資料庫儲存失敗";
            }
            result.Data = TransferViewModel(dataList);

            return result;
        }

        public List<NewsDataModel> TransferViewModel(List<NewsDataModel> dataList) 
        {
            dataList.ForEach(x =>
            {
                x.starttime = FormatDateTimeString(x.starttime);
                x.endtime = FormatDateTimeString(x.endtime);
                x.updatetime = FormatDateTimeString(x.updatetime);
            });

            return dataList;
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

        public async Task<NewsApiResponse> GetApiDataList(int timeout = 10)
        {
            var result = new NewsApiResponse();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(timeout);
                    HttpResponseMessage response = await client.GetAsync(new Uri(NewsApiUrl));
                    response.EnsureSuccessStatusCode();

                    //HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, NewsApiUrl);
                    //var response = await client.SendAsync(httpRequest);

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