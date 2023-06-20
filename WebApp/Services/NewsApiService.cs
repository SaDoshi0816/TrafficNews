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
            var result = new NewsApiResponse()
            {
                Success = true
            };

            if (dataList.Any(x => string.IsNullOrEmpty(x.starttime) || x.starttime.Length != 14))
            {
                result.Success = false;
                result.Message = "Api存取成功，但開始時間資料格式錯誤";
            }

            //儲存資料
            if (result.Success)
            {
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
            }

            //轉換格式
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
    }
}