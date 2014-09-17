using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Client;
using KudevolveWeb.Models;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KudevolveWeb.APIS
{
    public sealed class RealTimePostUpdater
    {
        private HubConnection hubConnection;
        private static RealTimePostUpdater Instance;
        private IHubProxy postsHubProxy;
        public RealTimePostUpdater ()
	        {
                hubConnection = new HubConnection("http://localhost:4775");
                hubConnection.StateChanged += hubConnection_StateChanged;
                hubConnection.Received += hubConnection_Received;
             
                postsHubProxy = hubConnection.CreateHubProxy("NotificationStreamer");
                hubConnection.Closed += hubConnection_Closed;
                hubConnection.Start().Wait();
                
               Debug.WriteLine("New signalr updater object created");
	        }
        public static RealTimePostUpdater GetInstance()
        {
            if (Instance == null)
            {
                Instance =  new RealTimePostUpdater();
                return Instance;
            }
            else
            {
                return Instance;
            }
        }
        void hubConnection_Closed()
        {
            //throw new NotImplementedException();
        }

        void hubConnection_Received(string obj)
        {
           // throw new NotImplementedException();
        }

        void hubConnection_StateChanged(StateChange obj)
        {
            var state = hubConnection.State;
           // throw new NotImplementedException();
        }
        public async Task UpdatePost(Post thePost)
        {
            var post = JsonConvert.SerializeObject(thePost);
           await postsHubProxy.Invoke("Update", post);
        }

        public async Task UpdatePostComment(string postid, Post thePost)
        {
            var post = JsonConvert.SerializeObject(thePost);
           await postsHubProxy.Invoke("UpdatePostComment", post);
        }

        public static void NotifyUser(AppUser user)
        {

        }

        

    }
}