using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtGentseFeesten.Domain.Model
{
    public class Planner
    {
        private List<Evenement> _evenementen;
        public List<Evenement> Evenementen
        {
            get
            {
                return _evenementen;
            }
            set
            {
                _evenementen = value;
            }
        }

        private int CalculateTotalPrice(List <Evenement> evenementen)
        {
            int totalPrice = 0;
            foreach (var evenement in evenementen)
            {
                totalPrice += evenement.Price;
            };
            return totalPrice;
        }

        public int TotalPrice { get => CalculateTotalPrice(this._evenementen); }
    }
}
