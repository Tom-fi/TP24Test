using Microsoft.EntityFrameworkCore;
using TP24.Api.Invoices;
using TP24.Api.Summaries;
using TP24.Domain.Interfaces;
using TP24.Infrastructure;
using TP24.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<SummaryService>();

builder.Services.AddDbContext<EFContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TP24Test")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
