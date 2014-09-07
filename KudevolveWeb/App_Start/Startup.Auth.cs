using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
[assembly: OwinStartup(typeof(KudevolveWeb.Startup))]

namespace KudevolveWeb
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.MapSignalR();
            
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Accounts/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            //// Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "000000004C114187",
            //    clientSecret: "smm8UI3R-a-REoZfkLbNNlc9j4VLQg3U");

            //app.UseTwitterAuthentication(
            //   consumerKey: "CWfPQ6dvtFMCgrnslLvaxsexH",
            //   consumerSecret: "GAV5MtpCpzWjuzE54sJR3xR5WVouuWJ1eAwrYeW7dngBNhbU6T");

            //app.UseFacebookAuthentication(
            //   appId: "651257824946190",
            //   appSecret: "e7fb235b83346f03e6d16f8eb5359a7c");

            //app.UseGoogleAuthentication();
        }
    }
}