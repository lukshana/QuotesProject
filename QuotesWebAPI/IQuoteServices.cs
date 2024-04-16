using Microsoft.AspNetCore.Mvc;
using Shared;

namespace QuotesWebAPI
{
    public interface IQuoteServices
    {
        public void AddQuote(Quote newQuote);
        public Task<ActionResult<List<Quote>>> GetQuotes();
        public Quote GetQuote(int id);
        public void UpdateQuote(Quote updatedquote);
       public void DeleteQuote(int id);
        int IfExists(int id);
        Task<ActionResult<IEnumerable<Quote>>> SearchQuotes(string? author = null, string? tags = null, string? quote = null);
    }
}
