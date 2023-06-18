using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.ApiModels;

namespace WebApp.Models.Responses
{
    public class NewsApiResponse : BaseResponse
    {

        public List<NewsDataModel> Data { get; set; }

        public NewsApiResponse()
        {
            Data = new List<NewsDataModel>();
        }
    }
}