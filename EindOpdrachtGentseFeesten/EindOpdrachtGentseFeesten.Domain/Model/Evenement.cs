using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtGentseFeesten.Domain.Model
{
    public class Evenement
    {
        private readonly string _id;
        public string Id { get { return _id; } }
        public Evenement(string id, string name, string description, string parentEvenement) //List<string> ancestry//
        {
            _id = id;
            this.Name = name;
            this.Description = description;
            this.ParentEvenement = parentEvenement;
            //this.Ancestry = ancestry;///
        }

        public string Name { get; init; }
        public string Description { get; init; }
        public virtual DateTime Start { get; init; }
        public virtual DateTime End { get; init; }
        public virtual int Price { get; init; }
        public string ParentEvenement { get; init; }
        //public List<string> Ancestry {get ; init; }//

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(Evenement))
            {
                return false;
            }
            Evenement other = (Evenement)obj;
            if (this._id == other._id)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
