using Microsoft.EntityFrameworkCore;

namespace adventureWorks.webApi.employees;

public class EmployeeRepo : IEmployeeRepo
{
    private readonly EmployeeDbContext _employeeDbContext;
    public EmployeeRepo(EmployeeDbContext employeeDbContext)
    {
        _employeeDbContext = employeeDbContext ?? throw new ArgumentNullException(nameof(employeeDbContext));
    }

    public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees(CancellationToken cancellationToken) => await
            (from e in _employeeDbContext.Employees
             join p in _employeeDbContext.People
                 on e.BusinessEntityId equals p.BusinessEntityId
             select new EmployeeInfo()
             {
                 // employee
                 BusinessEntityId = e.BusinessEntityId,
                 NationalIdnumber = e.NationalIdnumber,
                 LoginId = e.LoginId,
                 OrganizationLevel = e.OrganizationLevel,
                 JobTitle = e.JobTitle,
                 BirthDate = e.BirthDate,
                 MaritalStatus = e.MaritalStatus,
                 Gender = e.Gender,
                 HireDate = e.HireDate,
                 SalariedFlag = e.SalariedFlag,
                 VacationHours = e.VacationHours,
                 SickLeaveHours = e.SickLeaveHours,
                 CurrentFlag = e.CurrentFlag,
                 EmployeeRowguid = e.Rowguid,
                 EmployeeModifiedDate = e.ModifiedDate,
                 // person
                 PersonType = p.PersonType,
                 NameStyle = p.NameStyle,
                 Title = p.Title,
                 FirstName = p.FirstName,
                 MiddleName = p.MiddleName,
                 LastName = p.LastName,
                 Suffix = p.Suffix,
                 EmailPromotion = p.EmailPromotion,
                 AdditionalContactInfo = p.AdditionalContactInfo,
                 Demographics = p.Demographics,
                 PersonRowguid = p.Rowguid,
                 PersonModifiedDate = p.ModifiedDate,
             }
         ).ToListAsync(cancellationToken);
}
