using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo(args[0]);

            Child Tree = new Child((FileSystemInfo)di);

            Console.WriteLine(Tree);
            Console.WriteLine("\nThe oldest date: " + di.getTheOldestDate());

            //====================SERIALIZE===========================

            Serialize("file.txt", Tree.childs);

            //===================DESERIALIZE==========================

            SortedList<Child, int> childs = Deserialize("file.txt");

            foreach(KeyValuePair<Child, int> kvp in childs)
            {
                Console.Write("\n" + kvp.Key.Name + " -> " + kvp.Value);
            }

            Console.ReadKey();
        }

        private static void Serialize(string path, SortedList<Child, int> childs)
        {
            FileStream fs = new FileStream(path, FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, childs);
            }
            catch(SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            }
            finally
            {
                fs.Close();
            }
        }

        private static SortedList<Child, int> Deserialize(string path)
        {
            SortedList<Child, int> childs = null;

            FileStream fs = new FileStream(path, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();
            childs = (SortedList<Child, int>)formatter.Deserialize(fs);

            fs.Close();

            return childs;
        }
    }
}
