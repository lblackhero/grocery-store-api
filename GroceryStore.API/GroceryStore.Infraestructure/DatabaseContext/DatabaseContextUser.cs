using GroceryStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Infraestructure.DatabaseContext;

/// <summary>
/// Contexto de la base de datos para identity
/// </summary>
/// <param name="options"></param>
public class DatabaseContextUser(DbContextOptions<DatabaseContextUser> options) : IdentityDbContext<UserEntity>(options)
{
}