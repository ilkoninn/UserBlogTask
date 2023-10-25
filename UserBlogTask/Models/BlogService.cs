using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBlogTask.Exceptions;

namespace UserBlogTask.Models
{
    internal static class BlogService
    {
        public static void AddBlog(Blog blog)
        {
            BlogDatabase.Blogs.Add(blog);
        }
        public static void DeleteBlog(int Id)
        {
            try
            {
                Blog newBlog = BlogDatabase.Blogs.Find(x => x.Id == Id);
                if(newBlog != null)
                {
                    BlogDatabase.Blogs.Remove(newBlog);
                }
                else
                {
                    throw new BlogNotFoundException("There is no such a blog!");
                }
            }
            catch(BlogNotFoundException ex) 
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static Blog GetBlogById(int Id)
        {
            Blog newBlog = null;
            newBlog = BlogDatabase.Blogs.Find(x => x.Id == Id);

            return newBlog;
        }
        public static List<Blog> GetAllBlogs()
        {
            return BlogDatabase.Blogs;
        }
        public static List<Blog> GetBlogsByValue(string value)
        {
            return BlogDatabase.Blogs.FindAll(x => x.Title.Contains(value) || x.Description.Contains(value)).ToList();
        }
    }
}
