using Interface.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DOANV1.Extensions.Service.MongDb
{
    public static class MongoDb_Client_Static
    {

        

        public static void ConfigureMongoDbClient(this IServiceCollection services, IConfiguration Configuration) 

        {

            //Add service to the container
                BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // Định dạng  ID thành chuỗi kiểu Guid ;; giúp cho Mongo có thể hiển thị dạng chuỗi Json được

                BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));// Định dạng DateoffTime thành chuỗi kiểu ;; giúp cho Mongo có thể hiển thị dạng chuỗi Json được
            
                BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));// Định dạng DateoffTime thành chuỗi kiểu ;; giúp cho Mongo có thể hiển thị dạng chuỗi Json được




            //ConnectMongoClient
            services.AddSingleton<IMongoClient>(ServiceProvider =>
            {
                var connect = Configuration.GetSection(nameof(ConnectMongoDbClient)).Get<ConnectMongoDbClient>();

                return new MongoClient(connect.ConnectionString);

            });

        
        
        }

    }
}
