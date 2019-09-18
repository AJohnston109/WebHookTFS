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
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;


namespace WebHookTFS
{
    public class CloneWorkItemResult
    {
        public System.Web.Util.WorkItem originalWorkItem { get; set; }
        public System.Web.Util.WorkItem cloneWorkItem { get; set; }
        public CloneWorkItemResult(System.Web.Util.WorkItem originalWorkItem, System.Web.Util.WorkItem cloneWorkItem)
        {
            this.originalWorkItem = originalWorkItem;
            this.cloneWorkItem = cloneWorkItem;
        }
    }
}