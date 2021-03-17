using ProductWebApi.Models;
using System;
using System.Linq;

namespace ProductWebApi.Authentication
{
    public class UserHelper
    {
        private  static IProductService _service;
        public UserHelper(IProductService service)
        {
            _service = service;
        }
        public static bool Validate(string username, string password)
        {
            var users = _service.GetUsers();

            return users.Any(user =>
                        string.Compare(user.username, username, StringComparison.OrdinalIgnoreCase) == 0
                        && user.password == password);
        }
    }
}