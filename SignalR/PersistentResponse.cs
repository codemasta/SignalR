﻿using System;
using System.Collections.Generic;
using System.IO;
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
        public IList<string> Messages { get; set; }

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

        public string AsJson()
        {
            using (var sw = new StringWriter())
            using (var writer = new JsonTextWriter(sw))
            {
                writer.WriteStartObject();
                
                writer.WritePropertyName("MessageId");
                writer.WriteValue(MessageId);

                writer.WritePropertyName("Disconnect");
                writer.WriteValue(Disconnect);

                writer.WritePropertyName("Aborted");
                writer.WriteValue(Aborted);

                //writer.WritePropertyName("TransportData");
                //writer.WriteValue(TransportData);

                writer.WritePropertyName("Messages");
                writer.WriteStartArray();
                for (var i = 0; i < Messages.Count; i++)
                {
                    writer.WriteRawValue(Messages[i]);
                }
                writer.WriteEndArray();

                writer.WriteEndObject();

                return sw.ToString();
            }
        }
    }
}