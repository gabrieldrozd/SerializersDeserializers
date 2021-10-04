using SerialDeserial.Models;
using System;
using static SerialDeserial.Serializers;

namespace SerialDeserial
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlPath = $"C:\\Users\\drozd\\Desktop\\xmlFile.xml";
            string jsonPath = $"C:\\Users\\drozd\\Desktop\\jsonFile.json";

            var book = new Book()
            {
                Id = 1,
                Isbn = "564413874312",
                Title = "The Witcher",
                DatePublished = DateTime.Today
            };

            JsonSerializerClass<Book>.DoJsonSerialization(book, jsonPath);
            var returnedObjectJson = JsonSerializerClass<Book>.DoJsonDeserialization(typeof(Book), jsonPath);
            Console.WriteLine($"{returnedObjectJson.Isbn} - {returnedObjectJson.Title}");

            XmlSerializerClass<Book>.DoXmlSerialization(typeof(Book), book, xmlPath);
            var returnedObjectXml = XmlSerializerClass<Book>.DoXmlDeserialization(typeof(Book), xmlPath);
            Console.WriteLine($"{returnedObjectXml.Isbn} - {returnedObjectXml.Title}");

            Console.ReadKey();
        }
    }
}
