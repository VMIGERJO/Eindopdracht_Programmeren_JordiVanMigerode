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
        public List<Evenement> GetChildEvenementen(string id);
        public List<HigherLevelEvenement> GetAllTopLevelEvenementen();
        public List<BaseLevelEvenement> GetAllBaseLevelDescendants(string id);
        public void SaveToPlanner(string id);
        public void RemoveFromPlanner(string id);
        public List<Evenement> GetEvenementenOnPlanner();
    }
}
