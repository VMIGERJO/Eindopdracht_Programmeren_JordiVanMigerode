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
        public List<string> GetParents(string originalParentId);
    }
}
