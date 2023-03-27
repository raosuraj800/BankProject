using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsExample
{
    public static class CheckTwoValues
    {
        public static bool CheckIfEquals<T>(T Param1,T Param2) {
            return Param1.Equals(Param2);
        }
    }

    public class CheckClassType<T>
    {
        public T MyProperty { get; set; }
        public CheckClassType(T myProperty)
        {
            MyProperty = myProperty;
        }
        public T GenerateMethod<T>(T param)
        {
            Console.WriteLine("the Type of Property is "+MyProperty.GetType());
            Console.WriteLine("the Type of Property is " + param.GetType());
            return param;
        }
    }
}
