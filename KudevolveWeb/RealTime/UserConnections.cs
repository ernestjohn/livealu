using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KudevolveWeb.RealTime
{
    public class UserConnections
    {
        //Make a dictionary for user connection strings
        private static Dictionary<string, List<string>> Connections = new Dictionary<string, List<string>>();
       
        public void AddConnection(string userid)
        {
            Connections.Add(userid, new List<string>());
        }
        public void AddUserConnection(string userid, string connectionid)
        {
            //Add the connection to the user
            Connections.Where(connection => connection.Key == userid).FirstOrDefault().Value.Add(connectionid);

        }

        public void RemoveUserConnections(string userid)
        {
            //Algorithm to remove user connections
        }
        public List<string> GetConnections(string userid)
        {
            return Connections.Where(conn => conn.Key == userid).FirstOrDefault().Value;
        }

      
      

        //Code to save everything by deserialization
        public void Save()
        {
            //Serialize the whole dictionary into a json object
            JsonConvert.SerializeObject(null);
        }


    }
}