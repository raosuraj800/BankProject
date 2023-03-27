using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class Client
    {
    }
    public abstract class Animal
    {
        public abstract string Speak();

    }
    public class Dog : Animal
    {
        public override string Speak()
        {
            return "Boww";
        }
    }
    public class Cat : Animal
    {
        public override string Speak()
        {
            return "Meoww";
        }
    }
    public class Shark : Animal
    {
        public override string Speak()
        {
            return "www";
        }
    }
    public class Octopus : Animal
    {
        public override string Speak()
        {
            return "Oct";
        }
    }
    public abstract class AnimalFactory
    {
        public abstract Animal GetAnimal(string AnimalType);
        public static AnimalFactory getFactory(string FactoryType)
        {
            if (FactoryType == "Sea")
            {
                return new SeaFactory();
            }
            else
            {
                return new LandFactory();
            }
        }
    }
    public class LandFactory : AnimalFactory
    {
        public override Animal GetAnimal(string AnimalType)
        {
            if (AnimalType.Equals("Dog")) { return new Dog(); }
            else return new Cat();
        }
    }
    public class SeaFactory : AnimalFactory
    {
        public override Animal GetAnimal(string AnimalType)
        {
            if (AnimalType.Equals("Shark")) { return new Shark(); }
            else return new Octopus();
        }
    }

}