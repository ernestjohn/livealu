using Microsoft.AspNet.SignalR;

namespace KudevolveWeb.RealTime
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.addMessage();
        }
        public void SendMessage(string message)
        {
            Clients.All.addMessage(message);
        }

        //override the OnConnected Session 
        public override System.Threading.Tasks.Task OnConnected()
        {
            Clients.Caller.addMessage("You have currently connected to Kudevolve Online chat and you are available. Thank you");
            Clients.All.addMessage("New user Connected");
            return base.OnConnected();
        }
        //public override System.Threading.Tasks.Task OnDisconnected()
        //{
        //    Clients.All.addMessage("Someone has left the group chat");
        //    return base.OnDisconnected();
        //}
    }
}