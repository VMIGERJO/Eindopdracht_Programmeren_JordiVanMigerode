using EindOpdrachtGentseFeesten.Domain.Repository;
using EindOpdrachtGentseFeesten.Domain.Model;
namespace EindOpdrachtGentseFeesten.Domain

{
    public class DomainController
    {
        private readonly IEvenementRepository _repo;

        private readonly EvenementPlanner _planner;
        public DomainController(IEvenementRepository repo)
        {
            _repo = repo;
            
            List<Evenement> evenementenOnPlannerAtStartup = _repo.GetEvenementenOnPlanner();
            FinishBuildingEvenementen(evenementenOnPlannerAtStartup);
            _planner = new(evenementenOnPlannerAtStartup);
        }
        public void AddToPlanner(Evenement evenement)
        {
            _planner.AddToPlannedEvenementen(evenement, _repo);
        }
        public void RemoveFromPlanner(Evenement evenement)
        {
            _planner.RemoveFromPlannedEvenementen(evenement, _repo);
        }
        public List<Evenement> GetEvenementenOnPlanner()
        {
            foreach (var item in _planner._plannedEvenementen)
            {
                System.Diagnostics.Trace.Write($"in dc: {item}");
            }
            return _planner._plannedEvenementen;
        }
        public List<Evenement> GetChildren(string id)
        {
            List<Evenement> children = _repo.GetChildEvenementen(id);
            FinishBuildingEvenementen(children);
            return children;
        }
        public List<HigherLevelEvenement> GetAllTopLevelEvenementVerzamelingen()
        {
            List<HigherLevelEvenement> topLevelEvenementen = _repo.GetAllTopLevelEvenementen();
            FinishBuildingEvenementen(topLevelEvenementen);
            return topLevelEvenementen;
        }
        private void FinishBuildingEvenementen(IEnumerable<Evenement> evenementen)
        {
            if (evenementen.Any())
            {
                foreach (Evenement evenement in evenementen)
                {
                    if (evenement.ChildIds.Any())
                    {
                        List<BaseLevelEvenement> descendants = _repo.GetAllBaseLevelDescendants(evenement.Id);
                        evenement.CalculateStart(descendants);
                        evenement.CalculateEnd(descendants);
                        evenement.CalculatePrice(descendants);
                    }

                }
            }
        }

    }
}