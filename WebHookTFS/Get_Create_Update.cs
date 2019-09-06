using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHookTFS
{
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

                // public string api-version { get; set; }

            }

        }
    }
}