
using Microsoft.EntityFrameworkCore;

class Program
{
    public static void Main(string[] args)
    {
       using(VBContext context=new VBContext())
        {
            var dataHistory = context.Customers.TemporalAll().ToList();
            foreach (var item in dataHistory)
            {
                Console.WriteLine(item.CustomerName);
            }
            Console.WriteLine("========================================");
            var dataHistory2=context.Customers
                .TemporalAll()
                .Where(c=>c.Id==1)
                .ToList();
            foreach (var item in dataHistory2)
            {
                Console.WriteLine(item.CustomerName);
            }
            Console.WriteLine("========================================");
            var dataHistory3 = context.Customers
                .TemporalAll()
                .Where(c => c.Id == 1)
                .Select(cust => new
                {
                    cust.CustomerName,
                    ValidFrom=EF.Property<DateTime>(cust,"PeriodStart"),
                    ValidTo = EF.Property<DateTime>(cust, "PeriodEnd")
                })
                .ToList();

            foreach (var item in dataHistory3)
            {
                Console.WriteLine(item.CustomerName+"   "+item.ValidFrom+"    "+item.ValidTo); 
            }
        }
    }
}
public class VBContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"data source=(localdb)\mssqllocaldb;database=rajcustomerorderdb;trusted_connection=true");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customers", b => b.IsTemporal());
    }
}
public abstract class BaseDomainModel
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
}
public class Customer:BaseDomainModel
{
    public string? CustomerName { get; set; }
    public string? CustomerAddress { get; set; }
    public ICollection<Order> Orders { get; set; }
}
public class Order:BaseDomainModel
{
    public string? OrderName { get; set; }
    public int CustomerId { get; set; }
    public string? OrderDesc { get; set; }
    public Customer Customer { get; set; }
}