using CoreTweet;
using CoreTweet.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterStream
{
    public class KudevolveTweeter
    {
        public void Start()
        {

            var session = OAuth.Authorize("", "");
            
            var tokens = OAuth.GetTokens(session, "PINCODE");
           

            foreach (var m in tokens.Streaming.StartStream(StreamingType.User))
            {
                
                switch (m.Type)
                {
                      
                    case MessageType.Create:
                        var status = (m as StatusMessage).Status;
                        Console.WriteLine("{0}: {1}", status.User.ScreenName, status.Text);
                        break;

                    case MessageType.Disconnect:
                        Console.WriteLine("disconected");
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
