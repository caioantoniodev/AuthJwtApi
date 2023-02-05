using System.Text;
using AuthJwtApi.Application.Config;
using AuthJwtApi.Application.Ports.In;
using AuthJwtApi.Application.Ports.Out;
using AuthJwtApi.Application.Services;
using AuthJwtApi.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add config to the container.

builder.Services.Configure<AppSettings>(builder.Configuration);
var appSettings = builder.Configuration.Get<AppSettings>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.SecurityKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddTransient<ICustomerServicePortIn, CustomerService>();
builder.Services.AddTransient<ICacheRepositoryPortOut, CacheRepository>();
builder.Services.AddSingleton<IConnectionMultiplexer>
    (ConnectionMultiplexer.Connect(appSettings.DefaultConnection.CacheConnectionString));
builder.Services.AddScoped<ITokenServicePortIn, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();