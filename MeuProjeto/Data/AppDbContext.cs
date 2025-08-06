using MeuProjeto.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Tarefa> Tarefas { get; set; }
}
