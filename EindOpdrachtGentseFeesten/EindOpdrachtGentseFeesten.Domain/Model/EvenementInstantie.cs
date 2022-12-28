using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtGentseFeesten.Domain.Model
{
    public class EvenementInstantie : Evenement
    {
        public EvenementInstantie(string id, string name, string description, string parentEvenement, DateTime start, DateTime end, int price) : base(id, name, description, parentEvenement)
        {
            this.Start = start;
            this.End = end;
            this.Price = price;
        }
    }
}
