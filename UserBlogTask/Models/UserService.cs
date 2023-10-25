using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBlogTask.Models
{
    internal static class UserService
    {
        public static void Registration(string name, string surname, string password)
        {
            User user = new User(name, surname, password);
            BlogDatabase.Users.Add(user);
        }
        public static User Login(string username, string password)
        {
            User user = BlogDatabase.Users.Find(user => user.Username == username && user.Password == password);
            return user;
        }
    }
}
