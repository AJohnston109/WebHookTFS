using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebHookTFS.Controllers
{
    public class WebhooksController : Controller
    {
        public HttpProductInfoHeaderValueCollection UserAgent { get; }
        public object RequestConstants { get; private set; }
        // GET: Webhooks
        public string GetReleases(string url)
        {

            var client = new WebClient();
            var UserAgent = new HttpClient();
            //client.Headers.Add(RequestConstants.UserAgent, RequestConstants.UserAgentValue);
            var response = client.DownloadString(url);
            return response;
        }
        private static readonly HttpClient httpClient;





        public ActionResult Index()
        {
            WebHook.GetProjects();
            return View();

        }

    }

    public class HttpProductInfoHeaderValueCollection
    {
    }
}
