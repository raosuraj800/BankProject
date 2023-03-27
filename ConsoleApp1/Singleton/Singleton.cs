using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    internal class Singleton
    {
        private static Singleton? Instance = null;
        private string FirstNameAny;
        public string Anything;
        private Singleton()
        {
            Console.WriteLine("##########Instance##########");
            FirstNameAny = "ColdPlay";
            Anything = "Your Under Rain!!!!!!!";
        }
        public static object NewObject = new();
        public static Singleton InstanceCreation
        {
            get
            {
                lock (NewObject)
                {
                    if (Instance == null)
                    {
                        Instance = new Singleton(); 
                    }
                }
                return Instance;
            }

        }
        public void Show()
        {
            Console.WriteLine("The Name and Song is {0} and {1}",FirstNameAny,Anything);
        }
    }
}
