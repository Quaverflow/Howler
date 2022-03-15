using Microsoft.EntityFrameworkCore;

namespace ExamplesForWiseUp.Database;

public class ExampleDbContext : DbContext
{
    public static Guid AuthorizedPersonId;

    public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }

    private void OnSaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Created = DateTime.Now;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasData(new Person
        {
            Id = AuthorizedPersonId,
            Created = DateTime.UtcNow,
            Name = "Cutie",
            Surname = "Pug",
            Age = 20,
            Email = "cutiepug@personcentredsoftware.com",
            PhoneNumber = "012345678"
        });

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        OnSaveChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        OnSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
}