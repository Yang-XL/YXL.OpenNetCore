using System;

namespace Core.Log
{
    public class LogEntry
    {
        public string AppName { get; set; }

        public DateTime Date { get; set; }

        public string Ipaddress { get; set; }

        public string Level { get; set; }

        public string Logger { get; set; }

        public string Method { get; set; }

        public string Uri { get; set; }

        public Exception Exception { get; set; }

        public string Host { get; set; }

        public string Eventid { get; set; }

        public string Message { get; set; }
    }
}