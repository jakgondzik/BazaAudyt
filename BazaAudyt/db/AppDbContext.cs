using BazaAudyt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CzłonekZespołu> CzlonkowieZespolu { get; set; }
    public DbSet<LPA_Pytanie> LPA_Pytania { get; set; }
    public DbSet<LPA_Wynik> LPA_Wyniki { get; set; }
    public DbSet<Audyt> LPA_PlanAudytow { get; set; }
    public DbSet<StanowiskoPracy> StanowiskaPracy { get; set; }
    public DbSet<PodsumowanieWyniku> LPA_PodsumowanieWynikow { get; set; }

public DbSet<BazaAudyt.Models.Konto> Konto { get; set; } = default!;
}