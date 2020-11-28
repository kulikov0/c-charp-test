using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KDZCore.CacheDataSource
{
    interface UserCacheDataSource
    {
        void SaveUser(User user);
        public User GetUserCache();
    }

    class UserCacheDataSourceImpl : UserCacheDataSource
    {
        private const string UsersFileName = "user.json";

        private User _user;
        public UserCacheDataSourceImpl()
        {
            _user = GetUser();
        }

        public void SaveUser(User user)
        {
            _user = user;
            SaveUserInfo();
        }

        public User GetUserCache() => _user;

        private void SaveUserInfo()
        {
            using (StreamWriter sw = new StreamWriter(UsersFileName))
            {
                sw.WriteLine(JsonConvert.SerializeObject(_user, Formatting.Indented));
            }
        }
        private User GetUser()
        {
            try
            {
                using (StreamReader sr = new StreamReader(UsersFileName))
                {
                    return JsonConvert.DeserializeObject<User>(sr.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }

        }
    }
}
