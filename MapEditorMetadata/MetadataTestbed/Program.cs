using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoidenMetadata;
using VoidenMetadata.Parsing.XML;

namespace MetadataTestbed
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = new int[] { 10, 2, 4 };

            int index = Array.IndexOf(ints, 5);

            XMLMetadataLoader l = new XMLMetadataLoader();
            
            MetadataObjectGroup g =  l.Load(@"C:\Users\VoidBab\Desktop\Map editor\Metadata\Object.xml");

            MetadataObject o = g.GetObject("wrapper");
            Console.WriteLine(o.GetFieldValue<bool>("bool"));
            Console.WriteLine(o.GetFieldValue<float>("float"));
            Console.WriteLine(o.GetFieldValue<int>("int"));
            Console.WriteLine(o.GetFieldValue<byte>("byte"));
            Console.WriteLine(o.GetFieldValue<string>("string"));
            o.SetFieldValue("bool", false);
        }
    }
}
