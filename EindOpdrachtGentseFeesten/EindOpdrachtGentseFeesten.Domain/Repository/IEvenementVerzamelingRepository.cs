using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtGentseFeesten.Domain.Model;


namespace EindOpdrachtGentseFeesten.Domain.Repository
{
    public interface IEvenementVerzamelingRepository : IEvenementRepository
    {
        public List<Evenement> GetChildren(string parentId);
        public List<EvenementVerzameling> GetAllTopLevelEvenementVerzamelingen();
        public string GetTableLocationById(string Id);
    }
}
