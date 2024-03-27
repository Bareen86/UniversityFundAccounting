using Corpuses.Application;
using Corpuses.Infrastructure.Foundation;
using Corpuses.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile( "appsettings.json" )
                .Build();

string connectionString = configuration[ "ConnectionStrings:CoursesDbConnectionString" ];
builder.Services.AddDbContext<CorpusesDbContext>( db => db.UseNpgsql( connectionString,
    db => db.MigrationsAssembly( "Corpuses.Infrastructure" ) ) );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCorpusesBindings();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
