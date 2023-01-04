using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtGentseFeesten.Domain.Model
{
    public class Evenement : IComparer<Evenement>
    {
        private readonly string _id;
        public string Id { get { return _id; } }
        public Evenement(string id, string name, string description, string parentId, List<String> childIds, DateTime start, DateTime end, int price)
        {
            _id = id;
            this.Name = name;
            this.Description = description;
            this.ParentId = parentId;
            this.ChildIds = childIds;
            this.Price = price;
            this.Start = start;
            this.End = end;

        }

        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Price { get; set; }
        public string ParentId { get; init; }
        public List<String> ChildIds { get; init; }
        public void CalculateStart(List<BaseLevelEvenement> baseLevelDescendants)
        {
            this.Start = baseLevelDescendants.Select(d => d.Start).Min();
        }
        public void CalculateEnd(List<BaseLevelEvenement> baseLevelDescendants)
        {
            this.End = baseLevelDescendants.Select(d => d.End).Max();
        }

        public void CalculatePrice(List<BaseLevelEvenement> baseLevelDescendants)
        {
            this.Price = baseLevelDescendants.Select(d => d.Price).Sum();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
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

        public int Compare(Evenement x, Evenement y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
