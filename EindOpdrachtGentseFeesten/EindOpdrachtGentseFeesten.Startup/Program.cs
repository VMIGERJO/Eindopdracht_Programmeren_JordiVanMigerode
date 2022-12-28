using EindOpdrachtGentseFeesten.Persistence;
using EindOpdrachtGentseFeesten.Domain;
using EindOpdrachtGentseFeesten.Presentation;


EvenementMapper evenementMapper = new EvenementMapper();
EvenementVerzamelingMapper evenementVerzamelingMapper = new EvenementVerzamelingMapper();
DomainController domain = new DomainController(evenementMapper, evenementVerzamelingMapper);
GentseFeestenApplication app = new GentseFeestenApplication(domain);
