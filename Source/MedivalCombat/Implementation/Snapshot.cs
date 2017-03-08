using System;
using System.Collections.Generic;
using MedivalCombat.API;
using Newtonsoft.Json;

namespace MedivalCombat.Implementation
{
    [Serializable]
    public class Snapshot : ISnapshot
    {
        public uint ObjectId { get; set; }

        [JsonProperty]
        private readonly Dictionary<string, object> data = new Dictionary<string, object>();

        public void Add(string key, object value)
        {
            data.Add(key, value);
        }

        public T Get<T>(string key)
        {
            return (T)data[key];
        }
    }
}