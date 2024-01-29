using Elasticsearch.Repository.Interface;
using Nest;

namespace Elasticsearch.Repository.Services
{
    public class ElasticsearchRepository<T> : IElasticsearchService<T> where T : class
    {
        private readonly ElasticClient _elasticClient;

        public ElasticsearchRepository(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<string> CreateDocument(T entity)
        {
            var response = await _elasticClient.IndexDocumentAsync(entity);
            return response.IsValid ? "Created" : "Failed";
        }

        public async Task<string> DeleteDocument(int id)
        {
            var response = await _elasticClient.DeleteAsync(new DocumentPath<T>(id));
            return response.IsValid ? "Deleted" : "Failed";
        }

        public async Task<T> GetDocument(int id)
        {
            var response = await _elasticClient.GetAsync(new DocumentPath<T>(id));
            return response.Source;
        }

        public async Task<IEnumerable<T>> GetDocuments()
        {
            var searchResponse = await _elasticClient.SearchAsync<T>(s => s
                .MatchAll()
                .Size(10));
            return searchResponse.Documents;
        }

        public async Task<string> UpdateDocument(T entity)
        {
            var response = await _elasticClient.UpdateAsync(new DocumentPath<T>(entity), u => u
                .Doc(entity)
                .RetryOnConflict(3)
            );
            return response.IsValid ? "Updated" : "Failed";

        }
    }
}
