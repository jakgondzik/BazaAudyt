using BazaAudyt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CzłonekZespołu> CzlonkowieZespolu { get; set; }
    public DbSet<LPA_Pytanie> Pytania { get; set; }
    public DbSet<LPA_Wynik> LPA_Wyniki { get; set; }
    public DbSet<Audyt> PlanAudytów { get; set; }
    public DbSet<StanowiskoPracy> StanowiskaPracy { get; set; }
    public DbSet<PodsumowanieWyniku> PodsumowanieWyników { get; set; }
}