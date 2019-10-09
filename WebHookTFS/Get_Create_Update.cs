using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
//using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace WebHookTFS
{
    //class Program
    //{
    //    static int Main(string[] args)
    //    {
    //        Uri collectionUri = (args.Length < 1) ?
    //            new Uri("http://server:8080/TFS/") : new Uri(args[0]);
    //        TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(collectionUri);
    //        WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();
    //        Project teamProject = workItemStore.Projects["MyProject"];
    //        WorkItemType workItemType = teamProject.WorkItemTypes["Product Backlog Item"];

    //        WorkItem PBI = new WorkItem(workItemType);

    //        PBI.Title = "TITLE GOES HERE";
    //        PBI.Description = "DESCRIPTION GOES HERE";
    //        PBI.Fields["Issue ID"].Value = "999999";


    //        PBI.Save();
    //        return (PBI.Id);

    //    }
    //}
    public class Get_Create_Update
    {
        public class Create_Delete
        {
            public object Id;
            public object Status;
            public object From;
            public object Description;
            public object Assigned_to;
            public object Effort;


            public class Tfs_Input
            {
                public int Id { get; set; }
                public string Status { get; set; }
                public string From { get; set; }
                public string Description { get; set; }
                public string Assigned_to { get; set; }
                public int Effort { get; set; }


            }

            public class Create
            {
                public string from { get; set; }
               // public Operation op { get; set; }
                public string path { get; set; }
                public object value { get; set; }
            }
            public class Get_Item
            {
                public string organization { get; set; }
                public string project { get; set; }
                public Int32 id { get; set; }
                public string fields { get; set; }
                public string asOf { get; set; }
                //public WorkItemExpand $expand{ get; set; }

                // public string api-version { get; set; }
            }
            public class Update
            {
                public string organization { get; set; }
                public string project { get; set; }
                public int id { get; set; }
                public bool validateOnly { get; set; }
                public Boolean bypassRules { get; set; }
                public Boolean suppressNotifications { get; set; }
                //public WorkItemExpand $expand{ get; set; }

                //public string api-version { get; set; }

            }


        }
    }
}