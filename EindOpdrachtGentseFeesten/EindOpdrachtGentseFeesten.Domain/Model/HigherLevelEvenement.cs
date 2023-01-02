using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtGentseFeesten.Domain.Model
{
    public class HigherLevelEvenement : Evenement
    {
        public HigherLevelEvenement(string id, string name, string description, string parentEvenementId, List<string> childIds) : base(id, name, description, parentEvenementId, childIds)
        {

        }
    }
    
}
