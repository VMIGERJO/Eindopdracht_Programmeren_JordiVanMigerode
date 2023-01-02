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

        private int CalculateTotalPrice()
        {
            int totalPrice = 0;
            foreach (Evenement evenement in _evenementen)
            {
                totalPrice += evenement.Price;
            };
            return totalPrice;
        }

        public int TotalPrice { get => CalculateTotalPrice(); }
    }
}
