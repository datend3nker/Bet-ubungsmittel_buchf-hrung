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
    /// Interaktionslogik für NeuerEntrag.xaml
    /// </summary>
    public partial class NeuerEntrag : Window
    {
        public NeuerEntrag()
        {
            InitializeComponent();
            using(BtmContext context = new BtmContext())
            {
                IQueryable<Stufe> Pro = from Stufe in context.Stufen select Stufe;
                cb_Stufe.ItemsSource = Pro.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            using (BtmContext context = new BtmContext())
            {
                Eintrag leerEintrag = new Eintrag();
                IQueryable<int> query = from Stufe in context.Stufen
                                        where Stufe.MaterialName == cb_Stufe.Text
                                        select Stufe.StufId;
                var x = query.ToList();
                if (x.Any())
                {
                    leerEintrag.StufId = x[0];
                    if (tb_Bezeichnung.Text == "")
                    {
                        tb_Bezeichnung.Background = Brushes.Red;
                    }
                    else
                    {
                        tb_Bezeichnung.Background = Brushes.White;
                        leerEintrag.Bezeichnung = tb_Bezeichnung.Text;
                    }
                    if (tb_LdfNr.Text == "")
                    {
                        tb_LdfNr.Background = Brushes.Red;
                    }
                    else
                    {
                        tb_LdfNr.Background = Brushes.White;
                        leerEintrag.LfdNr = tb_LdfNr.Text;
                    }
                    
                    bool eintcor = float.TryParse(tb_Anfangsbestand.Text, out float eintranf);
                    if (eintcor)
                    {
                        tb_Anfangsbestand.Background = Brushes.White;
                        leerEintrag.Anfangsbestand = eintranf;
                        string einh = cb_Einheit.Text;
                        switch (einh)
                        {
                            case "Gramm":
                                leerEintrag.Einheit = 1;
                                break;
                            case "Kilogramm":
                                leerEintrag.Einheit = 2;
                                break;
                            case "":
                                leerEintrag.Einheit = 3;
                                MessageBox.Show("Keine Einheit ausgewählt!", "Warnung");
                                break;
                        }
                        if ((leerEintrag.Einheit ==1) || (leerEintrag.Einheit == 2))
                        {
                            leerEintrag.IsFirst = true;
                            context.Einträge.Add(leerEintrag);
                            context.SaveChanges();
                            main.UpdateTreeView();
                            this.Close();
                        }
                    }
                    else
                    {
                        tb_Anfangsbestand.Background = Brushes.Red;
                        string einh = cb_Einheit.Text;
                        switch (einh)
                        {
                            case "Gramm":
                                leerEintrag.Einheit = 1;
                                break;
                            case "Kilogramm":
                                leerEintrag.Einheit = 2;
                                break;
                            case "":
                                leerEintrag.Einheit = 3;
                                MessageBox.Show("Keine Einheit ausgewählt!", "Warnung");
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Keine Stufe ausgewählt!", "Warnung");
                }
            }
        }
    }
}
