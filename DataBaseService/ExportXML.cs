using DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataBaseService
{
    public class ExportXML
    {
     
        public void exportDbToXml<T>(ICollection<T> CollectionT)
        {
            //var dir = Environment.GetFolderPath(Environment.SpecialFolder.System))
            //var fileName = Path.Combine(dir, "DBExport.xml");
            var path = @"C:\Users\User\Music";
            var file = "DBExport.xml";
            var fileName = Path.Combine(path, file);
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (TextWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                {
                   
                    XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                    serializer.Serialize(sw, CollectionT);
                }
            }
        }
    }
}
