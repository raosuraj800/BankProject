using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    public class TravelContext
    {
        public ITravel travel;
        public TravelContext(ITravel travel)
        {
            this.travel = travel;
        }
        public void GoTOAirpot()
        {
            this.travel.GoToAirport();
        }
    }
    public interface ITravel
    {
        public void GoToAirport();
    }
    public class Auto : ITravel
    {
        public void GoToAirport()
        {
            Console.WriteLine("The cost of Travelling to Airport through Auto may cost around 800rs");
        }
    }
    public class Bus : ITravel
    {
        public void GoToAirport()
        {
            Console.WriteLine("The cost of Travelling to Airport through Bus may cost around 200rs");
        }
    }
    public class Train : ITravel
    {
        public void GoToAirport()
        {
            Console.WriteLine("The cost of Travelling to Airport through Train may cost around 300rs");
        }
    }
    public class Taxi : ITravel
    {
        public void GoToAirport()
        {
            Console.WriteLine("The cost of Travelling to Airport through Taxi may cost around 600rs");
        }
    }
}
