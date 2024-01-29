namespace Elasticsearch.Repository.Interface
{
    public interface IElasticsearchService<T>
    {
        Task<string> CreateDocument(T entity);
        Task<T> GetDocument(int id);
        Task<IEnumerable<T>> GetDocuments();
        Task<string> UpdateDocument(T entity);
        Task<string> DeleteDocument(int id);

    }
}
