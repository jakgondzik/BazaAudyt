using BazaAudyt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CzlonkowieZespolu> CzlonkowieZespolu { get; set; }
    public DbSet<LPA_Pytania> LPA_Pytania { get; set; }
    public DbSet<LPA_Wyniki> LPA_Wyniki { get; set; }
    public DbSet<LPA_PlanAudytow> LPA_PlanAudytow { get; set; }
    public DbSet<StanowiskoPracy> StanowiskaPracy { get; set; }
    public DbSet<LPA_PodsumowanieWynikow> LPA_PodsumowanieWynikow { get; set; }
    public DbSet<Konta> Konta { get; set; }

   // public DbSet<BazaAudyt.Models.Konto> Konto { get; set; } = default!;
}