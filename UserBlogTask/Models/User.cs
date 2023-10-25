using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBlogTask.Models
{
    internal class User
    {
        internal int Id { get; set; }
        static int _id;
        public string Name { get; set; }
        internal string Surname { get; set; }
        internal string Username { get; set; }
        internal string Password { get; set; }

        public User(string name, string surname, string password)
        {
            Id = ++_id;
            Name = name;
            Surname = surname;
            Username = $"{name.ToLower().Trim()}.{surname.ToLower().Trim()}";
            Password = password;
        }
    }
}
