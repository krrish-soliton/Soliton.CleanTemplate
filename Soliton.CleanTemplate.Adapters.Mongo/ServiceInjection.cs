using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Soliton.CleanTemplate.Core;
using Soliton.Shared;
using System;

namespace Soliton.CleanTemplate.Adapters.Mongo
{
    public static class ServiceInjection
    {
        public static void RegisterDataAdapter(this IServiceCollection services)
        {
            ConventionPack snakeCaseConventionPack = [];
            snakeCaseConventionPack.AddMemberMapConvention("SnakeCaseConvention", m => m.SetElementName(m.ElementName.ToSnakeCase()));
            ConventionRegistry.Register("SnakeCaseConvention", snakeCaseConventionPack, __ => true);
            services.AddSingleton(_ =>
            {
                string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "mongodb://localhost:27017";
                MongoClient client = new(connectionString);
                return client.GetDatabase("solitondb");
            });
            BsonClassMap.RegisterClassMap<SolitonUser>(classMap =>
            {
                classMap.AutoMap();
                classMap.MapIdMember(setting => setting.Id).SetIdGenerator(new StringObjectIdGenerator()).SetSerializer(new StringSerializer(BsonType.String));
                classMap.SetIgnoreExtraElements(true);
            });
            services.AddSingleton<ISolitonRepository, SolitonRepository>();
        }
    }
}
