using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Common.Web
{
    public class MongoDb
    {
        private readonly IConfiguration _configuration;
        private readonly string MongoDbUrl;

        public MongoDb(IConfiguration configuration)
        {
            _configuration = configuration;
            MongoDbUrl = _configuration["ConnectionStrings:MongoDB"];

            MongoClient client = new MongoClient(MongoDbUrl);

            List<string> databases = client.ListDatabaseNames().ToList();
            foreach (string database in databases)
            {
                Console.WriteLine(database);
            }
        }

        public MongoClient GetClient()
        {
            MongoClient client = new MongoClient(MongoDbUrl);
            return client;

            //var playlistCollection = client.GetDatabase("sample_mflix").GetCollection<Playlist>("playlist");

            //List<string> movieList = new List<string>();
            //movieList.Add("1234");

            //playlistCollection.InsertOne(new Playlist("nraboy", movieList));
        }
    }
    
}

