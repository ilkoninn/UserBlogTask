using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBlogTask.Exceptions
{
    internal class InvalidSurNameException:Exception
    {
        public InvalidSurNameException(string message):base(message) { }
    }
}
