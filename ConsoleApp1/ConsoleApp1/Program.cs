using ConsoleApp1;

class Program
{
   public static void Main(String[] args)
    {
        Person person= new CheckPerson();
        IPerson musy = person.GiveString("Msore");
        Console.WriteLine(musy.GetNumberOfPerson(10));
        IPerson ngl = person.GiveString("bgl");
        Console.WriteLine(ngl.GetNumberOfPerson(20));
    }

}