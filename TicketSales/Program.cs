using System;
using System.Collections.Generic;
using System.Linq;
using TicketSales.XmlSaver;
using TicketShop.DailySales;
using TicketShop.DB;

namespace TicketSales
{

    static class Extensions
    {
        public static void ToConsole<T>(this IEnumerable<T> input, string str)
        {
            Console.WriteLine("*** BEGIN " + str);
            foreach (T item in input)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("*** END " + str);
            Console.ReadLine();
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            using (TicketDbContext ctx = new TicketDbContext())
            {
                ctx.Venues.Select(x => x.Name).ToConsole("VENUES");
                string[] sectors = ctx.Sectors.Select(x => x.Code).ToArray();
                sectors.ToConsole("SECTORS");
                string[] sellers = ctx.Sellers.Select(x => x.Name).ToArray();
                sellers.ToConsole("SELLERS");

                DailySaleGenerator gen = new DailySaleGenerator(sellers, sectors);
                var list = gen.GenerateSales(10, 6, 10);
                list.ToConsole("LIST");

                var xdoc = XmlGenerator.GenerateXML(list);

                Console.WriteLine(xdoc);

                var perseller = from sellerNode in xdoc.Descendants("seller")
                                group sellerNode by sellerNode.Attribute("name").Value into grp
                                orderby grp.Key
                                select new
                                {
                                    Seller = grp.Key,
                                    TotalSold = grp.Sum(x => (int)x.Element("sold"))
                                };

                perseller.ToConsole("PER SELLER");

                var perSector = from sectornode in xdoc.Descendants("sector")
                                group sectornode by sectornode.Attribute("code").Value into grp
                                orderby grp.Key
                                select new
                                {
                                    Sector = grp.Key,
                                    TotalSold = grp.Sum(x => (int)x.Element("sold"))
                                };

                perSector.ToConsole("PER SECTOR");

            }
        }
    }
}
