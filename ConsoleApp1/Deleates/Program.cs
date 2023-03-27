// See https://aka.ms/new-console-template for more information

using Delegates;
using Microsoft.AspNetCore.Http;
using static Delegates.Class1;

internal class Program
{
    private static void Main(string[] args)
    {
        Class1 ccc = new();
        MyProperty handle = ccc.DelegateMethod;
        handle("hello");
        CheckRes check = ccc.DelegateMethodCalculate;
        Console.WriteLine("the result is "+check(1,2));
        ccc.DelegateMethodCalculateVOid(1, 5, handle);
        Result res = ccc.Add;
        //Console.WriteLine("Add Val = " + res(1, 2));
        res += ccc.Sub;
        //  Console.WriteLine("Add Val = " + res(1, 2));
        res(11, 12);
        res -= ccc.Add;
        res -= ccc.Sub;
        CookieOptions s= new CookieOptions();
        s.Expires = DateTime.Now.AddMinutes(10);
        MethodB(3,MethodA);
        MethodC(ccc.readNo);
        Console.WriteLine("The Func as return type and the result is "+MethodA(9).ToString());
        Console.ReadLine();
        
       
        

       // res(11, 9);
        //Console.WriteLine("the string result is as follows "+handle());
    }
}