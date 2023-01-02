using EindOpdrachtGentseFeesten.Persistence;
using EindOpdrachtGentseFeesten.Domain;
using EindOpdrachtGentseFeesten.Presentation;


EvenementMapper mapper  = new EvenementMapper();
DomainController domain = new DomainController(mapper);
GentseFeestenApplication app = new GentseFeestenApplication(domain);
