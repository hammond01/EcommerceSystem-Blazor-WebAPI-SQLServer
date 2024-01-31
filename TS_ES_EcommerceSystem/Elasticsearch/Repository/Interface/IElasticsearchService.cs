using Elasticsearch.Model;

namespace Elasticsearch.Repository.Interface
{
    public interface IElasticsearchService<T>
    {
        Task<bool> CreateDocument(T entity);
        Task<T> GetDocument(string id);
        Task<IEnumerable<T>> GetDocuments();
        List<Product> Search(string search);
        Task<bool> UpdateDocument(T entity);
        Task<bool> DeleteDocument(string id);
        Task<bool> SynData(List<T> entity);
        Product GetProductbyID(string productID);

    }
}
