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
    /// Interaction logic for EvenementWindow.xaml
    /// </summary>
    public partial class EvenementWindow : Window
    {
        private const string loadingText = "Loading...";
        public bool triggeringShutdown = false;
        private List<Evenement> _evenementenToLoad = new List<Evenement>();
        public EvenementWindow(List<HigherLevelEvenement> topLevelEvenementen)
        {
            InitializeComponent();
            _evenementenToLoad.AddRange(topLevelEvenementen);
            foreach (HigherLevelEvenement e in _evenementenToLoad)
            {
                EvenementenTrv.Items.Add(CreateTreeItem(e));
            }
        }
        public event EventHandler<Evenement> EvenementExpanded;
        public event EventHandler<Evenement> AddToPlannerSelected;
        public event EventHandler GoToPlannerSelected;
        public List<Evenement> EvenementenToLoad
        {
            get => _evenementenToLoad;
            set
            {
                _evenementenToLoad = value;
            }
        }
        private static TreeViewItem CreateTreeItem(Evenement e)
        {
            TreeViewItem item = new TreeViewItem
            {
                Header = e.ToString(),
                Tag = e
            };
            if (e.ChildIds.Any())
            {
                item.Items.Add(loadingText);
            }
            return item;
        }

        private void EvenementenTrv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = sender as TreeView;
            TreeViewItem item = treeView.SelectedItem as TreeViewItem;
            Evenement evenement = (Evenement)item.Tag;
            if (evenement != null)
            {
                EvenementContent.ItemsSource = new List<GentseFeestenEvenementView> { new GentseFeestenEvenementView(evenement) };
            }
        }

        private void EvenementTrvItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;
            try
            {
                if ((item.Items.Count == 1) && ((string)item.Items[0] == loadingText))
                {
                    item.Items.Clear();
                    Evenement expandedEvenement;
                    if (item.Tag is HigherLevelEvenement)
                    {
                        expandedEvenement = (HigherLevelEvenement)item.Tag;
                        EvenementExpanded?.Invoke(this, expandedEvenement);
                        try
                        {
                            foreach (Evenement evenement in _evenementenToLoad)
                                item.Items.Add(CreateTreeItem(evenement));
                        }
                        catch (Exception ex)
                        {
                            TextBox txtError = new TextBox();
                            txtError.Text = "An error occurred: " + ex.Message;
                            txtError.Style = (Style)FindResource("ErrorTextBoxStyle");
                            this.Content = txtError;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                TextBox txtError = new TextBox();
                txtError.Text = "An error occurred: " + ex.Message;
                this.Content = txtError;
            }

        }

        private void EvenementContent_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            var dataGrid = sender as DataGrid;
            if (e.Column is DataGridTextColumn textColumn)
            {
                textColumn.ElementStyle = dataGrid.FindResource(typeof(TextBlock)) as Style;
                e.Column.MaxWidth = 200;
                e.Column.Width = DataGridLength.SizeToCells;

            }
        }

        private void AddToPlanner_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = EvenementenTrv.SelectedItem as TreeViewItem;
            Evenement selectedEvenement = item.Tag as Evenement;
            AddToPlannerSelected?.Invoke(this, selectedEvenement);

        }

        private void GoToPlanner_Click(object sender, RoutedEventArgs e)
        {
            GoToPlannerSelected?.Invoke(this, EventArgs.Empty);
        }

        private void EvenementenWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            triggeringShutdown = true;
            Application.Current.Shutdown();
        }
    }
}


