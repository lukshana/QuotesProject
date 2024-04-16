using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesWebAPI.Data;
using Shared;
using System.Linq;

namespace QuotesWebAPI
{
    public class QuoteServices : IQuoteServices
    {
        private readonly QuoteDbContext _context;
        public QuoteServices(QuoteDbContext context)
        {
            this._context = context;
        }
       
        public void AddQuote(Quote newQuote)
        {

            _context.Quotes.Add(newQuote);
            _context.SaveChanges();
        }
        public async Task<ActionResult<List<Quote>>> GetQuotes()
        {
            return await _context.Quotes.ToListAsync();
        }
        public Quote GetQuote(int id)
        {
            return _context.Quotes.Find(id);
            
        }
        public void UpdateQuote(Quote updatedquote)
        {
            try
            {
                _context.Entry(updatedquote).State = EntityState.Modified;

                _context.SaveChanges();
            }
            catch
            {
                throw;
            }

        }
        public int IfExists(int id)
        {
            var quote = _context.Quotes.FirstOrDefaultAsync(e => e.Id == id);
            if(quote==null) { return  0; }else { return 1; }
        }
        public void DeleteQuote(int id)
        {
            Quote quote = new();
            try
            {
                 quote = _context.Quotes.Find(id);
                if (quote != null)
                {
                    _context.Quotes.Remove(quote);
                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
            
        }
        public async Task<ActionResult<IEnumerable<Quote>>> SearchQuotes(string? author = null, string? tags = null, string? quote = null)
        {
            IQueryable<Quote> query = _context.Quotes;

            // Filter by author
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(q => q.Author.ToLower().Contains(author.ToLower()));
            }

            // Filter by tags
            if (!string.IsNullOrEmpty(tags))
            {
                var tagList = tags.Split(',');
                query = query.Where(q => q.Tags.Any(t => tagList.Contains(t)));
            }

            // Filter by quote
            if (!string.IsNullOrEmpty(quote))
            {
                query = query.Where(q => q.QuoteText.ToLower().Contains(quote.ToLower()));
            }

            var result = await query.ToListAsync();
            return result;
        }

    }
}
