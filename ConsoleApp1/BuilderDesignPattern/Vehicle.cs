using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderDesignPattern
{
    public class Vehicle
    {
        public string Model { get; set; }
        public string Engine { get; set; }
        public string Transmission { get; set; }
        public string Body { get; set; }
        public List<string> Accessories { get; set; }
        public Vehicle()
        {
            Accessories = new List<string>();
        }
        public void Show()
        {
            Console.WriteLine("Model = " + Model);
            Console.WriteLine("Engine = " + Engine);
            Console.WriteLine("Transmission = " + Transmission);
            Console.WriteLine("Body = " + Body);
            Console.WriteLine("Accesories =");
            foreach (var item in Accessories)
            {
                Console.WriteLine("\t" + item);
            }

        }
    }
    public interface IVehicleBuilder
    {
        void SetModel();
        void SetTransmission();
        void SetEngine();
        void SetBody();
        void SetAccessories();
        Vehicle GetVehicles();
    }
    public class HeroBuilder : IVehicleBuilder
    {
        Vehicle vehicle = new Vehicle();
        public Vehicle GetVehicles()
        {
            return vehicle;
        }
        public void SetAccessories()
        {
            vehicle.Accessories.Add("Helmet");
            vehicle.Accessories.Add("Rear Glass");
            vehicle.Accessories.Add("Phantom");
        }
        public void SetBody()
        {
            vehicle.Body = "Plastic";
        }
        public void SetEngine()
        {
            vehicle.Engine = "5 Speed";
        }
        public void SetModel()
        {
            vehicle.Model = "CBR";
        }
        public void SetTransmission()
        {
            vehicle.Transmission = "180 KMPH";
        }
    }
    public class BajajBuilder : IVehicleBuilder
    {
        Vehicle vehicle = new Vehicle();
        public Vehicle GetVehicles()
        {
            return vehicle;
        }
        public void SetAccessories()
        {
            vehicle.Accessories.Add("Helmet");
            vehicle.Accessories.Add("Rear Glass");
            vehicle.Accessories.Add("Brakes");
        }
        public void SetBody()
        {
            vehicle.Body = "Fiber";
        }
        public void SetEngine()
        {
            vehicle.Engine = "6 Speed";
        }
        public void SetModel()
        {
            vehicle.Model = "Pulsar 220";
        }
        public void SetTransmission()
        {
            vehicle.Transmission = "220 KMPH";
        }
    }
    public class Director
    {
        private readonly IVehicleBuilder vehile;
        public Director(IVehicleBuilder vehile)
        {
            this.vehile = vehile;
        }
        public void CreateVehicle()
        {
            this.vehile.SetAccessories();
            this.vehile.SetModel();
            this.vehile.SetEngine();
            this.vehile.SetTransmission();
            this.vehile.SetBody();
        }
        public Vehicle GetVehicle()
        {
            return this.vehile.GetVehicles();
        }
    }
}
