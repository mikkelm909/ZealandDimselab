using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Helpers
{
    /// <summary>
    /// Gives access to store json files temporarily in the session.  
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// Serializes objects into json files, and stores it..
        /// </summary>
        /// <param name="session">The current session</param>
        /// <param name="key">Key to store and retrieve the data from</param>
        /// <param name="value">The object you want to serialize.</param>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize<object>(value));
        }

        /// <summary>
        /// Deserialize objects from the specified key.
        /// </summary>
        /// <typeparam name="T">The value of what you want to deserialize.</typeparam>
        /// <param name="session">The current session</param>
        /// <param name="key">The key, that the data is stored under.</param>
        /// <returns></returns>
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }

        public static int Exists(List<Item> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
