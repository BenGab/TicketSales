using System;
using System.Collections.Generic;
using System.Linq;
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
            }
        }
    }
}
