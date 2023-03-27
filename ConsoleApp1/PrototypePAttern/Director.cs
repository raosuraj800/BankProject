using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePAttern
{
    public class Director : IEmployee
    {
        public string Name { get; set; }
        public string Result { get; set; }
        public Address AddressName { get; set; }
        //Address ad = new Address();
        
        public IEmployee Clone()
        {
           return (IEmployee)this.MemberwiseClone();
        }

        public string GetDetails()
        {
            return "The "+this.GetType().Name+ "'s Name is "+Name+" and the result is as follows "+Result+" and the address is "+AddressName.AddressName.ToString();
        }
    }
    public interface IEmployee
    {
        IEmployee Clone();
        string GetDetails();
    }
    public class Address
    {
        public string AddressName{ get; set; }
        public Address Clone()
        {
            return (Address)this.MemberwiseClone(); 
        }
    }
    public class Typist : IEmployee
    {
        public string Name { get; set; }
        public Address AddressName { get; set; }
        public string Result { get; set; }
        public IEmployee Clone()
        {
            Typist emp = (Typist)this.MemberwiseClone();
            emp.AddressName = AddressName.Clone();
            return emp;
        }

        public string GetDetails()
        {
            return "The "+this.GetType().Name+"'s Name is "+Name+" and the result is "+Result+" and the address is "+AddressName.AddressName.ToString();
        }
    }

}
