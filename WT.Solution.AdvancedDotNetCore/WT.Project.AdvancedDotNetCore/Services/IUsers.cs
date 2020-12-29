using System.Threading.Tasks;
using WT.Project.AdvancedDotNetCore.Services.Entities;

namespace WT.Project.AdvancedDotNetCore.Services
{
    public interface IUsers
    {
        Task<User> Authenticate(string username, string password);
    }
}
