using KDZCore.CacheDataSource;
using KDZCore.CloudDataSource;
using KDZCore.CloudDataSource.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KDZCore.Repository
{
    public interface KDZRepository
    {
        public User GetUser();
        public void SaveUser(User user);

        public Task<List<UserResponse>> SearchUsers(string name);
        public Task<List<LessonResponse>> SearchLessons(string type, string id, string startDate, string offset);
    }

    public class KDZRepositoryImpl : KDZRepository
    {
        private static KDZRepositoryImpl instance;

        private readonly UserCacheDataSource userCache = new UserCacheDataSourceImpl();

        private readonly KDZCloudDataSource cloud = new KDZCloudDataSourceImpl();

        public static KDZRepositoryImpl GetInstance()
        {
            if (instance == null) instance = new KDZRepositoryImpl();
            return instance;
        }
        public User GetUser() => userCache.GetUserCache();
        public void SaveUser(User user) => userCache.SaveUser(user);
        public Task<List<UserResponse>> SearchUsers(string name) => cloud.SearchUsers(name);
        public Task<List<LessonResponse>> SearchLessons(
            string type,
            string id,
            string startDate,
            string offset) => cloud.SearchLessons(type, id, startDate, offset);

    }
}
