using CRUDEFcore.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "V# pi",
        Version = "v1",
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme= "Bearer",
        BearerFormat ="Jwt",
        In= ParameterLocation.Header,
        Description = "Pss toen cey"

    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
   {
        { new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {

                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
          },
          Array.Empty<string>() }
        




    });

});



builder.Services.AddDbContext<CtegoriesContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MyAppConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiCorspolicy", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

string secricey = builder.Configuration.GetSection("Api").Value;
byte[] cey = Encoding.ASCII.GetBytes(secricey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(cey),
        ValidateIssuer = false,
        ValidateAudience = false


    };
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ApiCorspolicy");


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
