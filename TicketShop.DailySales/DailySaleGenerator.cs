using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.DailySales.Models;

namespace TicketShop.DailySales
{
    public class DailySaleGenerator
    {
        private readonly string[] sellers;
        private readonly string[] secctors;
        private Random rnd;

        public DailySaleGenerator(string[] sellers, string[] secctors)
        {
            this.sellers = sellers;
            this.secctors = secctors;
            rnd = new Random();
        }

        public ICollection<DailySale> GenerateSales(int numDays, int numInstances, int maxsold)
        {
            List<DailySale> result = new List<DailySale>();

            for (int i = 0; i < numInstances; i++)
            {
                result.Add(new DailySale()
                {
                    Date = DateTime.Now.AddDays(rnd.Next(-numDays, 0)),
                    Sector = secctors[rnd.Next(secctors.Length -1)],
                    Seller = sellers[rnd.Next(sellers.Length - 1)],
                    TicketsSold = rnd.Next(0, maxsold + 1)
                });
            }

            return result;
        }
    }
}
