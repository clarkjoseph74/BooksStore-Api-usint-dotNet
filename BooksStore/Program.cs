using BooksStore.Data;
using BooksStore.Data.Models;
using BooksStore.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BooksStore.Extenstions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("mainConnection")));
// Add services to the container.
builder.Services.AddScoped<IBookRepo , BookRepo>();
builder.Services.AddIdentity<User , IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllers().AddNewtonsoftJson( options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfigsJwt();

builder.Services.AddCustomJwtAuth(builder.Configuration);

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
