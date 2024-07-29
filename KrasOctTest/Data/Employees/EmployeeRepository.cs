using Microsoft.EntityFrameworkCore;

namespace KrasOctTest.Data.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    private ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<EmployeeData>> GetEmployeesAsync()
    {
        return await _dbContext.Employees.ToListAsync();
    }
    
    public async Task<EmployeeData> CreateEmployee(string firstName, string lastName, string patronymic)
    {
        var newEmployee = new EmployeeData
        {
            Firstname = firstName,
            Lastname = lastName,
            Patronymic = patronymic
        };

        _dbContext.Employees.Add(newEmployee);
        await _dbContext.SaveChangesAsync();
        
        return newEmployee;
    }
    
    public async Task DeleteEmployee(string lastName, string firstName, string patronymic)
    {
        var employee = await _dbContext.Employees
            .Where(e => e.Lastname == lastName && e.Firstname == firstName && e.Patronymic == patronymic)
            .FirstOrDefaultAsync();
    
        if (employee != null)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Сотрудник не найден");
        }
    }
    
    public async Task UpdateEmployeeInDatabase(string oldLastName, string oldFirstName, string oldPatronymic, string newLastName, string newFirstName, string newPatronymic)
    {
        try
        {
            using (var context = _dbContext)
            {
                var employee = await context.Employees
                    .FirstOrDefaultAsync(e => e.Lastname == oldLastName && e.Firstname == oldFirstName && e.Patronymic == oldPatronymic);

                if (employee != null)
                {
                    employee.Lastname = newLastName;
                    employee.Firstname = newFirstName;
                    employee.Patronymic = newPatronymic;

                    context.Employees.Update(employee);
                    await context.SaveChangesAsync();
                }
                else
                {
                    MessageBox.Show("Сотрудник не найден.");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex}");
            Console.WriteLine(ex);
        }
    }
}