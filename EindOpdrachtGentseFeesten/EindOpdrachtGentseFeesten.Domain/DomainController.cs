using EindOpdrachtGentseFeesten.Domain.Repository;
using EindOpdrachtGentseFeesten.Domain.Model;
namespace EindOpdrachtGentseFeesten.Domain

{
    public class DomainController
    {
        private readonly IEvenementRepository _evenementRepository;
        private readonly IEvenementVerzamelingRepository _evenementVerzamelingRepository;

        public DomainController(IEvenementRepository evenementRepository, IEvenementVerzamelingRepository evenementVerzamelingRepository)
        {
            _evenementRepository = evenementRepository;
            _evenementVerzamelingRepository = evenementVerzamelingRepository;
        }
        public List<Evenement> GetChildren(string parentId)
        {
            return _evenementVerzamelingRepository.GetChildren(parentId);
        }
        public List<EvenementVerzameling> GetAllTopLevelEvenementVerzamelingen()
        {
            return _evenementVerzamelingRepository.GetAllTopLevelEvenementVerzamelingen();
        }

    }
}