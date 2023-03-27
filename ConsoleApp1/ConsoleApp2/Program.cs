using AdapterPattern;

class Program
{
    public static void Main(String[] args)
    {
        IPeopleList person = new OverridePeope();
        var party = new ThirdPArty(person);
        party.ShowPeopleList();
    }

}