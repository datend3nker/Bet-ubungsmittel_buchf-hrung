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
                string y = tb_BtmBestandsbuchNr.Text;
                ProjektLeer.Produktbezeichnung = tb_Produktbezeichnung.Text;
                ProjektLeer.Zeitraum = tb_Zeitraum.SelectedDate.GetValueOrDefault(DateTime.Today);
                string ProduktNr = tb_ProduktNr.Text;
                string stufenzahl = tb_Stufenanzahl.Text;
                bool ProCorr = Int32.TryParse(ProduktNr, out int nr);
                bool stufcor = Int32.TryParse(stufenzahl, out int stufenZahl);
                if(ProCorr && stufcor && !(tb_BtmBestandsbuchNr.Text == "") && !(tb_Produktbezeichnung.Text == ""))
                {
                    tb_ProduktNr.Background = Brushes.White;
                    tb_Stufenanzahl.Background = Brushes.White;

                    ProjektLeer.ProduktNr = nr;
                    ProjektLeer.Stufenanzahl = stufenZahl;
                    context.Projekte.Add(ProjektLeer);
                    context.SaveChanges();
                    main.UpdateTreeView();
                    this.Close();
                }
                else
                {
                    if (!ProCorr)
                    {
                        tb_ProduktNr.Background = Brushes.Red;
                    }
                    else
                    {
                        tb_ProduktNr.Background = Brushes.White;
                    }
                    if (!stufcor)
                    {
                        tb_Stufenanzahl.Background = Brushes.Red;
                    }
                    else
                    {
                        tb_Stufenanzahl.Background = Brushes.White;
                    }
                    if(tb_BtmBestandsbuchNr.Text == "")
                    {
                        tb_BtmBestandsbuchNr.Background = Brushes.Red;
                    }
                    else
                    {
                        tb_BtmBestandsbuchNr.Background = Brushes.White;
                    }
                    if(tb_Produktbezeichnung.Text == "")
                    {
                        tb_Produktbezeichnung.Background = Brushes.Red;
                    }
                    else
                    {
                        tb_Produktbezeichnung.Background = Brushes.White;
                    }
                }
            }
        }
    }
}
