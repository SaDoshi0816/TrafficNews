using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ApiModels
{
    public class NewsApiModel
    {
        public DateTime updateTime { get; set; }

        public List<NewsDataModel> News { get;set; }

    }

    public class NewsDataModel
    {
        public string chtmessage { get; set; }
        public string engmessage { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string updatetime { get; set; }
        public string content { get; set; }
        public string url { get; set; }
    }
}