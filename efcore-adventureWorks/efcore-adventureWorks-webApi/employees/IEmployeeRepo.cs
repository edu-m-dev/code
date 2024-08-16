namespace adventureWorks.webApi.employees;

public interface IEmployeeRepo
{
    Task<IEnumerable<EmployeeInfo>> GetAllEmployees(CancellationToken cancellationToken);
}
