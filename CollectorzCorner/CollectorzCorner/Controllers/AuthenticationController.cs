using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using System.Text;

namespace CollectorzCorner.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Facebook/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("FacebookLogin")]
        public ActionResult FacebookLogin()
        {
            
            Uri myUri = new Uri("http://" + Request.Url.Host + ":" + Request.Url.Port);
            //Uri myUri = new Uri(baseUri, "/Authentication/Handshake/"); // GET JAMES TO FIX THIS

            //redirect to https://graph.facebook.com/oauth/authorize giving Facebook my application id, the request type and the redirect url
            return new RedirectResult("https://graph.facebook.com/oauth/authorize? type=web_server& client_id=114756055226487& redirect_uri=http://localhost:57042/Authentication/Handshake/");
        }

        //this is the statically typed representation of the JSON object that will get returned from: https://graph.facebook.com/me
        public class FacebookUser
        {
            public long id { get; set; } //yes. the user id is of type long...dont use int
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string name { get; set; }
            public string email { get; set; }

        }

        //this controller action will be called when Facebook redirects
        [HttpGet]
        [ActionName("handshake")]
        public ActionResult Handshake(string code)
        {
            //after authentication, Facebook will redirect to this controller action with a QueryString parameter called "code" (this is Facebook's Session key)

            //example uri: http://www.examplewebsite.com/facebook/handshake/?code=2.DQUGad7_kFVGqKTeGUqQTQ__.3600.1273809600-1756053625|dil1rmAUjgbViM_GQutw-PEgPIg.

            //this is your Facebook App ID
            string clientId = "279947185459930";

            //this is your Secret Key
            string clientSecret = "1f35d86b4161e52398f255d61117ea7e";

            //we have to request an access token from the following Uri
            string url = "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}";

            //your redirect uri must be EXACTLY the same Uri that caused the initial authentication handshake
            string redirectUri = "http://localhost:57042/Authentication/Handshake/";

            //Create a webrequest to perform the request against the Uri
            WebRequest request = WebRequest.Create(string.Format(url, clientId, redirectUri, clientSecret, code));

            //read out the response as a utf-8 encoding and parse out the access_token
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader streamReader = new StreamReader(stream, encode);
            string accessToken = streamReader.ReadToEnd().Replace("access_token=", "");
            streamReader.Close();
            response.Close();

            //set the access token to some session variable so it can be used through out the session
            Session["FacebookAccessToken"] = accessToken;

            //now that we have an access token, query the Graph Api for the JSON representation of the User
            url = "https://graph.facebook.com/me?access_token={0}";

            //create the request to https://graph.facebook.com/me
            request = WebRequest.Create(string.Format(url, accessToken));

            //Get the response
            response = request.GetResponse();

            //Get the response stream
            stream = response.GetResponseStream();

            //Take our statically typed representation of the JSON User and deserialize the response stream
            //using the DataContractJsonSerializer
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(FacebookUser));
            FacebookUser user = new FacebookUser();
            user = dataContractJsonSerializer.ReadObject(stream) as FacebookUser;

            //close the stream
            response.Close();

            //capture the UserId
            Session["FacebookUserId"] = user.id;

            //Set the forms authentication auth cookie
            FormsAuthentication.SetAuthCookie(user.email, false);

            //redirect to home page so that user can start using your application      
            return RedirectToAction("Index","Home");
        }
    }
}
