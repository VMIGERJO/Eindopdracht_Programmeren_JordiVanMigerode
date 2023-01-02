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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EvenementWindow : Window
    {
        private const string loadingText = "Loading...";
        private List<Evenement> _evenementenToLoad;
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
        public List<Evenement> EvenementenToLoad
        {
            get => _evenementenToLoad;
            set
            {
                _evenementenToLoad = value;
            }
        }
        public TreeViewItem CreateTreeItem(Evenement e)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = e.ToString();
            item.Tag = e;
            item.Items.Add(loadingText);
            return item;
        }

        private void EvenementenTrv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void EvenementTrvItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if ((item.Items.Count == 1) && ((string)item.Items[0] == loadingText))
            {
                item.Items.Clear();
                Evenement expandedEvenement;
                if (item.Tag is HigherLevelEvenement)
                {
                    expandedEvenement = (HigherLevelEvenement)item.Tag;
                    EvenementExpanded.Invoke(this, expandedEvenement);
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
    }
}


