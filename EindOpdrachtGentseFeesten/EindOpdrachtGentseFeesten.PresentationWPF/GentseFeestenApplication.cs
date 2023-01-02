using System;
using EindOpdrachtGentseFeesten.Domain;
using EindOpdrachtGentseFeesten.Domain.Model;

namespace EindOpdrachtGentseFeesten.PresentationWPF
{
    public class GentseFeestenApplication
    {
        private DomainController _controller;
        private EvenementWindow _evenementWindow;
        public GentseFeestenApplication(DomainController domaincontroller)
        {
            _controller = domaincontroller;
            EvenementWindow _evenementWindow = new EvenementWindow(_controller.GetAllTopLevelEvenementVerzamelingen());
            _evenementWindow.EvenementExpanded += EvenementWindow_EvenementExpanded;
            _evenementWindow.Show();
        }

        private void EvenementWindow_EvenementExpanded(object? sender, Evenement e)
        {
            _evenementWindow.EvenementenToLoad = _controller.GetChildren(e.Id);
        }
    }
}
