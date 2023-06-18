using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Models.ApiModels;

namespace WebApp.Repositories
{
    public class NewsRepository
    {
        private string _connectionString;
        private SqlConnection _conn;

        public NewsRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TrafficNewsDB"].ConnectionString;
            _conn = new SqlConnection(_connectionString);
        }

        public bool InsertNewsData(List<NewsDataModel> dataList)
        {
            string sql = @" INSERT INTO [TrafficNews].[dbo].[News_Taipei]
                                      (
                                        [chtmessage], [engmessage],
                                        [starttime], [endtime], [updatetime],
                                        [content], [url] 
                                      )
                                 VALUES 
                                      ( 
                                        @chtmessage, @engmessage, 
                                        @starttime, @endtime, @updatetime, 
                                        @content, @url
                                      ) ";

            int i = _conn.Execute(sql, dataList);

            return i > 0;
        }
    }
}