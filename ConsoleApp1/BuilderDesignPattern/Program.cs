using BuilderDesignPattern;

var director = new Director(new HeroBuilder());
director.CreateVehicle();
director.GetVehicle().Show();
Console.WriteLine("-------------------------------------------------------");
var directors = new Director(new BajajBuilder());
directors.CreateVehicle();
directors.GetVehicle().Show();
