using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WT.Project.AdvancedDotNetCore.Services.Entities;

namespace WT.Project.AdvancedDotNetCore.Services
{
    public class Users : IUsers
    {
        private InternalUser[] _users;
        public Users(IWebHostEnvironment env)
        {
            _users = JsonConvert.DeserializeObject<InternalUser[]>(File.ReadAllText(env.ContentRootFileProvider.GetFileInfo("Data/users.json").PhysicalPath));
        }
        public Task<User> Authenticate(string username, string password)
        {
            var user = _users.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            //if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            //{
            //    return Task.FromResult<User>(null);
            //}

            return Task.FromResult(user.AsUser());
        }
        public Task<User> WhereIdIs(int id)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.Id == id)?.AsUser());
        }
        public Task<User> WithName(string name)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.Username.Equals(name, StringComparison.OrdinalIgnoreCase))?.AsUser());
        }
    }
}
