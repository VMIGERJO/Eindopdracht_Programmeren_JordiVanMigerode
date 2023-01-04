using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtGentseFeesten.Domain.Model;

namespace EindOpdrachtGentseFeesten.PresentationWPF
{
    public class GentseFeestenEvenementView
    {
        public GentseFeestenEvenementView(Evenement evenement)
        {
            Name = evenement.Name;
            Description = evenement.Description;
            Start = evenement.Start;
            End = evenement.End;
            Price = evenement.Price;
        }

        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
        public int Price { get; init; }

    }

}
