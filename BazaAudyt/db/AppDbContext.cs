using BazaAudyt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }
    public String loggedConnectionString = "Data Source=KUBA-KOMPUTER;Database=Audyty;User Id=audytor;Password=audytor;Integrated Security=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
    public static String newConnectionString = "";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(newConnectionString!="")
        {
            optionsBuilder.UseSqlServer(newConnectionString);
        }
        else
        {
            optionsBuilder.UseSqlServer(loggedConnectionString);
        }
        
    }
    public AppDbContext()
    {

    }
    public DbSet<CzlonkowieZespolu> CzlonkowieZespolu { get; set; }
    public DbSet<LPA_Pytania> LPA_Pytania { get; set; }
    public DbSet<LPA_Wyniki> LPA_Wyniki { get; set; }
    public DbSet<LPA_PlanAudytow> LPA_PlanAudytow { get; set; }
    public DbSet<StanowiskoPracy> StanowiskaPracy { get; set; }
    public DbSet<LPA_PodsumowanieWynikow> LPA_PodsumowanieWynikow { get; set; }
    public DbSet<Konta> Konta { get; set; }

    public DbSet<AudytyWidok> AudytyWidok { get; set; }
    // public DbSet<BazaAudyt.Models.Konto> Konto { get; set; } = default!;
}