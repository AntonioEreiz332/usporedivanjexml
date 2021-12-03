using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace usporedivanjeXml
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = @"<Books>
                 <book id='20504' image='C01' name='C# in Depth'/>
                 <book id='20505' image='C02' name='ASP.NET'/>
                 <book id='20506' image='C03' name='LINQ in Action '/>
                 <book id='20507' image='C04' name='Architecting Applications'/>
                </Books>";
            string s2 = @"<Books>
                  <book id='20504' image='C011' name='C# in Depth'/>
                  <book id='20505' image='C02' name='ASP.NET 2.0'/>
                  <book id='20506' image='C03' name='LINQ in Action '/>
                  <book id='20508' image='C04' name='Architecting Applications'/>
                </Books>";

            XDocument xml1 = XDocument.Parse(s1);
            XDocument xml2 = XDocument.Parse(s2);

            
            var result1 = from xmlBooks1 in xml1.Descendants("book")
                          from xmlBooks2 in xml2.Descendants("book")
                          select new
                          {
                              book1 = new
                              {
                                  id = xmlBooks1.Attribute("id").Value,
                                  image = xmlBooks1.Attribute("image").Value,
                                  name = xmlBooks1.Attribute("name").Value
                              },
                              book2 = new
                              {
                                  id = xmlBooks2.Attribute("id").Value,
                                  image = xmlBooks2.Attribute("image").Value,
                                  name = xmlBooks2.Attribute("name").Value
                              }
                          };

            
            var result2 = from i in result1
                          where (i.book1.id == i.book2.id
                                 || i.book1.image == i.book2.image
                                 || i.book1.name == i.book2.name) &&
                                 !(i.book1.id == i.book2.id
                                 && i.book1.image == i.book2.image
                                 && i.book1.name == i.book2.name)
                          select i;

            int br = 1;
            Console.WriteLine("Issued" + "        " + "Issue type" + "        " + "IssueInFirst" + "        " + "IssueInSecond" + "        " + "\n");
            foreach (var aa in result2)
            {
                
                if (aa.book1.image!=aa.book2.image)
                {
                    Console.WriteLine(br + "        " + "image is different" + "      " + aa.book1.image + "                " + aa.book2.image + "      ");
                    br++;
                }
                if (aa.book1.name != aa.book2.name)
                {
                    Console.WriteLine(br + "        " + "name is different" + "        " + aa.book1.name + "            " + aa.book2.name + "        ");
                    br++;
                }
                if (aa.book1.id != aa.book2.id)
                {
                    Console.WriteLine(br + "        " + "id is different" + "        " + aa.book1.id + "                " + aa.book2.id + "        ");
                    br++;
                }

                
            }



            Console.ReadKey();
        }
    }
}
