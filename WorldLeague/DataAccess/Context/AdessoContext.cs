using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WorldLeague.Entities;

namespace WorldLeague.DataAccess.Context;

public class AdessoContext : DbContext
{
    public AdessoContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Drawer> Drawers { get; set; }
    
}