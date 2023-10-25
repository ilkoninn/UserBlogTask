using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using UserBlogTask.Enum;
using UserBlogTask.Exceptions;
using UserBlogTask.Models;

namespace UserBlogTask
{
    internal class Program
    {
        static User LoggedInUser = null;
        static void Main(string[] args)
        {
            Menu();
        }
        public static void Menu()
        {
            bool running = true;
            do
            {
                if (LoggedInUser == null)
                {
                    Console.WriteLine("\n\t<======== Menu ========>\n" +
                    "\n1. Register\n" +
                    "2. Login\n" +
                    "0. Exit\n");
                    Console.Write("User choice: ");
                    string userChoice = Console.ReadLine();
                    if (int.TryParse(userChoice, out int choice))
                    {
                        if (choice == 0) return;
                        FirstMenu(choice);
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid choice, try again!\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n\t<======== Blog Menu  ========>\n" +
                        $"\nUser: {LoggedInUser.Name} {LoggedInUser.Surname}\n" +
                        "1. Add blog\n" +
                        "2. Remove blog\n" +
                        "3. Blog detail\n" +
                        "4. Show all blogs\n" +
                        "5. Search by value\n" +
                        "6. Log out\n" +
                        "0. Exit\n");
                    Console.Write("User choice: ");
                    string userChoice = Console.ReadLine();
                    if (int.TryParse(userChoice, out int choice))
                    {
                        if (choice == 0) return;
                        SecondMenu(choice);
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid choice, try again!\n");
                    }
                }


            } while (running);
        }
        public static void FirstMenu(int choice)
        {
            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    try
                    {
                        Console.WriteLine("\n\tUser registration section\n");
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Regex regex1 = new Regex("^[a-zA-Z0-9_]*$");
                        if (name.Length < 3 || !regex1.IsMatch(name)) throw new InvalidNameException("\nName length must greater and equel than 3 and there is no space between them!");
                        Console.Write("Surname: ");
                        string surname = Console.ReadLine();
                        if (surname.Length < 3 || !regex1.IsMatch(surname)) throw new InvalidNameException("\nSurname length must greater and equel than 3 and there is no space between them!");
                        PATH1:
                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        Regex regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$");

                        if (!regex.IsMatch(password))
                        {
                            throw new InvalidPasswordException("\nPassword is not meet to criteria!, ex: Ilkin2004(1 upper, 1 lower, 1 digit)");
                        }
                        else
                        {
                            UserService.Registration(name, surname, password);
                            Console.WriteLine($"\nNew account successfully created!\nYour surname: {name.ToLower().Trim()}.{surname.ToLower().Trim()}\nYour password: {password}");
                        }
                    }
                    catch (InvalidNameException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (InvalidSurNameException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (InvalidPasswordException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    break;
                case 2:
                    Console.WriteLine("\n\tUser login section\n");
                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Password: ");
                    string password2 = Console.ReadLine();

                    LoggedInUser = UserService.Login(username, password2);
                    if (LoggedInUser != null) Console.WriteLine("\nYou are successfully logged in!");
                    else Console.WriteLine("\nYou are not logged in!");
                    break;
                default:
                    Console.WriteLine("\nInvalid choice, try again!\n");
                    break;
            }
        }
        public static void SecondMenu(int choice)
        {
            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    Console.WriteLine("\n\tAdd blog section\n");
                    Console.Write("Blog title: ");
                    string blogTitle = Console.ReadLine();
                    Console.Write("Blog description: ");
                    string blogDesc = Console.ReadLine();
                PATH2:
                    Console.WriteLine("Blog type:\n");
                    Console.WriteLine("1. Programming");
                    Console.WriteLine("2. Educational");
                    Console.WriteLine("3. Thriller");
                    Console.Write("User choice: ");
                    string blogChoice = Console.ReadLine();
                    if (BlogEnum.TryParse(blogChoice, out BlogEnum blogEnum))
                    {
                        Blog blog = new Blog(blogTitle, blogDesc, blogEnum);
                        BlogService.AddBlog(blog);
                        Console.WriteLine($"New blog added!, Blog ID: {blog.Id}");
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid choice, try again!\n");
                        goto PATH2;
                    }
                    break;
                case 2:
                    try
                    {
                        Console.WriteLine("\n\tRemove blog section\n");
                        Console.Write("Blog ID: ");
                        string blogID = Console.ReadLine();
                        if (int.TryParse(blogID, out int ID))
                        {
                            BlogService.DeleteBlog(ID);
                        }
                        else
                        {
                            throw new BlogNotFoundException("\nThere is no such a blog!");
                        }
                    }
                    catch (BlogNotFoundException ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    Console.WriteLine("\n\tGet blog by ID section\n");
                    Console.Write("Blog ID: ");
                    string blogID2 = Console.ReadLine();
                    if (int.TryParse(blogID2, out int ID2))
                    {
                        try
                        {
                            if (BlogService.GetBlogById(ID2) == null) throw new BlogNotFoundException("There is no such a blog!");
                            Console.WriteLine($"\nTitle: {BlogService.GetBlogById(ID2).Title}");
                            Console.WriteLine($"Description: {BlogService.GetBlogById(ID2).Description}");
                            Console.WriteLine($"Type: {BlogService.GetBlogById(ID2).BlogEnum}");
                        }
                        catch (BlogNotFoundException ex)
                        {
                            Console.WriteLine();
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine();
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid choice, try again!\n");
                    }

                    break;
                case 4:
                    Console.WriteLine("\n\tGet all blogs section\n");
                    foreach (var item in BlogService.GetAllBlogs())
                    {
                        Console.WriteLine();
                        Console.WriteLine($"ID: {item.Id}\nTitle: {item.Title}\nDescription: {item.Description}");
                        Console.WriteLine($"Type: {item.BlogEnum}");
                    }
                    break;
                case 5:
                    Console.WriteLine("\n\tGet blog by search value section\n");
                    Console.Write("Search value: ");
                    string value = Console.ReadLine();

                    foreach (var item in BlogService.GetBlogsByValue(value))
                    {
                        Console.WriteLine();
                        Console.WriteLine($"ID: {item.Id}\nTitle: {item.Title}\nDescription: {item.Description}");
                        Console.WriteLine($"Type: {item.BlogEnum}");
                    }
                    break;
                case 6:
                    LoggedInUser = null;
                    break;
                default:
                    Console.WriteLine("\nInvalid choice, try again!\n");
                    break;
            }
        }
    }
}