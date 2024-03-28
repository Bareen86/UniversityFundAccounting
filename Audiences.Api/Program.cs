using Audiences.Application;
using Audiences.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Audiences.Infrastructure.Foundation;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile( "appsettings.json" )
                .Build();

string connectionString = configuration[ "ConnectionStrings:AudiencesDbConnectionString" ];
builder.Services.AddDbContext<AudiencesDbContext>( db => db.UseNpgsql( connectionString,
    db => db.MigrationsAssembly( "Audiences.Infrastructure" ) ) );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAudiencesAppication();
builder.Services.AddAudienceInfrastructire();

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
