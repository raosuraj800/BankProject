using GenericsExample;

if (CheckTwoValues.CheckIfEquals("Ra0", "Ra0"))
{
    Console.WriteLine("Equals");
}
else
{
    Console.WriteLine("not equals");
}
var cc = new CheckClassType<double>(2.343);
var x = cc.GenerateMethod("ewe");
Console.WriteLine(x);
//(!CheckTwoValues.CheckIfEquals(3, 4) ? Console.WriteLine("not equals") : Console.WriteLine("Equal"));