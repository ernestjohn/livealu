using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Client;
using KudevolveWeb.Models;
using System.Net.Http;

namespace KudevolveWeb.APIS
{
    public class RealTimePostUpdater
    {
        private static HubConnection hubConnection;
        private static IHubProxy postsHubProxy;
        public RealTimePostUpdater ()
	        {
                hubConnection = new HubConnection("http://kudevolve.azurewebsites.net/");
                postsHubProxy = hubConnection.CreateHubProxy("NotificationStreamer");
                hubConnection.Start().Wait();
                
                    
	        }
        public static void UpdatePost(Post thePost)
        {
            var post = JsonConvert.SerializeObject(thePost);
            postsHubProxy.Invoke("Update", post).Wait();
        }

        public static void UpdatePostComment(string postid, Post thePost)
        {
            var post = JsonConvert.SerializeObject(thePost);
            postsHubProxy.Invoke("", post).Wait();
        }

        public static void NotifyUser(AppUser user)
        {

        }

        

    }
}