using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace Check
{
    public class Class1
    {
        private static readonly List<int> xsz = new List<int>();
        static Class1()
        {
            Console.WriteLine("Hi! Im static");
        }
        public void method1()
        {
            xsz.Add(1);
            Console.WriteLine(xsz[0]);
        }
    }
    class Class2 : Class1
    {
    }
}
