using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EindOpdrachtGentseFeesten.Domain.Model;

namespace EindOpdrachtGentseFeesten.PresentationWPF
{
    /// <summary>
    /// Interaction logic for PlannerWindow.xaml
    /// </summary>
    public partial class PlannerWindow : Window
    {
        public PlannerWindow()
        {
            InitializeComponent();
        }
        public event EventHandler<Evenement> RemoveFromPlannerSelected;
        public event EventHandler NavigatedBackToEvenementen;

        private void RemoveFromPlanner_Click(object sender, RoutedEventArgs e)
        {
            Evenement evenementToRemove = PlannerList.SelectedItem as Evenement;
            RemoveFromPlannerSelected?.Invoke(this, evenementToRemove);
            PlannerContent.ItemsSource = null;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigatedBackToEvenementen?.Invoke(this, EventArgs.Empty);
        }

        private void PlannerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NavigatedBackToEvenementen?.Invoke(this, EventArgs.Empty);
            e.Cancel = true;
        }

        private void PlannerContent_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (e.Column is DataGridTextColumn textColumn)
            {
                textColumn.ElementStyle = dataGrid.FindResource(typeof(TextBlock)) as Style;
                e.Column.MaxWidth = 200;
                e.Column.Width = DataGridLength.SizeToCells;

            }
        }

        private void PlannerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox plannerList = sender as ListBox;
            Evenement evenement = plannerList.SelectedItem as Evenement;
            if (evenement != null)
            {
                PlannerContent.ItemsSource = new List<GentseFeestenEvenementView> { new GentseFeestenEvenementView(evenement) };
            }
        }
    }
}
