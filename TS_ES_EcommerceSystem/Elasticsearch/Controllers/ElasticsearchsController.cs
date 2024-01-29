using Elasticsearch.Model;
using Elasticsearch.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticsearchsController : ControllerBase
    {
        private readonly IElasticsearchService<Product> _repo;

        public ElasticsearchsController(IElasticsearchService<Product> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var data = await _repo.GetDocuments();
            return Ok(data);
        }
    }
}
