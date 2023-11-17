using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WorldLeague.DataAccess.Context;

namespace WorldLeague.DataAccess;

public class AdessoContextFactory : IDesignTimeDbContextFactory<AdessoContext>
{
    public AdessoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AdessoContext>();
        optionsBuilder.UseNpgsql("Server=localhost; Initial Catalog=CodebaseECommerce;Persist Security Info=False;User ID=sa;Password=MyPass@word;MultipleActiveResultSets=False;TrustServerCertificate=True;");

        return new AdessoContext(optionsBuilder.Options);
    }
}