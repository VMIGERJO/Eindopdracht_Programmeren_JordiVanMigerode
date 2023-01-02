using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtGentseFeesten.Domain.Repository;

namespace EindOpdrachtGentseFeesten.Domain.Model
{
    static class HigherLevelEvenementBuilder
    {
        public static HigherLevelEvenement FinishBuildingHigherLevelEvent(HigherLevelEvenement evenement, IEvenementRepository repo)
        {
            List<BaseLevelEvenement> baseLevelDescendants = repo.GetAllBaseLevelDescendants(evenement.Id);
            SetStart(evenement, baseLevelDescendants);
            SetEnd(evenement, baseLevelDescendants);
            SetPrice(evenement, baseLevelDescendants);
            return evenement;
        }

        static void SetStart(HigherLevelEvenement evenement, List<BaseLevelEvenement> baseLevelDescendants)
        {
            evenement.Start  = baseLevelDescendants.Select(d => d.Start).Min();
        }

        static void SetEnd(HigherLevelEvenement evenement, List<BaseLevelEvenement> baseLevelDescendants)
        {
            evenement.End = baseLevelDescendants.Select(d => d.End).Max();
        }
        static void SetPrice(HigherLevelEvenement evenement, List<BaseLevelEvenement> baseLevelDescendants)
        {
            evenement.Price = baseLevelDescendants.Sum(d => d.Price);
        }
    }
}
