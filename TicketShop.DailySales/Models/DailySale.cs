using System;

namespace TicketShop.DailySales.Models
{
    public class DailySale
    {
        public DateTime Date { get; set; }
        public string Seller { get; set; }
        public string Sector { get; set; }
        public int TicketsSold { get; set; }
        public override string ToString()
        {
            return $"{Date.ToShortDateString()}: Seller {Seller}, Sector {Sector}, Sold {TicketsSold}";
        }
    }
}
