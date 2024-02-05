using ElasticSearchModelBase;

namespace Elasticsearch.Repository.Interface
{
    public interface IElasticsearchService<T>
    {
        Task<bool> CreateDocument(T entity);
        Task<T> GetDocument(string id);
        Task<IEnumerable<T>> GetDocuments();
        List<EProduct> Search(string search);
        Task<bool> UpdateDocument(T entity);
        Task<bool> DeleteDocument(string id);
        Task<bool> SynData(List<T> entity);
        EProduct GetProductbyID(string productID);

    }
}
