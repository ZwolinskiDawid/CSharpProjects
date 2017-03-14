using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lab3
{
    [XmlRoot("cars")]
    public class Cars : List<Car>
    {
        public Cars()
        {

        }
    }

    [XmlType("car")]
    public class Car
    {
        public string model { get; set; }

        [XmlElement("engine")]
        public Engine engine { get; set; }

        public int year { get; set; }

        public Car(string model, Engine engine, int year)
        {
            this.model = model;
            this.engine = engine;
            this.year = year;
        }

        public Car()
        {

        }
    }

    public class Engine
    {
        public double displacement { get; set; }
        public double horsePower { get; set; }

        [XmlAttribute]
        public string model { get; set; }

        public Engine(double displacement, double horsePower, string model)
        {
            this.displacement = displacement;
            this.horsePower = horsePower;
            this.model = model;
        }

        public Engine()
        {

        }
    }
}
