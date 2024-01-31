using Elasticsearch.Model;
using Elasticsearch.Net;
using Elasticsearch.Repository.Interface;
using Nest;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Elasticsearch.Repository.Services
{
    public class ElasticsearchRepository<T> : IElasticsearchService<T> where T : class
    {
        private readonly ElasticClient _elasticClient;

        public ElasticsearchRepository(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<bool> CreateDocument(T entity)
        {
            var response = await _elasticClient.IndexDocumentAsync(entity);
            return response.IsValid;
        }

        public async Task<bool> SynData(List<T> entities)
        {
            try
            {
                var bulkAllObservable = await _elasticClient.IndexManyAsync(entities);
                if (bulkAllObservable.IsValid)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }



        public async Task<bool> DeleteDocument(string id)
        {
            var response = await _elasticClient.DeleteAsync(new DocumentPath<T>(id));
            return response.IsValid;
        }

        public async Task<T> GetDocument(string id)
        {
            var response = await _elasticClient.GetAsync(new DocumentPath<T>(id));
            return response.Source;
        }

        public async Task<IEnumerable<T>> GetDocuments()
        {
            var searchResponse = await _elasticClient.SearchAsync<T>(s => s
                .MatchAll()
                .Size(10000));
            return searchResponse.Documents;
        }

        public async Task<bool> UpdateDocument(T entity)
        {
            var response = await _elasticClient.UpdateAsync(new DocumentPath<T>(entity), u => u
                .Doc(entity)
                .RetryOnConflict(3)
            );
            return response.IsValid;

        }

        public List<Product> Search(string search)
        {
            var searchResponse = _elasticClient.Search<Product>(s => s
                .Query(q => q
                    .Prefix(c => c
                               .Field(p => p.ProductName)
                                .Value(search)))
            //.Size(100)
            );

            if (searchResponse.IsValid)
            {
                return searchResponse.Documents.ToList();
            }
            else
            {
                return new List<Product>();
            }
        }

        public Product GetProductbyID(string search)
        {
            var searchResponse = _elasticClient.Search<Product>(s => s
        .Query(q => q
            .Term(t => t
                .Field(f => f.ProductID)
                .Value(search)
            )
        )
        .Size(1)  // Assuming you want only one result
    );
            if (searchResponse.IsValid && searchResponse.Documents.Any())
            {
                var idDocument = searchResponse.Hits.First().Id;
                return searchResponse.Documents.Single();
            }
            else
            {
                return new Product();
            }
        }
    }
}
