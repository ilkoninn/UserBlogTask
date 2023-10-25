using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBlogTask.Exceptions
{
    internal class InvalidPasswordException:Exception
    {
        public InvalidPasswordException(string message):base(message)
        {

        }
    }
}
