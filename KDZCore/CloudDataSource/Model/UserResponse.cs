using System;
using System.Collections.Generic;
using System.Text;

namespace KDZCore.CloudDataSource.Model
{
    public class UserResponse
    {
        public string id { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public Additional additional { get; set; }

    }

    public class Additional
    {
        public string email { get; set; }
    }
}
