using Microsoft.AspNet.SignalR;
using KudevolveWeb.Models;
using KudevolveWeb.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Hubs;

namespace KudevolveWeb.RealTime
{
    [HubName("notificationStreamer")]
    public class NotificationStreamer : Hub
    {
        //Instantiate our Connection Manager
        UserConnections ConnectionManager = new UserConnections();

        //A static method to send messages called from the API Controller
       
        public void Hello()
        {
            Clients.All.hello();
        }

        //Method to push updates, refresh content on the Client side
        public void Update(string newPost)
        {
            //Send the new post
            Clients.All.addNewPost(newPost);
        }

        //Code to push a comment in the Angular Scope post comments
        public void AddComment(string postid, string comment)
        {
            //Call the Client method to do so
            Clients.All.addPostComment(postid,comment);
        }

        //Code to Notify user by passing a user id
        public void NotifyUser(string userid, string message)
        {
            //Send the message to the Client
            Clients.Client(userid).notify(message);
        }

        public void AddPetition(string petition)
        {
            Clients.All.addPetition(petition);
        }

        public void AddGroupMessage(string grpname, string message)
        {
            //Notify the Clients in the Group
            Clients.Group(grpname).addMessage(message);
        }

        public void AddGroupMessageComment(string grpname, string postid, string comment)
        {
            Clients.All.addGroupMessageComment(grpname, postid, comment);
        }

        //Override the connection methods
        public override System.Threading.Tasks.Task OnConnected()
        {
            //var userid = Context.Headers.Get("userid");
          string conn = Context.ConnectionId;
            ////Save the connected user's connection
            //ConnectionManager.AddConnection(userid);
            //ConnectionManager.AddUserConnection(userid, Context.ConnectionId);
            Clients.Caller.notify("You have connected");

            return base.OnConnected();
        }
      

        //public override System.Threading.Tasks.Task OnDisconnected()
        //{
        //    //Remoe the connected user's connection
        //    return base.OnDisconnected();
        //}
    }
}