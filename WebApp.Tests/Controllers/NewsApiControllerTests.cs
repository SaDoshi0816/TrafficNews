using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.ApiModels;
using Newtonsoft.Json;
using System.Web.Mvc;
using WebApp.Models.Responses;

namespace WebApp.Controllers.Tests
{
    [TestClass()]
    public class NewsApiControllerTests
    {
        [TestMethod()]
        public void GetApiDataListsTest()
        {
            var api = new NewsApiController();

            var JsonResult =  api.GetApiDataLists();
            var data = (NewsApiResponse)JsonResult.Result.Data;
            if (!data.Success)
            {
                Assert.Fail();
            }            
        }
    }
}