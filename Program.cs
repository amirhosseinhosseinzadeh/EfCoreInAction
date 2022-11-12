#nullable disable
using Microsoft.EntityFrameworkCore;

using var dbContext = new ApplicationDbContext();

await dbContext.Database.MigrateAsync();

var bookDbSet = dbContext.Set<Book>();

var books = await bookDbSet.Include(x => x.Author).ToListAsync();

books.ForEach(x => Console.WriteLine(x.Name));


public class ApplicationDbContext : DbContext
{
    private string _connectionString = "Server=(localdb)\\MSSQLLocalDb;Database=EfCoreInAction;Integrated Security=True;MultipleActiveResultSets=True;";

    public ApplicationDbContext()
    {
            
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Author>(x => x.HasData(new Author[]
        {
            new(1,"Robert david","https://robert.com"),
            new(2,"Bob david","https://robert.com"),
        }));

        builder.Entity<Book>(x => x.HasData(new Book[]
        {
            new (1,"Clean code",2),
            new (2,"Onion architecture",1),
            new (3,"Test migrations",1)
        }));
    }

    public DbSet<Book> Books { get; set; }

    public DbSet<Author> Authors { get; set; }
}

public class Book
{
    public Book(int id ,string name, int authorId)
    {
        Id = id;
        Name = name;
        AuthorId = authorId;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public int AuthorId { get; set; }

    public Author Author { get; set; }
}

public class Author
{
    public Author(int id, string fullName, string webUrl)
    {
        Id = id;
        FullName = fullName;
        WebUrl = webUrl;
    }

    public int Id { get; set; }

    public string FullName { get; set; }

    public string WebUrl { get; set; }
}
