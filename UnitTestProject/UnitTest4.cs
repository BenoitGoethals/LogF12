using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace UnitTestProject
{
    public class UnitTest4
    {
        [Fact]
        public void TestXml()
        {
            if (!File.Exists(@"C:\temp\Something.xml"))
            {
                XNamespace empNm = "urn:lst-emp:emp";

                XDocument xDoc = new XDocument(
                            new XDeclaration("1.0", "UTF-16", null),
                            new XElement(empNm + "Employees",
                                new XElement("Employee",
                                    
                                    new XElement("EmpId", "5"),
                                    new XElement("Name", "Kimmy"),
                                    new XElement("Sex", "Female")
                                    )));

                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                xDoc.Save(xWrite);
                xWrite.Close();
                xDoc.Save(@"C:\temp\Something.xml");
                Console.WriteLine("Saved");
            }
            else
            {
                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                // Save to Disk
               


                XElement xEle = XElement.Load(@"C:\temp\Something.xml");
                xEle.Add(new XElement("Employee",
                 
                                    new XElement("EmpId", "6"),
                                    new XElement("Name", "jkjhk"),
                                    new XElement("Sex", "Female")
                                    ));

                Console.Write(xEle);

                xEle.Save(xWrite);
                xWrite.Close();
                xEle.Save(@"C:\temp\Something.xml");



            }
        }
    }
}
