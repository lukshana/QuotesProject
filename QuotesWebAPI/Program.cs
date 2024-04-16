using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using QuotesWebAPI;
using QuotesWebAPI.Data;
using Microsoft.AspNetCore.ResponseCompression;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IQuoteServices, QuoteServices>();
builder.Services.AddDbContext<QuoteDbContext>(Options =>
{
    Options.UseSqlite(builder.Configuration.GetConnectionString("QuoteDB"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   
}
app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);
//app.UseCors(policy=>
//policy.All
//   // policy.WithOrigins("https://localhost:7291", "https://localhost:7291/").AllowAnyMethod().WithHeaders(HeaderNames.ContentType)
//    );
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapRazorPages();

app.MapControllers();

app.Run();
