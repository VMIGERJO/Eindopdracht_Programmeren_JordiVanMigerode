using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtGentseFeesten.Domain.Repository;

namespace EindOpdrachtGentseFeesten.Domain.Model
{
    public class EvenementPlanner
    {
        public EvenementPlanner(List<Evenement> currentEvenementenOnPlanner)
        {
            _plannedEvenementen = currentEvenementenOnPlanner;
        }
        public List<Evenement> _plannedEvenementen;
        public List<Evenement> PlannedEvenementen
        {
            get
            {
                _plannedEvenementen.Sort();
                return _plannedEvenementen;
            }
        }
        public void AddToPlannedEvenementen(Evenement evenementToAdd, IEvenementRepository repository)
        {
            if (_plannedEvenementen.Contains(evenementToAdd))
            {
                throw new PlannerException($"{evenementToAdd} was already added to the Planner");
            }

            foreach (Evenement currentlyPlannedEvenement in _plannedEvenementen)
            {
                if (evenementToAdd.Start <= currentlyPlannedEvenement.End && evenementToAdd.End >= currentlyPlannedEvenement.Start)
                {
                    throw new PlannerException($"{evenementToAdd} could not be added to Planner" +
                        $" as it overlaps with currently planned evenement {currentlyPlannedEvenement}");
                }

            }

            _plannedEvenementen.Add(evenementToAdd);
            repository.SaveToPlanner(evenementToAdd.Id);
        }
        public void RemoveFromPlannedEvenementen(Evenement evenementToRemove, IEvenementRepository repository)
        {
            foreach (var item in _plannedEvenementen)
            {
            }
            if (_plannedEvenementen.Remove(evenementToRemove))
            {
                repository.RemoveFromPlanner(evenementToRemove.Id);
            }
        }

        private int CalculateTotalPrice()
        {
            int totalPrice = 0;
            foreach (Evenement evenement in _plannedEvenementen)
            {
                totalPrice += evenement.Price;
            };
            return totalPrice;
        }

        public int TotalPrice { get => CalculateTotalPrice(); }
    }
}
