namespace DOANV1.Extensions.Service.MongDb
{
    public class ConnectMongoDbClient
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        { 
            get
            {

                return $"mongodb://{User}:{Password}@{Host}:{Port}";
                //return $"mongodb://{Host}:{Port}";

            }    
        }


    }
}
