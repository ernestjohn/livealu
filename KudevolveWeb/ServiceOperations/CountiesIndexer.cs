using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;
using Elasticsearch.Net.Connection;

namespace KudevolveWeb.ServiceOperations
{
    public sealed class CountiesIndexer
    {

        private static CountiesIndexer Instance;
        private static string URL = "http://quintelelastic.cloudapp.net";
        private static Uri uri = new Uri(URL);
        private static ConnectionSettings settings = new ConnectionSettings(uri, defaultIndex: "kudevolve");
        private static ElasticClient client = new ElasticClient(settings, new HttpConnection(settings));

        public static CountiesIndexer GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CountiesIndexer();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }

        public void Index(CountyItem data)
        {
            client.Index(data);
        }
    }
}