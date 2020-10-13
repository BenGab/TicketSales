using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TicketShop.DailySales.Models;

namespace TicketSales.XmlSaver
{
    public class XmlGenerator
    {
        public static XDocument GenerateXML(ICollection<DailySale> dailySales)
        {
            XDocument doc = new XDocument(new XElement("stats"));

            var q1 = from dailysale in dailySales
                     group dailysale by dailysale.Date into grp
                     orderby grp.Key
                     select grp;

            foreach(var groupItem in q1)
            {
                XElement node = new XElement("day");

                node.SetAttributeValue("date",groupItem.Key.Date.ToShortDateString());

                var sectorq1 = from item in groupItem
                               group item by item.Sector into sectorGrp
                               select new XElement("sector",
                                  new XAttribute("code", sectorGrp.Key),
                                  new XElement("sold", sectorGrp.Sum(x => x.TicketsSold)));

                var sellerq2 = from item in groupItem
                               group item by item.Seller into sellerGrp
                               select new XElement("seller",
                               new XAttribute("name", sellerGrp.Key),
                               new XElement("sold", sellerGrp.Sum(x=> x.TicketsSold)));

                sectorq1.ToList().ForEach(xelement => node.Add(xelement));
                sellerq2.ToList().ForEach(xelement => node.Add(xelement));

                doc.Root.Add(node);
            }

            return doc;
        }
    }
}
