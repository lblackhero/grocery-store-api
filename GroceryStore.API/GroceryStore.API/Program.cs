using GroceryStore.Application.Interfaces.Email;
using GroceryStore.Application.Interfaces.Grocery.Order;
using GroceryStore.Application.Interfaces.Grocery.Products;
using GroceryStore.Application.Interfaces.Grocery.Stock;
using GroceryStore.Application.Interfaces.User;
using GroceryStore.AzureCommunicationEmail.AzureEmailService;
using GroceryStore.Common.Functionalities;
using GroceryStore.Common.Profiler.Grocery.OrderProfiler;
using GroceryStore.Common.Profiler.Grocery.ProductProfiler;
using GroceryStore.Common.Profiler.Grocery.StockProfiler;
using GroceryStore.Domain.Entities.Identity;
using GroceryStore.Infraestructure.DatabaseContext;
using GroceryStore.Logic.Repositories.Email;
using GroceryStore.Logic.Repositories.Grocery.Orders;
using GroceryStore.Logic.Repositories.Grocery.Products;
using GroceryStore.Logic.Repositories.Grocery.Stock;
using GroceryStore.Logic.Repositories.User;
using Microsoft.AspNetCore.Identity;
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
builder.Services.AddIdentityApiEndpoints<UserEntity>(options => options.User.RequireUniqueEmail = true)
		.AddRoles<IdentityRole>()
		.AddEntityFrameworkStores<DatabaseContextUser>();

//Adicion de servicios
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IStockRepository), typeof(StockRepository));
builder.Services.AddScoped(typeof(IFunctionalities), typeof(Functionalities));
builder.Services.AddScoped(typeof(IAzureEmailService), typeof(AzureEmailService));
builder.Services.AddScoped(typeof(IEmailRepository), typeof(EmailRepository));

//Mapeo de entidades
builder.Services.AddAutoMapper(typeof(ProductProfiler).Assembly);
builder.Services.AddAutoMapper(typeof(StockProfiler).Assembly);
builder.Services.AddAutoMapper(typeof(OrderProfiler).Assembly);
builder.Services.AddAutoMapper(typeof(OrderDetailProfiler).Assembly);

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
