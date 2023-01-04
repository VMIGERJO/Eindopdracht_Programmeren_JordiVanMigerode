using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtGentseFeesten.Domain.Model
{
    public class BaseLevelEvenement : Evenement
    {
        public BaseLevelEvenement(string id, string name, string description, string parentId, List<string> childIds, DateTime start, DateTime end, int price) : base(id, name, description, parentId, childIds, start, end, price)
        {
        }
    }
}
