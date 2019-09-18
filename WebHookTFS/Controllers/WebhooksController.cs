using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Util;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WebHookTFS.Controllers
{
    public class WebhooksController : Controller
    {
        public HttpProductInfoHeaderValueCollection UserAgent { get; }
        //private ApplicationDbContext _context;
        public object RequestConstants { get; private set; }
        // GET: Webhooks
        public string GetReleases(string url)
        {
            //_context = new ApplicationDbContext();
            var client = new WebClient();
            var UserAgent = new HttpClient();
            //client.Headers.Add(RequestConstants.UserAgent, RequestConstants.UserAgentValue);
            var response = client.DownloadString(url);
            return response;
        }
        //public IEnumerable<WorkItem> GetWorkItems()
        //{
        //    var workItem = url.WorkItems.ToList(); 
        //}
        private static readonly HttpClient httpClient;
       

        public ActionResult Index()
        {
            WebHook.GetProjects();
            return View();
        }

    }
    //public static object DumpJson(this object value, string desctiption = null)
    //{
    //    return GetJsonDumpTarget(value).Dump(desctiption);
    //}
    //public static object DumpJson(this object value, string desctiption, int depth)
    //{
    //    return GetJsonDumpTarget(value).Dump(desctiption, depth);
    //}
    public class HttpProductInfoHeaderValueCollection
    {
    }
}
