using Corpuses.Application;
using Corpuses.Infrastructure;
using Corpuses.Infrastructure.Foundation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UniversityFundAccounting.Shared;

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
builder.Services.AddCorpusesInfrastructure();

builder.Services.AddMassTransit( x =>
{
    x.UsingRabbitMq( ( context, cfg ) =>
    {
        cfg.Host( "rabbitmq://rabbitmq", c =>
        {
            c.Username( "guest" );
            c.Password( "guest" );
        } );
        cfg.ClearSerialization();
        cfg.UseRawJsonSerializer();
    } );
} );

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
