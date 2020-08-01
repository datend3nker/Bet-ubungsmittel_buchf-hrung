using BtmManager.Data;
using BtmManager.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BtmManager
{ 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateTabelle();
            UpdateListe();
        }

        private void btn_neuer_Click(object sender, RoutedEventArgs e)
        {
            using(BtmContext context = new BtmContext())
            {
                var eintragleer = new Eintrag { };
                context.Einträge.Add(eintragleer);
                context.SaveChanges();
                UpdateTabelle();
            }
        }
        void UpdateTabelle()
        {
            using (BtmContext context = new BtmContext())
            {
                var result = from Eintrag in context.Einträge select Eintrag;
                var xc = result.ToList();
                DataGrid.ItemsSource = result.ToList();
            }
        }
        void UpdateListe()
        {
            using (BtmContext context = new BtmContext())
            {
                var pro = from Projekt in context.Projekte select Projekt;
                TreeView.ItemsSource = pro.ToList();
            }
        }
    }
}
