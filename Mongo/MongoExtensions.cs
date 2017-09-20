using Core.Repository.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mongo
{
    public  static class MongoExtensions
    {
        public static IServiceCollection UserMongoLog(this IServiceCollection services,IConfigurationSection configurationSection)
        {
            services.Configure<MongoOptions>(configurationSection);
            services.AddSingleton<MongoContext>();
            return services;
        }
    }
}
