
using System.Web.Util;
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
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Text;
using System.Collections.Specialized;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Microsoft.TeamFoundation.Client;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Newtonsoft.Json;
using System.Web.Util;

namespace WebHookTFS.Controllers
{

    public class WebhooksController : Controller
    {
        //public ActionResult Verify()
        //{
        //    string url = 
        //}
        public async Task<ActionResult> Index()
        {
            await WebHook.GetProjects();
            return View();
        }
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
        //https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems/{id}

        List<System.Web.Util.WorkItem> workItems = new List<System.Web.Util.WorkItem>();
        [System.Web.Http.Route("_apis/wit/workitems")]
        [System.Web.Http.HttpGet]
        public System.Web.Util.WorkItem Get (int id)
        {
            return workItems.Where(workItem => workItem.Id == id).FirstOrDefault();
        }
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "Value1", "Value2" };
        //}
        public void Post(System.Web.Util.WorkItem val)
        {
            System.Web.Util.WorkItem.Add(val);
        }
        public void Put([FromBody] string value)
        {

        }
        public class HttpProductInfoHeaderValueCollection
        {

        }
        public class ExecuteQuery
        {
            readonly string _uri;
            readonly string _personalAccessToken;
            readonly string _project;

            /// <summary>
            /// Constructor. Manually set values to match yourorganization. 
            /// </summary>
            public ExecuteQuery()
            {
                _uri = "https://ndi-tfs.visualstudio.com";
                _personalAccessToken = "yggseimvsboxrc3rvodr4dzshnc6cch6shcyavjrqxxbydb6geda";
                _project = "Web Hook";
            }

            /// <summary>
            /// Execute a WIQL query to return a list of bugs using the .NET client library
            /// </summary>
            /// <returns>List of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem</returns>
            public async Task<List<System.Web.Util.WorkItem>> RunGetBugsQueryUsingClientLib()
            {
                Uri uri = new Uri(_uri);
                string personalAccessToken = _personalAccessToken;
                string project = _project;

                VssBasicCredential credentials = new VssBasicCredential("", _personalAccessToken);

                //create a wiql object and build our query
                Wiql wiql = new Wiql()
                {
                    Query = "Select [State], [Title] " +
                            "From WorkItems " +
                            "Where [Work Item Type] = 'Bug' " +
                            "And [System.TeamProject] = '" + project + "' " +
                            "And [System.State] <> 'Closed' " +
                            "Order By [State] Asc, [Changed Date] Desc"
                };

                //create instance of work item tracking http client
                using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
                {
                    //execute the query to get the list of work items in the results
                    WorkItemQueryResult workItemQueryResult = await workItemTrackingHttpClient.QueryByWiqlAsync(wiql);

                    //some error handling                
                    if (workItemQueryResult.WorkItems.Count() != 0)
                    {
                        //need to get the list of our work item ids and put them into an array
                        //List<int> list = new List<int>();
                        List<int> list = new List<int>();
                        foreach (var item in workItemQueryResult.WorkItems)
                        {
                            list.Add(item.Id);
                        }
                        int[] arr = list.ToArray();

                        //build a list of the fields we want to see
                        string[] fields = new string[3];
                        fields[0] = "System.Id";
                        fields[1] = "System.Title";
                        fields[2] = "System.State";

                        //get work items for the ids found in query
                        var workItems = await workItemTrackingHttpClient.GetWorkItemsAsync(arr, fields, workItemQueryResult.AsOf);

                        Console.WriteLine("Query Results: {0} items found", workItems.Count);

                        //loop though work items and write to console
                        foreach (var workItem in workItems)
                        {
                            Console.WriteLine("{0} {1}  {2}", workItem.Id, workItem.Fields["System.Title"], workItem.Fields["System.State"]);
                        }

                       return workItems;
                    }

                    return null;
                }
            }
        }

    }

}


//public static async Task AsyncMain()
//{
//    //A.Johnston@nditech.com
//    string accountName = "ndi-tfs";
//    string urlAccount = string.Format("https://{0}.visualstudio.com", accountName);
//    string serviceAccountUsername = "Anthony";
//    string serviceAccountPassword = "Aj0hnston109";

//    var client = HttpClientFactory.Create(new HttpClientHandler() { AllowAutoRedirect = false });
//    var resp = await client.GetAsync(urlAccount, HttpCompletionOption.ResponseHeadersRead);
//    Console.WriteLine("Status Code = " + resp.StatusCode);
//    Debug.Assert(resp.StatusCode == HttpStatusCode.Redirect);

//    if (!resp.Headers.Contains("x-tfs-fedauthrealm") ||
//        !resp.Headers.Contains("x-tfs-fedauthissuer"))
//    {
//        Console.WriteLine("Cannot determine federation data");
//        return;
//    }
//    string realm = resp.Headers.GetValues("x-tfs-fedauthrealm").First();
//    string issuer = resp.Headers.GetValues("x-tfs-fedauthissuer").First();

//    Console.WriteLine("Federation Data:Realm={0}; Issuer={1}", realm, issuer);

//    //2 - Get an access token from ACS using WRAP protocol
//    string acsHost = new Uri(issuer).Host;
//    string acsRequestTokenUrl = string.Format("https://{0}/WRAPv0.9", acsHost);
//    resp = await client.PostAsync(acsRequestTokenUrl, new FormUrlEncodedContent(
//        new KeyValuePair<string, string>[] {
//            new KeyValuePair<string, string>("wrap_scope", realm),
//            new KeyValuePair<string, string>("wrap_name", serviceAccountUsername),
//            new KeyValuePair<string, string>("wrap_password", serviceAccountPassword)
//        }
//    ));

//    resp.EnsureSuccessStatusCode();
//    var acsTokenData = await resp.Content.ReadAsFormDataAsync();
//    var accessToken = acsTokenData["wrap_access_token"];
//    var accessTokenExpiredInSecs = int.Parse(acsTokenData["wrap_access_token_expires_in"]);
//    Console.WriteLine("Your access token expires in {0}: {1}",
//        accessTokenExpiredInSecs,
//        accessToken);

//    //3 - Let's use the access token to consumer the REST api
//    var request = new HttpRequestMessage(
//        HttpMethod.Get,
//        urlAccount + "/DefaultCollection/_apis/projects?api-version=1.0-preview.1");

//    request.Headers.Authorization = new AuthenticationHeaderValue(
//        "WRAP",
//        string.Format("access_token=\"{0}\"", accessToken));


//    resp = await client.SendAsync(request);
//    resp.EnsureSuccessStatusCode();

//    JObject jsonResult = await resp.Content.ReadAsAsync<JObject>();
//    var projects = jsonResult.SelectTokens("$.value[*].name");
//    Console.WriteLine("Here is the list of your pojects");
//    foreach (var p in projects)
//    {
//        Console.WriteLine(p);
//    }


//}





//public class client
//{
//    readonly WebClient client = new WebClient();
//    client.BaseAddress = string.Format("https://ndi-tfs.visualstudio.com.accesscontrol.windows.net");
//    NameValueCollection values = new NameValueCollection();
//    values.Add("wrap_name", "mysncustomer1");
//    values.Add("wrap_password", "5znwNTZDYC39dqhFOTDtnaikd1hiuRa4XaAj3Y9kJhQ=");
//    values.Add("wrap_scope", "https://ndi-tfs.visualstudio.com/Web%20Hook");
//    // WebClient takes care of the URL Encoding
//    byte[] responseBytes = client.UploadValues("WRAPv0.9", "POST", values);

//    // the raw response from ACS
//    string response = Encoding.UTF8.GetString(responseBytes);
//}



