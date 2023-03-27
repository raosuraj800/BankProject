using FlyWeightPattern;

ShapeDecider sof = new ShapeDecider();

IShape shape = sof.GetShape("Rectangle");
shape.Print();
 shape = sof.GetShape("Rectangle");
shape.Print();
 shape = sof.GetShape("Rectangle");
shape.Print();
shape = sof.GetShape("Circle");
shape.Print();
shape = sof.GetShape("Circle");
shape.Print();
Console.WriteLine("the number of objects created are "+ sof.TotalObjectsCreated);
