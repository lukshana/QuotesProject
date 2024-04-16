using Microsoft.EntityFrameworkCore;
using Shared;
namespace QuotesWebAPI.Data
{
    public class QuoteDbContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }
        public QuoteDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
