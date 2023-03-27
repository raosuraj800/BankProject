using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    public class Class1
    {
        public delegate void MyProperty(string Message);
        public delegate int CheckRes(int l,int m);
        public delegate void Result(int x, int y);
       // public delegate int Result(int x, int y);
        public  void DelegateMethod(string Message)
        {
            Console.WriteLine(Message);
        }

        public int DelegateMethodCalculate(int x,int y)
        {
            return x+y;
            
        }
        public void DelegateMethodCalculateVOid(int x, int y,MyProperty res)
        {
            res("The final string"+(x + y).ToString());

        }

        //public int Add(int x,int y)
        //{
        //    return x+y;
        //}
        //public int Sub(int x,int y)
        //{
        //    return x-y;
        //}
        public void Add(int x, int y)
        {
            Console.WriteLine(x + y);
        }
        public void Sub(int x, int y)
        {
            Console.WriteLine(x - y);
        }
        public void readNo(int x)
        {
            var z = new int[x];

            for (int i = 0; i < x; i++)
            {
                z[i] = i*3;
            }
            Console.WriteLine("The Array is [" + string.Join(", ",z)+"]");
        }
        public static int MethodA(int a,int b) //Func<string,string>
        {
            return a+b;
        }
        public static int MethodB(int a) //Func<string,string>
        {
            return a*340;
        }
        //public Func<int, int, int> asks { get; set; }
        public static void MethodB(int x, Func<int, int, int> ask)
        {
           
            Console.WriteLine("the Result is as follows"+x*ask(3,3));
        }

        public static Func<int,int> MethodA(int a) //Func<string,string>
        {

            return MethodB => a;
        }

        public static void MethodC(Action<int> action)
        {
            action(12);
        }
    }

}
