using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)

        {
            session.SetString(key, JsonConvert.SerializeObject(
                value,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                }));
        }

        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(
                value,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
        }
    }
}
