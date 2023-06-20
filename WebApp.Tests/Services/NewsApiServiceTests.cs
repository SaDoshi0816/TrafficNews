using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.ApiModels;

namespace WebApp.Services.Tests
{
    [TestClass()]
    public class NewsApiServiceTests
    {
        [TestMethod()]
        public void InsertDataTest()
        {
            var service = new NewsApiService();

            // 開始時間錯誤檢查
            List<NewsDataModel> dataList = new List<NewsDataModel>()
            {
               new NewsDataModel { chtmessage = "測試", engmessage = "test", starttime = "123" }
            };
            var result = service.InsertData(dataList);
            if (result.Success)
            {
                Assert.Fail();
            }

            //資料庫寫入錯誤
            dataList = new List<NewsDataModel>()
            {
               new NewsDataModel { chtmessage = "測試", engmessage = "test", starttime = "20230620205530" , endtime = "2120230620205530" }
            };
            result = service.InsertData(dataList);
            if (result.Success)
            {
                Assert.Fail();
            }
        }
    }
}