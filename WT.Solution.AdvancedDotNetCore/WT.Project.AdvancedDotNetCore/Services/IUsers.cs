using System.Threading.Tasks;
using WT.Project.AdvancedDotNetCore.Services.Entities;

namespace WT.Project.AdvancedDotNetCore.Services
{
    public interface IUsers
    {
        Task<User> Authenticate(string username, string password);
        Task<User> WhereIdIs(int id);
        Task<User> WithName(string name);
    }
}
