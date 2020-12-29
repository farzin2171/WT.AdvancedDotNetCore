using System.Threading.Tasks;
using WT.Project.AdvancedDotNetCore.Services.Entities;

namespace WT.Project.AdvancedDotNetCore.Services
{
    public interface IEmployees
    {
        Task<Employee[]> All();
        Task<Employee> WithId(int employeeId);
        Task<Employee> WithId(int tenantId, int employeeId);
        Task<Employee> Add(string firstName, string lastName, string title);
    }
}
