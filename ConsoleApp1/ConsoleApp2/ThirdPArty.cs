using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    internal class ThirdPArty
    {
        public IPeopleList PPlList;
        public ThirdPArty(IPeopleList PPlList)
        {
            this.PPlList = PPlList;
        }
        public void ShowPeopleList()
        {
            var list =this.PPlList.GetPeoplesList();
            Console.WriteLine("###########The Peoples List###############");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
    public interface IPeopleList
    {
        List<string> GetPeoplesList();
    }
    public class People
    {
        public string[][] GetPeople()
        {
            string[][] people = new string[][]
            {
                new string[] {"1","Alexander","Male"},
                new string[] {"2","David","Male"},
                new string[] {"3","Jessy","Female"},
                new string[] {"4","Maddison","Female"}
            };
            return people;
        }

    }
    public class OverridePeope : People, IPeopleList
    {
        public List<string> GetPeoplesList()
        {
            List<string> list = new();
            var people = GetPeople();
            foreach (var item in people)
            {
                list.Add(item[1] + "'s Gender, Id is " + item[2] + " and " + item[0] + " respectively");
            }
            return list;
        }
    }
}
