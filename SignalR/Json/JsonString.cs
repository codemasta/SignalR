using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalR
{
    public class JsonString : IJsonString
    {
        public string Value
        {
            get;
            private set;
        }

        public JsonString(string jsonString)
        {
            Value = jsonString;
        }
    }
}
