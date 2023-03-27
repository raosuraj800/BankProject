using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyWeightPattern
{
    public class Rectangle : IShape
    {
        public void Print()
        {
            Console.WriteLine("Printing Rectangle");
        }
    }
    public class Circle : IShape
    {
        public void Print()
        {
            Console.WriteLine("Printing Circle");
        }
    }
    public interface IShape
    {
        public void Print();
    }
    public class ShapeDecider
    {
        Dictionary<string, IShape> shapes = new Dictionary<string, IShape>();
        public int TotalObjectsCreated
        {
            get { return shapes.Count; }
        }
        public IShape GetShape(string ShapeTYpe) {
            IShape shape = null;
            if(shapes.ContainsKey(ShapeTYpe)) { 
            shape = shapes[ShapeTYpe];
            }
            else
            {
                switch(ShapeTYpe)
                {
                    case "Rectangle":
                        shape = new Rectangle();
                        shapes.Add(ShapeTYpe, shape);
                        break;
                    case "Circle":
                        shape = new Circle();
                        shapes.Add(ShapeTYpe, shape);
                        break;
                    default:throw new Exception("no more objects can be created");
                }
            }
            return shape;
        }
    }
}
