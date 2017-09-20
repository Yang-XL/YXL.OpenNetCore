﻿using System;
using System.Collections.Generic;

namespace Core.Repository.Options
{
    public class MongoOptions
    {
        public string DataBase { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<MongoServers> Services { get; set; }

    }
    public class MongoServers
    {
        public string Host { get; set; }

        public int Port { get; set; } = 27017;
    }
}
