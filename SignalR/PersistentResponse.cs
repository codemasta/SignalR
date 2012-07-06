﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SignalR
{
    /// <summary>
    /// Represents a response to a connection.
    /// </summary>
    public class PersistentResponse
    {
        private readonly IDictionary<string, object> _transportData = new Dictionary<string, object>();

        /// <summary>
        /// The id of the last message in the connection received.
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// The list of messages to be sent to the receiving connection.
        /// </summary>
        public IEnumerable<string> Messages { get; set; }

        /// <summary>
        /// True if the connection receives a disconnect command.
        /// </summary>
        public bool Disconnect { get; set; }

        /// <summary>
        /// True if the connection was forcibly closed. 
        /// </summary>
        [JsonIgnore]
        public bool Aborted { get; set; }

        /// <summary>
        /// True if the connection timed out.
        /// </summary>
        public bool TimedOut { get; set; }

        /// <summary>
        /// Transport specific configurtion information.
        /// </summary>
        public IDictionary<string, object> TransportData
        {
            get { return _transportData; }
        }

        public IJsonString Json { get; set; }

        public void FillJson()
        {
            var json =
                "{\"MessageId\":\"" + MessageId + "\"," +
                "\"Disconnect\":" + (Disconnect ? "true" : "false") + "," +
                 "\"Aborted\":" + (Aborted ? "true" : "false") + "," +
                 // TODO: Groups, state, etc.
                 "\"Messages\":[" + String.Join(",", Messages) + "]" +
                "\"}";

            Json = new JsonString(json);
        }
    }
}