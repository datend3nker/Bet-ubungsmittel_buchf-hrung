using BtmManager.Data;
using BtmManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaktionslogik für NeueStufe.xaml
    /// </summary>
    public partial class NeueStufe : Window
    {
        public NeueStufe()
        {
            InitializeComponent();
            using (BtmContext context = new BtmContext())
            {
                IQueryable<Projekt> Pro = from Projekt in context.Projekte select Projekt;
                cb_Projekt.ItemsSource = Pro.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            using(BtmContext context = new BtmContext())
            {
                var leerestufe = new Stufe { };
                leerestufe.MaterialName = tb_MaterialName.Text;
                string stufnr = tb_StufenNummer.Text;
                bool stufcor = Int32.TryParse(stufnr, out int stufennr);
                if (stufcor)
                {
                    tb_StufenNummer.Background = Brushes.White;
                    leerestufe.StufenNummer = stufennr;
                    IQueryable<int> query = from Projekt in context.Projekte
                                where Projekt.Produktbezeichnung == cb_Projekt.Text
                                select Projekt.ProjektId;
                    var x = query.ToList();
                    if (x.Any())
                    {
                        leerestufe.ProjektId = x[0];
                        context.Stufen.Add(leerestufe);
                        context.SaveChanges();
                        main.UpdateTreeView();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kein Projekt ausgewählt!", "Warnung");
                    }
                    
                }
                else
                {
                    tb_StufenNummer.Background = Brushes.Red;
                }
            }
            

        }
    }
}
