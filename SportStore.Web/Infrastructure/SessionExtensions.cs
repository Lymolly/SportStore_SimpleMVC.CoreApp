using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SportStore.Web.Models;

namespace SportStore.Web.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session,string key,object value)
        {
            string serObj = JsonConvert.SerializeObject(value as Cart);
            session.SetString(key,serObj);
        }
        public static T GetJson<T>(this ISession session,string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null 
                ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}