using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Cars myCars = new Cars()
            {
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
            };            

            //=============ZAD1=================

            var v1 = myCars
                .Where(car => car.model == "A6")
                .Select(car => new
                {
                    engineType = (car.engine.model == "TDI") ? "petrol" : "diesel",
                    hppl = car.engine.horsePower / car.engine.displacement
                });

            var v2 = v1
                .GroupBy(engine => engine.engineType)
                .Select(engine => new
                {
                    engineType = engine.Key,
                    hppl = engine.Average(e => e.hppl)
                });

            foreach(var res in v2)
            {
                Console.WriteLine(res.engineType + ": " + res.hppl);
            }

            //=============ZAD 2==========================

            XmlSerializer x = new XmlSerializer(typeof(Cars));
            TextWriter writer = new StreamWriter(@"..\..\CarsCollection.xml");
            x.Serialize(writer, myCars);
            writer.Close();

            //============ZAD 3===================

            XElement rootNode = XElement.Load(@"..\..\CarsCollection.xml");
            double avgHP = (double)rootNode.XPathEvaluate(
                "sum(car/engine[@model != \"TDI\"]/horsePower)" +
                "div count(car/engine[@model != \"TDI\"])"
                );

            Console.WriteLine("\navgHP = " + avgHP);

            IEnumerable<XElement> models = rootNode.XPathSelectElements("Car[not(model = following::Car/model)]/model");

            //===========ZAD 4=======================

            IEnumerable<XElement> nodes = from car in myCars
                                          select new XElement("car",
                                                        new XElement("model", car.model),
                                                        new XElement("year", car.year),
                                                        new XElement("engine",
                                                            new XAttribute("model", car.engine.model),
                                                            new XElement("displacement", car.engine.displacement),
                                                            new XElement("horsePower", car.engine.horsePower)
                                                        )
                                                     );
            XElement root = new XElement("cars", nodes);
            rootNode.Save(@"..\..\CarsFromLinq.xml");

            //=============ZAD 5======================

            XDocument xmlFile = XDocument.Load(@"..\..\template.html");
            var body = xmlFile.Root.LastNode as XElement;

            IEnumerable<XElement> tableContent = from car in myCars
                                                 select new XElement("tr",
                                                           new XElement("td", car.model),
                                                           new XElement("td", car.engine.model),
                                                           new XElement("td", car.engine.displacement),
                                                           new XElement("td", car.engine.horsePower),
                                                           new XElement("td", car.year)
                                                       );
            body.Add(new XElement("table", new XAttribute("border", 1), tableContent));
            xmlFile.Save(@"..\..\CarsHtml.html");

            //===================ZAD 6=======================

            XDocument xmlFile6 = XDocument.Load(@"..\..\CarsCollection.xml");
            var query = from c in xmlFile6.Element("cars").Elements("car").Elements("engine").Elements("horsePower")
                        select c;
            foreach (XElement hp in query)
            {
                hp.Name = "hp";
            }

            var query2 = from c in xmlFile6.Element("cars").Elements("car")
                         select c;

            foreach (XElement car in query2)
            {
                car.Element("model").Add(new XAttribute("year", car.Element("year").Value));
                car.Element("year").Remove();
            }

            xmlFile6.Save(@"..\..\CarsZad6.xml");

            Console.Read();
        }
    }
}
