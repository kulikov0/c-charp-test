using KDZCore.CloudDataSource.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace KDZCore.CloudDataSource
{
    interface KDZCloudDataSource
    {
        Task<List<UserResponse>> SearchUsers(string name);
        Task<List<LessonResponse>> SearchLessons(
            string type,
            string id,
            string startDate,
            string offset);
    }
    class KDZCloudDataSourceImpl: KDZCloudDataSource
    {
        Task<List<UserResponse>> KDZCloudDataSource.SearchUsers(string name) => KDZHttpClient.SearchUsers(name);

        Task<List<LessonResponse>> KDZCloudDataSource.SearchLessons(
            string type,
            string id,
            string startDate,
            string offset) => KDZHttpClient.SearchLessons(type, id, startDate, offset);
    }
}
