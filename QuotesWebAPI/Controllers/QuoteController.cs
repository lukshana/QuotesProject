using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesWebAPI.Data;
using Shared;
using Syncfusion.Blazor.Kanban.Internal;

namespace QuotesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteServices _service;
        public QuoteController(IQuoteServices service)
        {
            _service = service;
        }

        [HttpPost]
        public void Post(Quote newQuote)
        {
            _service.AddQuote(newQuote);
        }

        // GET: api/Quote
        [HttpGet]
        public async Task<ActionResult<List<Quote>>> Get()
        {
            return await _service.GetQuotes();
        }

        // GET: api/Quote/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Quote quote =  _service.GetQuote(id);

            if (quote == null)
            {
                return NotFound();
            }

            return Ok(quote);
        }

        [HttpPut]
        public void Put(Quote updatedquote)
        {
             _service.UpdateQuote(updatedquote);
         
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuote(int id)
        {
          _service.DeleteQuote(id);
           return Ok();
           
        }

        // GET: api/Quotes/Search
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Quote>>> SearchQuotes(string? author = null, string? tags = null, string? quote = null)
        {
            var result = await _service.SearchQuotes(author,tags,quote);
            return result;
        }

        [HttpGet("CheckId")]
        public int IfExists(int id)
        {
            var result = _service.IfExists(id);
            return result;
        }
    }
}
   