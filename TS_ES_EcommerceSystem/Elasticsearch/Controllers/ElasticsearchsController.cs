using Dapper;
using Elasticsearch.Repository.Interface;
using ElasticSearchModelBase;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticsearchsController : ControllerBase
    {
        private readonly IElasticsearchService<EProduct> _repo;
        public ElasticsearchsController(IElasticsearchService<EProduct> repo)
        {
            _repo = repo;
        }
        [HttpPost("add")]
        public async Task<IActionResult> Create(EProduct product)
        {
            var success = await _repo.CreateDocument(product);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Failed to create product.");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _repo.GetDocument(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }
        [HttpGet("gets")]
        public async Task<IActionResult> Gets()
        {
            var product = await _repo.GetDocuments();
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(EProduct product)
        {
            var success = await _repo.UpdateDocument(product);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Failed to update product.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _repo.DeleteDocument(id);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Failed to delete product.");
        }
        [HttpGet("syn-data")]
        public async Task<IActionResult> SynData()
        {
            var query = "SELECT ProductID, ProductName, UnitPrice FROM Products";
            var products = (await Program.Sql.QueryAsync<EProduct>(query)).AsList();
            var syn = await _repo.SynData(products);
            if (syn)
            {
                return Ok(syn);
            }

            return NotFound();
        }
        [HttpGet("search")]
        public IActionResult Search(string word)
        {
            var product = _repo.Search(word);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }
        [HttpGet("/get-product/{word}")]
        public IActionResult GetProductbyID(string word)
        {
            var product = _repo.GetProductbyID(word);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

    }
}
