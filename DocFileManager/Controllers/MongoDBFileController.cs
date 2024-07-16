using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Common.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocFileManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoDBFileController : ControllerBase
    {
        //private readonly MongoDb mongoDB;
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly MongoClient MongoClient;
        private readonly IConfiguration configuration;

        public MongoDBFileController(IConfiguration _configuration)// MongoDb _mongoDB
        {
            this.configuration = _configuration;
            //var MongoDbUrl = configuration["ConnectionStrings:MongoDB"];
            var MongoDbUrl = configuration.GetConnectionString("MongoDB");

            MongoClient = new MongoClient(MongoDbUrl);
            var database = MongoClient.GetDatabase("DocumentAndFiles");
            _collection = database.GetCollection<BsonDocument>("_Documents");
            //this.mongoDB = _mongoDB;
        }

        [HttpPost]
        [Route("SaveDocuments")]
        public async Task<bool> SaveDocument(List<IFormFile> Documnets)
        {
            var IsSaved = false;
            if (Documnets != null || Documnets.Count == 0)
            {
                try
                {
                    var playlistCollection = MongoClient.GetDatabase("DocumentAndFiles").GetCollection<dynamic>("_Documents");

                    foreach (var file in Documnets)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            var fileBytes = memoryStream.ToArray();
                            var document = new BsonDocument
                            {
                                {"FileName", file.FileName},
                                {"FileData", fileBytes},
                                {"ContentType", file.ContentType}
                            };
                            await _collection.InsertOneAsync(document);
                            IsSaved = true;
                        }
                    }

                    //return Ok("Files uploaded successfully.");
                    //return IsSaved;
                }
                catch (Exception ex)
                {
                    //return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading files: {ex.Message}");
                    //return IsSaved;
                }
            }
            return IsSaved;
        }
    }
}
