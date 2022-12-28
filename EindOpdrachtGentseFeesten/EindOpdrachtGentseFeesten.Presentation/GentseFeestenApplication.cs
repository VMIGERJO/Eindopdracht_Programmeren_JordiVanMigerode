using EindOpdrachtGentseFeesten.Domain;
using EindOpdrachtGentseFeesten.Domain.Model;
namespace EindOpdrachtGentseFeesten.Presentation

{
    public class GentseFeestenApplication
    {
        private DomainController _domainController;

        public GentseFeestenApplication(DomainController domainController)
        {
            _domainController = domainController;
            foreach (Evenement evenement in _domainController.GetAllTopLevelEvenementVerzamelingen())
            {
                ShowInfo(evenement);
            }

            Console.WriteLine("Geef een Id in:");
            string id = Console.ReadLine();

            foreach (Evenement evenement in _domainController.GetChildren(id))
            {
                ShowInfo(evenement);
            }


        }
        public void ShowInfo(Evenement evenement)
        {
            Console.WriteLine($"Name: {evenement.Name}");
            Console.WriteLine($"Id: {evenement.Id}");
            Console.WriteLine($"Description: {evenement.Description}");
            Console.WriteLine();
            if (evenement is EvenementInstantie e)
            {
                Console.WriteLine($"Start: {e.Start}");
                Console.WriteLine($"Einde: {e.End}");
                Console.WriteLine($"Prijs: {e.Price}");
            }
            Console.WriteLine();
        }

        
    }
}