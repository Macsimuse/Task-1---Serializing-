using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace SerializingDocument
{
    class DataManipulation
    {
        // 1. File txt to File xml
        public void Serialized_TxtInXml(string _textFile, string _xmlFile)
        {
            List<string> list = new List<string>();

            if (_textFile.Contains(".txt") && _xmlFile.Contains(".xml"))
            {
                try
                {
                    using (StreamReader fileReader = new StreamReader(_textFile))
                    {
                        while (fileReader.ReadLine() != null)
                        {
                            list.Add(fileReader.ReadToEnd());
                        }
                    }
                    using (StreamWriter file = new StreamWriter(_xmlFile))
                    {
                        XmlSerializer xmlSerialized = new XmlSerializer(typeof(List<string>));
                        xmlSerialized.Serialize(file, list);
                    }
                }
                catch (FileNotFoundException ex) { Console.WriteLine(ex.Message); }
            }
            else Console.WriteLine("Wrong type of file");
        }

        // 2. Object to File xml
        public void Serialized_Object(string _xmlFile, object _objectForSerializing)
        {
            if (_xmlFile.Contains(".xml"))
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(_xmlFile))
                    {
                        XmlSerializer xmlSerialized = new XmlSerializer(_objectForSerializing.GetType());
                        xmlSerialized.Serialize(file, _objectForSerializing);
                    }
                }
                catch (FileNotFoundException ex) { Console.WriteLine(ex.Message); }
            }
            else Console.WriteLine("Wrong type of file");
        }
    }

    #region Object
    public enum Gender{Male,Female}
    [Serializable]
    public class Staff
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Gender gender { get; set; }
        public Staff() { }
    }
    #endregion

    class Program
    {
        #region Data
        static List<Staff> GetListOfStaff()
        {
            List<Staff> listOFStaff = new List<Staff>()
            {
              new Staff(){ Name = "Max",LastName = "Kucheruk", Age = 24, gender = Gender.Male},
              new Staff(){Name = "Yuri",LastName = "Kovalchjuk", Age = 29, gender = Gender.Male},
              new Staff(){Name = "Olga",LastName = "Bartosh", Age = 25, gender = Gender.Female},
              new Staff(){Name = "Sergei",LastName = "Saharov", Age = 39, gender = Gender.Male},
              new Staff(){Name = "Nastja",LastName = "Koval", Age = 21, gender = Gender.Female},
            };
            return listOFStaff;
        }
        #endregion

        static void Main(string[] args)
        {

            string _textFilePath = "../../TextContainer.txt";
            string _xmlFilePath = "../../XMLContainer.xml";
            DataManipulation dataManipulation = new DataManipulation();

      //1.   Invoke File to File
            dataManipulation.Serialized_TxtInXml(_textFilePath,_xmlFilePath);
            
      // 2.  Invoke Object to File
            dataManipulation.Serialized_Object(_xmlFilePath, GetListOfStaff());
            Console.ReadKey();
        }
    }
}
