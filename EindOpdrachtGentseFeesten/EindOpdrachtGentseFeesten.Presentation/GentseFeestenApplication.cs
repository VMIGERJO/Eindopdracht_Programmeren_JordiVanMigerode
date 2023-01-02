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

            Console.WriteLine("Geef een Id in:");
            string id2 = Console.ReadLine();

            foreach (Evenement evenement in _domainController.GetChildren(id2))
            {
                ShowInfo(evenement);
            }


        }
        public static void ShowInfo(Evenement e)
        {
            Console.WriteLine($"Name: {e.Name}");
            Console.WriteLine($"Id: {e.Id}");
            Console.WriteLine($"Description: {e.Description}");
            Console.WriteLine($"Start: {e.Start}");
            Console.WriteLine($"Einde: {e.End}");
            Console.WriteLine($"Prijs: {e.Price}");
            Console.WriteLine();
        }



    }
}
