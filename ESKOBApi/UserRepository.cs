using ESKOBApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ESKOBApi.Controllers
{
    public class UserRepository
    {


        public Manager ValidateUser(string userName, string password)
        {
            using (var _context = new ESKOBDbContext())
            {
                return _context.Managers.FirstOrDefault(manager =>
                manager.Name.Equals(userName, StringComparison.OrdinalIgnoreCase)
                && manager.Password == password);
            }
        }
    
        public void Dispose()
        {
            using (var _context = new ESKOBDbContext())
            {
                _context.Dispose();
            }
        }
    }
}
