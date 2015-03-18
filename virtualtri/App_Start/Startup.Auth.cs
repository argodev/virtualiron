using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Security.Claims;
using System.Threading.Tasks;

namespace virtualtri
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "kXp5tA0RmXXsaysGzuSJNw",
            //   consumerSecret: "BaaDd3TjD0B4nkmnhd4336yWFVc8yrW7DYiQZoJuLAY");

            // https://developers.facebook.com/x/apps/509610525822782/settings/
            var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = "509610525822782",
                AppSecret = "80d5a5f46acd068742634207e05039d4"
            };
            facebookAuthenticationOptions.Scope.Add("email");
            //app.UseFacebookAuthentication(facebookAuthenticationOptions);
            //app.UseFacebookAuthentication(
            //   appId: "509610525822782",
            //   appSecret: "80d5a5f46acd068742634207e05039d4");

            // 1038743317871-c2om754kk22134eivsd11tkbgjcrhrho.apps.googleusercontent.com
            // 
            var googleOptions = new GoogleOAuth2AuthenticationOptions() 
            {
                ClientId = Properties.Settings.Default.Google_ClientId,
                ClientSecret = Properties.Settings.Default.Google_ClientSecret,
                Provider = new GoogleOAuth2AuthenticationProvider() 
                {
                    OnAuthenticated = (context) =>
                        {
                            context.Identity.AddClaim(new Claim("urn:google:name", context.Identity.FindFirstValue(ClaimTypes.Name)));
                            context.Identity.AddClaim(new Claim("urn:google:email", context.Identity.FindFirstValue(ClaimTypes.Email)));
                            // This following line is need to retrieve the profile image
                            // context.Identity.AddClaim(new System.Security.Claims.Claim("urn:google:accesstoken", context.AccessToken, ClaimValueTypes.String, "Google"));

                            return Task.FromResult(0);
                        }
                }
            };

            app.UseGoogleAuthentication(googleOptions);
        }
    }
}