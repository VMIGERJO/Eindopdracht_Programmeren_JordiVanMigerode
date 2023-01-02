using EindOpdrachtGentseFeesten.Domain.Repository;
using EindOpdrachtGentseFeesten.Domain.Model;
namespace EindOpdrachtGentseFeesten.Domain

{
    public class DomainController
    {
        private readonly IEvenementRepository _repo;

        public DomainController(IEvenementRepository repo)
        {
            _repo = repo;
        }
        public List<Evenement> GetChildren(string id)
        {
            List<Evenement> children = new List<Evenement>();
            List<HigherLevelEvenement> higherLevelChildren = _repo.GetHigherLevelChildEvenementen(id);
            if (higherLevelChildren.Any())
            {
                foreach (HigherLevelEvenement evenement in higherLevelChildren)
                {
                    List<BaseLevelEvenement> descendants = _repo.GetBaseLevelChildEvenementen(evenement.Id);
                    evenement.CalculateStart(descendants);
                    evenement.CalculateEnd(descendants);
                    evenement.CalculatePrice(descendants);
                }
                children.AddRange(higherLevelChildren);
            }
            List<BaseLevelEvenement> baseLevelChildren = _repo.GetBaseLevelChildEvenementen(id);
            children.AddRange(baseLevelChildren);
            return children;
        }
        public List<HigherLevelEvenement> GetAllTopLevelEvenementVerzamelingen()
        {
            List<HigherLevelEvenement> topLevelEvenementen = _repo.GetAllTopLevelEvenementen();
            if (topLevelEvenementen.Any())
            {
                foreach (HigherLevelEvenement evenement in topLevelEvenementen)
                {
                    List<BaseLevelEvenement> descendants = _repo.GetAllBaseLevelDescendants(evenement.Id);
                    evenement.CalculateStart(descendants);
                    evenement.CalculateEnd(descendants);
                    evenement.CalculatePrice(descendants);
                }
            }
            return topLevelEvenementen;
        }

    }
}