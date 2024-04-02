using Audiences.Application;
using Audiences.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Audiences.Infrastructure.Foundation;
using MassTransit;
using Audiences.Api.Consumers;

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

builder.Services.AddMassTransit( x =>
{
    x.AddConsumer<AudienceConsumer>();

    x.UsingRabbitMq( ( context, cfg ) =>
    {
        cfg.Host( "rabbitmq://rabbitmq", c =>
        {
            c.Username( "guest" );
            c.Password( "guest" );
        } );
        cfg.ReceiveEndpoint( "audienceQueue", ep =>
        {
            ep.ConfigureConsumer<AudienceConsumer>( context );
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
