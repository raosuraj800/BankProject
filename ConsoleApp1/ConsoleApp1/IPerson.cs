using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IPerson
    {
        public int GetNumberOfPerson(int x);
    }
    public class InMsore : IPerson
    {
        public int GetNumberOfPerson(int x)
        {
            return x;
        }
    }
    public class InBglr : IPerson
    {
        public int GetNumberOfPerson(int x)
        {
            return x;
        }
    }
    public abstract class Person
    {
        public abstract IPerson GiveString(string Type);
    }
    public class CheckPerson : Person
    {
            public override IPerson GiveString(string type)
        {
            if (type == "Msore")
                return new InMsore();
            else
                return new InBglr();
        }
    }
}
