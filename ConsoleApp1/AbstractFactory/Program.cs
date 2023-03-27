using AbstractFactory;

Animal animal = null;
AnimalFactory factory = null;
string Speaks = null;

factory = AnimalFactory.getFactory("Sea");
Console.WriteLine("the life is "+factory.GetType().Name);

animal = factory.GetAnimal("Shark");
Speaks = animal.Speak();
Console.WriteLine(animal.GetType().Name+" speaks in "+Speaks);
Console.WriteLine();
animal = factory.GetAnimal("Octopus");
Console.WriteLine("the life is " + factory.GetType().Name);
Speaks = animal.Speak();
Console.WriteLine(animal.GetType().Name + " speaks in " + Speaks);

Console.WriteLine();
Console.WriteLine("###########################################################");
Console.WriteLine();

factory = AnimalFactory.getFactory("Land");
Console.WriteLine("the life is " + factory.GetType().Name);

animal = factory.GetAnimal("Dog");
Speaks = animal.Speak();
Console.WriteLine(animal.GetType().Name + " speaks in " + Speaks);
Console.WriteLine();
animal = factory.GetAnimal("Cat");
Console.WriteLine("the life is " + factory.GetType().Name);
Speaks = animal.Speak();
Console.WriteLine(animal.GetType().Name + " speaks in " + Speaks);