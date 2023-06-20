using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.ApiModels;

namespace WebApp.Repositories.Tests
{
    [TestClass()]
    public class NewsRepositoryTests
    {
        [TestMethod()]
        public void NewsRepositoryTest()
        {
            var repsitory = new NewsRepository();

            // 資料庫寫入錯誤
            List<NewsDataModel> dataList = new List<NewsDataModel>()
            {
               new NewsDataModel { chtmessage = "測試", engmessage = "test", starttime = "20230620205530213" }
            };
            if (repsitory.InsertNewsData(dataList))
            {
                Assert.Fail();
            }
        }
    }
}