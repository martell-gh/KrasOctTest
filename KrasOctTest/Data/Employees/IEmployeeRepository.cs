namespace KrasOctTest.Data.Employees;

public interface IEmployeeRepository
{
    public Task<List<EmployeeData>> GetEmployeesAsync();
    public Task<EmployeeData> CreateEmployee(string firstName, string lastName, string patronymic);
    public Task DeleteEmployee(string lastName, string firstName, string patronymic);

    public Task UpdateEmployeeInDatabase(string oldLastName, string oldFirstName, string oldPatronymic,
        string newLastName, string newFirstName, string newPatronymic);
}