using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Tests
{
    public class ClassForTests
    {
        public int Id = 19;
        public string Name = "ClassName";

        public string Password => "ClassPassword";

        public DateTime Date => DateTime.Now;
    }
}
