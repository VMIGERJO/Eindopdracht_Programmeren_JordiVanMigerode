using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtGentseFeesten.Domain.Model
{
    public class EvenementVerzameling : Evenement
    {
        public EvenementVerzameling(string id, string name, string description, string parentEvenement, List<string> childEvenementIds) : base(id, name, description, parentEvenement)
        {
            this.ChildEvenementIds = childEvenementIds;
        }

        public List<string> ChildEvenementIds { get; init; }
        public override DateTime End { get => base.End; init => base.End = CalculateEndTime(); }
        public override DateTime Start { get => base.Start; init => base.Start = CalculateStartTime(); }
        public override int Price { get => base.Price; init => base.Price = CalculatePrice(); }
        private DateTime CalculateEndTime()
        {
            throw new NotImplementedException();
        }
        private DateTime CalculateStartTime()
        {
            throw new NotImplementedException();
        }
        private int CalculatePrice()
        {
            throw new NotImplementedException();
        }
    }
    
}
