using Dapper;
using Elasticsearch.Model;
using Elasticsearch.Repository.Interface;
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
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var success = await _repo.CreateDocument(product);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Failed to create product.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _repo.GetDocument(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var product = await _repo.GetDocuments();
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var success = await _repo.UpdateDocument(product);
            if (success)
            {
                return Ok();
            }
            return BadRequest("Failed to update product.");
        }

        [HttpDelete("{id}")]
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
            var products = (await Program.Sql.QueryAsync<Product>(query)).AsList();
            var syn = await _repo.SynData(products);
            //var count = products.Count();
            //for (var i = 0; i < count; i++)
            //{
            //    var product = await _repo.CreateDocument(products[i]);
            //    if (i == count - 1)
            //    {
            //        await Console.Out.WriteLineAsync("Index" + i);
            //        return Ok(product);
            //    }
            //}
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
