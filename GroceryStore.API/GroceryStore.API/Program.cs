using GroceryStore.Application.Interfaces.Grocery.Products;
using GroceryStore.Common.Profiler.Grocery.ProductProfiler;
using GroceryStore.Common.Profiler.Grocery.StockProfiler;
using GroceryStore.Domain.Entities.Identity;
using GroceryStore.Infraestructure.DatabaseContext;
using GroceryStore.Logic.Repositories.Grocery.Products;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Obtiene configuracion del app settings
var environment = builder.Configuration.GetSection("environment").Value;
builder.Configuration.AddJsonFile(string.Concat("appsettings.", environment, ".json"), false, true);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Contexto de base de datos
builder.Services.AddDbContext<DatabaseContextGroceryStore>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GroceryStoreConnectionString")));
builder.Services.AddDbContext<DatabaseContextUser>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GroceryStoreConnectionString")));

//Servicios de identity
builder.Services.AddIdentityApiEndpoints<UserEntity>()
		.AddEntityFrameworkStores<DatabaseContextUser>();

//Adicion de servicios
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));

//Mapeo de entidades
builder.Services.AddAutoMapper(typeof(ProductProfiler).Assembly);
builder.Services.AddAutoMapper(typeof(StockProfiler).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapIdentityApi<UserEntity>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();