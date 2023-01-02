using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtGentseFeesten.Domain.Model;


namespace EindOpdrachtGentseFeesten.Domain.Repository
{
    public interface IEvenementRepository 
    {
        public List<BaseLevelEvenement> GetBaseLevelChildEvenementen(string id);
        public List<HigherLevelEvenement> GetHigherLevelChildEvenementen(string id);
        public List<HigherLevelEvenement> GetAllTopLevelEvenementen();
        public List<BaseLevelEvenement> GetAllBaseLevelDescendants(string id);
    }
}
