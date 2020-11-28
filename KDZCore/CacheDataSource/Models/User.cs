using System;
using System.Collections.Generic;
using System.Text;

namespace KDZCore.CacheDataSource
{
    public class User
    {
        private string _name = null;
        private string _description = null;
        private string _id = null;
        private string _type = null;
        private string _email = null;

        public string Name
        {
            get => _name ?? "";
            set => _name = value;
        }

        public string Description
        {
            get => _description ?? "";
            set => _description = value;
        }

        public string Id
        {
            get => _id ?? "";
            set => _id = value;
        }

        public string Type
        {
            get => _type ?? "";
            set => _type = value;
        }

        public string Email
        {
            get => _email ?? "";
            set => _email = value;
        }
    }
}
