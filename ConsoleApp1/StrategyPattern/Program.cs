using StrategyPattern;

TravelContext tc = null;
Console.WriteLine("Please enter the means of transportation - Bus, Train, Taxi or Auto!!!");
string TravelType  = Console.ReadLine();
Console.WriteLine("You Choosed to Travel By " + TravelType);
if("BUS".Equals(TravelType,StringComparison.InvariantCultureIgnoreCase))
    tc = new TravelContext(new Bus());
else if("Train".Equals(TravelType,StringComparison.InvariantCultureIgnoreCase))
    tc = new TravelContext(new Train());
else if ("Auto".Equals(TravelType, StringComparison.InvariantCultureIgnoreCase))
    tc = new TravelContext(new Auto());
else  
    tc = new TravelContext(new Taxi());
tc.GoTOAirpot();
Console.ReadLine();
