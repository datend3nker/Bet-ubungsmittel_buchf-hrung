using BtmManager.Data;
using BtmManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BtmManager
{
    /// <summary>
    /// Interaktionslogik für NeuesProjekt.xaml
    /// </summary>
    public partial class NeuesProjekt : Window
    {
        public NeuesProjekt()
        {
            InitializeComponent();
        }

        private void NeuesProjektErstellen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            using (BtmContext context = new BtmContext())
            {
                var ProjektLeer = new Projekt { };
                ProjektLeer.BtmBestandsbuchNr = tb_BtmBestandsbuchNr.Text;
                ProjektLeer.Produktbezeichnung = tb_Produktbezeichnung.Text;
                ProjektLeer.Zeitraum = tb_Zeitraum.SelectedDate.Value;
                string ProduktNr = tb_ProduktNr.Text;
                int nr;
                bool ProCorr = Int32.TryParse(ProduktNr, out nr);
                if (ProCorr)
                {
                    ProjektLeer.ProduktNr = nr;
                    tb_ProduktNr.Background = Brushes.White;
                    context.Projekte.Add(ProjektLeer);
                    context.SaveChanges();
                    main.UpdateTreeView();
                    this.Close();
                }
                else
                {
                    tb_ProduktNr.Background = Brushes.Red;
                }
            }
        }
    }
}
