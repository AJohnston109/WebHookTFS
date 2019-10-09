using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
//using Microsoft.VisualStudio.Services.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace WebHookTFS
{
    public static class WebHook
    {
        
        public class WorkItemPostData
        {
            public string op;
            public string path;
            public string value;
        }
        public async static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };
            return await client.SendAsync(request);
        }

        //private static object client;
        public class WorkItemDetails
        {
            public string id;
            public string rev;
            public IDictionary<string, string> fields;
            public string url;
        }

        [HttpGet]
        public static async Task GetProjects()
        {
            try
            {
                var personalAccessToken = "yggseimvsboxrc3rvodr4dzshnc6cch6shcyavjrqxxbydb6geda";
                //Console.ReadKey();
                //var personalAccessToken = String.Empty;
                using (HttpClient client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://ndi-tfs.visualstudio.com");  
                    //client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", "", personalAccessToken))));
                    //var param = "abc123";
                    //process the response stream
                    using (HttpResponseMessage response = await client.GetAsync(
                        "https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems/$Product%20Backlog%20Item?api-version=5.1"
                        ))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex);
            }
        }

        [HttpGet]
        public static async void GetWorkItem(string username, string password, int id)
        {
            try
            {
                var personalAccessToken = "yggseimvsboxrc3rvodr4dzshnc6cch6shcyavjrqxxbydb6geda";
                Console.ReadKey();
                //var personalAccessToken = String.Empty;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //provide the personal access token through an HTTP header, convert it to a Base64 string
                    //The resulting string can then be provided as an HTTP header in the format:
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                           Encoding.ASCII.GetBytes(
                           string.Format("{0}:{1}:{2}", username, personalAccessToken, id))));

                    //var param = "abc123";
                    //var url = $"https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems?ids={id}&api-version=5.1";
                    //process the response stream
                    string url = $"https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems/$Product%20Backlog%20Item?api-version=5.1";

                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        WorkItemDetails wiDetails = JsonConvert.DeserializeObject<WorkItemDetails>(responseBody);
                        Console.WriteLine("Work Item ID: \t" + wiDetails.id);
                        foreach (KeyValuePair<string, string> fld in wiDetails.fields)
                        {
                            Console.WriteLine(fld.Key + ":\t" + fld.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex);
            }
        }
        [HttpGet]
        public static async void GetBacklog(string username, int id)
        {
            try
            {
                var personalAccessToken = "yggseimvsboxrc3rvodr4dzshnc6cch6shcyavjrqxxbydb6geda";
                Console.ReadKey();
                //var personalAccessToken = String.Empty;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}:{2}", username, personalAccessToken, id))));

                    //process the response stream
                    using (HttpResponseMessage response = await client.GetAsync(
                        $"https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/backlogs/backlog/VISCE%20Team/Features&api-version=5.1"))
                    //  $"https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems?ids={id}&api-version=5.1"
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex);
            }
        }


        //public static void CreateWorkItem(int WiId)
        //{
        //    var username = "A.Jojhnston@nditech.com";
        //    var password = "Aj0hnston109";
        //    DoCreateWorkItem(username, password, WiId);
        //}

        [HttpPatch]
        private static async void CreateWorkItem(string username, string password)
        {
            try
            {
                var type = "Product%20Backlog%20Item";
                var personalAccessToken = "yggseimvsboxrc3rvodr4dzshnc6cch6shcyavjrqxxbydb6geda";
                using (HttpClient client = new HttpClient())
                {
                    //if (!ModelState.IsValid)
                    //    throw new HttpResponseException(HttpStatusCode.BadRequest);
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json-patch+json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                    System.Text.Encoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", username, password))));

                    WorkItemPostData wiPostData = new WorkItemPostData();
                    wiPostData.op = "add";
                    wiPostData.path = "/fields/System.Title";
                    wiPostData.value = "Employee edits other employees profile";
                    List<WorkItemPostData> wiPostDataArr = new List<WorkItemPostData> {
                    wiPostData };
                    string wiPostDataString = JsonConvert.SerializeObject(wiPostDataArr);
                    HttpContent wiPostDataContent = new StringContent(wiPostDataString, Encoding.UTF8, "application/json-patch+json");
                    string url = $"https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems/${type}";
                    //"https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems/create/Product%20Backlog%20Item";

                    using (HttpResponseMessage response = client.PatchAsync(url, wiPostDataContent).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string ResponseContent = await
                        response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }

        //Func<int, string, string> WorkItem(int WiId):this()
        //{
        //    var username = "SA";
        //    var password = "Jumbotron";
        //    DoCreateWorkItem(username, password, WiId);
        //}


        [HttpPatch]
        public static async Task UpdateWorkItem(string username, int id)
        {
            try
            {
                var personalAccessToken = "yggseimvsboxrc3rvodr4dzshnc6cch6shcyavjrqxxbydb6geda";
                Console.ReadKey();
                //var personalAccessToken = String.Empty;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}:{2}", username, personalAccessToken, id))));

                    WorkItemPostData wiPostData = new WorkItemPostData();
                    wiPostData.op = "replace";
                    wiPostData.path = "/fields/System.Title";
                    wiPostData.value = "Employee edits own profile in broser based app";
                    List<WorkItemPostData> wiPostDataArr = new List<WorkItemPostData> { wiPostData };
                    string wiPostDataString = JsonConvert.SerializeObject(wiPostDataArr);
                    HttpContent wiPostDataContent = new StringContent(wiPostDataString,
                    Encoding.UTF8, "application/json-patch+json");
                    //var param = "abc123";
                    var url = $"https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems/{id}";
                                
                    //https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems?ids={id}&api-version=5.1
                    //process the response stream
                    using (HttpResponseMessage response = await client.PatchAsync(url, wiPostDataContent))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                        //const Json = await Json();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex);
            }
        }
        //https://ndi-tfs.visualstudio.com/VISCE/_apis/build/builds/?/logs?api-version=2.0
        //https://ndi-tfs.visualstudio.com/VISCE/_apis/wit/workItemTypes?api-version=5.1
        //https://ndi-tfs.visualstudio.com/VISCE/_apis/wit/queries?api-version=5.1
        //https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitemtypes/Support%20Escalation/states
        //https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitemtypes
    }
}
//enter to debug in postman
//http://localhost:55462/Webhooks
//to register your app
//https://vsooauthtest.azurewebsites.net/
//WebHook Token Password: yggseimvsboxrc3rvodr4dzshnc6cch6shcyavjrqxxbydb6geda
//System.LinkTypes.Dependency\
//System.LinkTypes.Related\