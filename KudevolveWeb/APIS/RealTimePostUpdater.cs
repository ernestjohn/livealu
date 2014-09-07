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
        private HubConnection hubConnection;
        
        private IHubProxy postsHubProxy;
        public RealTimePostUpdater ()
	        {
                hubConnection = new HubConnection("http://kudevolve.azurewebsites.net/");
                hubConnection.StateChanged += hubConnection_StateChanged;
                hubConnection.Received += hubConnection_Received;

                postsHubProxy = hubConnection.CreateHubProxy("NotificationStreamer");
                
                hubConnection.Start().Wait();

               
                
	        }

        void hubConnection_Received(string obj)
        {
            throw new NotImplementedException();
        }

        void hubConnection_StateChanged(StateChange obj)
        {
            throw new NotImplementedException();
        }
        public  void UpdatePost(Post thePost)
        {
            var post = JsonConvert.SerializeObject(thePost);
            postsHubProxy.Invoke("Update", post).Wait();
        }

        public void UpdatePostComment(string postid, Post thePost)
        {
            var post = JsonConvert.SerializeObject(thePost);
            postsHubProxy.Invoke("", post).Wait();
        }

        public static void NotifyUser(AppUser user)
        {

        }

        

    }
}