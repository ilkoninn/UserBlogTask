using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBlogTask.Enum;

namespace UserBlogTask.Models
{
    internal class Blog
    {
        internal int Id { get; set; }
        static int _id;
        internal string Title { get; set; }
        internal string Description { get; set; }
        internal BlogEnum BlogEnum { get; set; }

        public Blog(string title, string description, BlogEnum blogEnum)
        {
            Id = ++_id;
            Title = title;
            Description = description;
            BlogEnum = blogEnum;
        }
    }
}
