
using Microsoft.EntityFrameworkCore;

namespace KrasOctTest.Data.Employees;

public class EmployeeDbContext : DbContext
{
    public DbSet<EmployeeData>? Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True;Encrypt=False;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeData>(entity =>
        {
            entity.ToTable("Employees");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Firstname).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Lastname).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
        });
    }
    
    public async Task CreateEmployee(DataGridView dataGridView, string firstName, string lastName, string patronymic)
    {
        var newEmployee = new EmployeeData
        {
            Firstname = firstName,
            Lastname = lastName,
            Patronymic = patronymic
        };

        Employees.Add(newEmployee);
        await SaveChangesAsync();
        
        dataGridView.Rows.Add(firstName, lastName, patronymic);
    }
    
    public async Task DeleteEmployee(string lastName, string firstName, string patronymic)
    {
        var employee = await Employees
            .Where(e => e.Lastname == lastName && e.Firstname == firstName && e.Patronymic == patronymic)
            .FirstOrDefaultAsync();
    
        if (employee != null)
        {
            Employees.Remove(employee);
            await SaveChangesAsync();
        }
        else
        {
            throw new Exception("Сотрудник не найден");
        }
    }

    public async Task InitializeDatabase()
    {
        try
        {
            if (await Database.EnsureCreatedAsync())
            {
                Console.WriteLine("Таблица Employees создана.");
            }
            else
            {
                Console.WriteLine("Таблица Employees уже существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred: " + ex.Message);
            throw;
        }
    }


}