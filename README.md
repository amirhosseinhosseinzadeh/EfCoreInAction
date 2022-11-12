The modeling steps which ef core will use is at the first time that SomeDbContext being initilaize it reads all the models registred as `DbSet<T>` insinde that dbcontext
After that the models are `cached` ,so subsequent instances are created faster.

* EfCore looks through the `dbcontext` so finds all public `DbSet<T>` and define an initial name for tables
* EfCore looks through the all the classes referred to in `DbSet<T>` and look for that class properties to find the tables's columns ,datatypes and so forth. it also
looks for special attribute which provide more configurations for that table
* EfCore looks for any classes that the `DbSet<T>` classes referre to so it can finds the relationships between the tables
* for the last input to the modeling process EfCore runs the virtual method `OnModelCreating` which can provide extra configurations using `fluent apis`
* EfCore will creates an internal model from database base on information gathered through the modeling process and then cache the model so than later data access will be quicke then
this model is used for performing add database access

EfCore creates internal model from the registered classes through the application and not database it self so its important to create a good model from database otherwise
problems could apears if a mismatch  exists between dataabase and model.

Here is a sample reading data from dbcontext:
<details><summary>Collapse</summary>
```
public static void ListAll()
{
	using var dbContext = new ApplicationDbContext();
	var dbSet = dbContext.Set<T>();
	var records = dbSet.Include(x => x.ReferencedTable).ToList();
	records.ForEach(x => Console.WriteLine(x.Name));
}
```
</details>