using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder( args );

IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile( "ocelot.json" )
                .Build();
builder.Services.AddOcelot( configuration );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors( options => options.AddPolicy( name: "Frontend",
    policy =>
    {
        policy.WithOrigins( "http://localhost:4200" ).AllowAnyMethod().AllowAnyHeader();
    }
    ) );

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors( "Frontend" );

await app.UseOcelot();
app.Run();
