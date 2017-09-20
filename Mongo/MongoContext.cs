using System;
using System.Collections.Generic;
using System.Text;
using Core.Repository.Options;
using Microsoft.Extensions.Options;
using Mongo.Models;
using MongoDB.Driver;

namespace Mongo
{
   public  class MongoContext
    {
        private readonly IMongoDatabase _db;

        public MongoContext(IOptions<MongoOptions> options)

        {
            var permissionSystem =
                MongoCredential.CreateCredential(options.Value.DataBase, options.Value.UserName,
                    options.Value.Password);
            var services = new List<MongoServerAddress>();
            foreach (var item in options.Value.Services)
            {
                services.Add(new MongoServerAddress(item.Host, item.Port));
            }
            var settings = new MongoClientSettings
            {
                Credentials = new[] { permissionSystem },
                Servers = services
            };


            var _mongoClient = new MongoClient(settings);
            _db = _mongoClient.GetDatabase(options.Value.DataBase);
        }

        public IMongoCollection<PermissionSystemLogs> PermissionSystemLogs => _db.GetCollection<PermissionSystemLogs>("PermissionSystemLogs");
        

    }
}
