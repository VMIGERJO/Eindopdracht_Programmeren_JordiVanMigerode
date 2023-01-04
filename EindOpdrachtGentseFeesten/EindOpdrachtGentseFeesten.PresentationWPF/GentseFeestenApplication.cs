using System;
using EindOpdrachtGentseFeesten.Domain;
using EindOpdrachtGentseFeesten.Domain.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;

namespace EindOpdrachtGentseFeesten.PresentationWPF
{
    public class GentseFeestenApplication
    {
        private DomainController _controller;
        private EvenementWindow _evenementWindow;
        private PlannerWindow _plannerWindow;
        private const string genericErrorMessage = "due to error:\n{ex} : {ex.Message} caused by {ex.Source}";
        public GentseFeestenApplication(DomainController domaincontroller)
        {
            _controller = domaincontroller;
            _evenementWindow = new EvenementWindow(_controller.GetAllTopLevelEvenementVerzamelingen());
            _plannerWindow = new();
            _evenementWindow.EvenementExpanded += EvenementWindow_EvenementExpanded;
            _evenementWindow.AddToPlannerSelected += EvenementWindow_AddToPlannerSelected;
            _evenementWindow.GoToPlannerSelected += EvenementWindow_GoToPlannerSelected;
            _plannerWindow.RemoveFromPlannerSelected += _plannerWindow_RemoveFromPlannerSelected;
            _plannerWindow.NavigatedBackToEvenementen += _plannerWindow_NavigatedBackToEvenementen;
            _evenementWindow.Show();
        }

        private void _plannerWindow_NavigatedBackToEvenementen(object? sender, EventArgs e)
        {
            _plannerWindow.Hide();
            if (_evenementWindow.triggeringShutdown == false)
            {
                _evenementWindow.Show();
            }
        }

        private void _plannerWindow_RemoveFromPlannerSelected(object? sender, Evenement e)
        {
            _controller.RemoveFromPlanner(e);
            _plannerWindow.PlannerList.ItemsSource = new List<Evenement>(_controller.GetEvenementenOnPlanner());
        }

        private void EvenementWindow_GoToPlannerSelected(object? sender, EventArgs e)
        {
            try
            {
                _evenementWindow.Hide();
                ObservableCollection<Evenement> itemsOnPlanner = new ObservableCollection<Evenement>(_controller.GetEvenementenOnPlanner());
                _plannerWindow.PlannerList.ItemsSource = itemsOnPlanner;
                _plannerWindow.Show();
            }
            catch (PlannerException ex)
            {
                MessageBox.Show(_evenementWindow, $"Planner could not be loaded" + genericErrorMessage, "Error loading planner", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void EvenementWindow_AddToPlannerSelected(object? sender, Evenement e)
        {
            try
            {
                _controller.AddToPlanner(e);
                MessageBox.Show(_evenementWindow, "Evenement was successfully added to the planner", "Alert", MessageBoxButton.OK, MessageBoxImage.None);
            }
            catch (PlannerException ex)
            {

                MessageBox.Show(_evenementWindow, ex.Message, "Unsuccesful", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            


        }

        private void EvenementWindow_EvenementExpanded(object? sender, Evenement e)
        {
            try
            {
                _evenementWindow.EvenementenToLoad = _controller.GetChildren(e.Id);
            }
            catch (Exception)
            {

                MessageBox.Show(_evenementWindow, "Evenement could not be expanded" + genericErrorMessage, "Unsuccesful", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            
        }
    }
}
