using ProductWebApi.Models;
using System;
using System.Linq;

namespace ProductWebApi.Authentication
{
    public class UserHelper
    {
        public static bool Validate(string username, string password)
        {
            using (ProductDbEntities entities = new ProductDbEntities())
            {
                return entities.users.Any(user =>
                        string.Compare(user.username, username, StringComparison.OrdinalIgnoreCase) == 0
                        && user.password == password);
            }
        }
    }
}