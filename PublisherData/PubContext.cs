using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData;
public class PubContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
                  @"Server=127.0.0.1,1433; Database=PubDatabase; User Id=SA; Password=MyPass@word;"
            );
    }
}