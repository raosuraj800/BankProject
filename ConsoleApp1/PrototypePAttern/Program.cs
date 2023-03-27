using PrototypePAttern;

Director dir = new Director();
//Address ad = new Address();
//ad.AddressName = "Mysore";
dir.Name = "Suraj";
dir.Result = "Aggregate";
dir.AddressName = new Address() { AddressName="Mysore"};
//dir.AddressName.AddressName = "Mysore";
var dir2 = (Director)dir.Clone();
dir2.Name = "Rao";
//dir2.Result = "Average";
dir2.AddressName.AddressName = "Bangalore";

Console.WriteLine(dir.GetDetails());
Console.WriteLine(dir2.GetDetails());
Typist type = new Typist();
type.Name = "Ankur";
type.Result = "Aggregate";
type.AddressName = new Address();
type.AddressName = new Address() { AddressName = "Mysore" };
var type2 = (Typist)type.Clone();
type2.Name = "Mathews";
//dir2.Result = "Average";
type2.AddressName.AddressName = "Rayalseema";

Console.WriteLine(type.GetDetails());
Console.WriteLine(type2.GetDetails());