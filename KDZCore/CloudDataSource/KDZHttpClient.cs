using KDZCore.CloudDataSource.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KDZCore.CloudDataSource
{
    class KDZHttpClient
    {
        private static readonly string _baseUrl = "https://api.hseapp.ru/";

        public static async Task<List<UserResponse>> SearchUsers(string name)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            HttpResponseMessage response = await client.GetAsync("dump/search?q=" + name);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserResponse>>(responseBody);
        }

        public static async Task<List<LessonResponse>> SearchLessons(
            string type, 
            string id,
            string startDate, 
            string offset)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            HttpResponseMessage response = await client.GetAsync(
                "v3/ruz/lessons?" + type + "=" + id 
                + "&start=" + startDate + "&offset=" + offset
                );
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<LessonResponse>>(responseBody);
        }

    }
}
