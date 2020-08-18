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
    /// Interaktionslogik für Reihe_markieren.xaml
    /// </summary>
    public partial class Reihe_markieren : Window
    {
        public DataGrid dg;
        public Reihe_markieren(DataGrid datagrid)
        {
            InitializeComponent();
            dg = datagrid;
        }

        private void btn_fertig_Click(object sender, RoutedEventArgs e)
        {
            int zeile;
            MainWindow main = new MainWindow();
            string zeilestr = cb_zeile.Text;
            List<Eintrag> p = this.dg.Items.OfType<Eintrag>().ToList();
            string farbe = cb_farbe.Text;
            if (zeilestr.Equals("Ausgewählte Zeile"))
            {
                zeile = dg.Items.IndexOf(dg.CurrentItem);
                if(zeile == -1)
                {
                    MessageBox.Show("Keine Tabelle/Logbuch geladen. Wählen sie bitte zuerst ein Logbuch aus!", "Warnung");
                    main.UpdateTabelle(main.BezeichnungSpace);
                    this.Close();
                    return;
                }
                using (BtmContext context = new BtmContext())
                {
                    if (p.Count == 0)
                    {
                        main.l_aktion.Content = "";
                        main.UpdateTabelle(main.BezeichnungSpace);
                        this.Close();
                        return;
                    }
                    else
                    {
                        var entry = context.Einträge.FirstOrDefault(item => item.EintragId == p[zeile].EintragId);
                        switch (farbe)
                        {
                            case "Rot":
                                entry.Farbe = "Red";
                                break;
                            case "Grün":
                                entry.Farbe = "Green";
                                break;
                            case "Gelb":
                                entry.Farbe = "Yellow";
                                break;
                            case "Lila":
                                entry.Farbe = "Purple";
                                break;
                            case "Nichts":
                                entry.Farbe = null;
                                break;
                        }
                        context.Einträge.Update(entry);
                    }

                    context.SaveChanges();
                }
            }
            else
            {
                bool zeilcor = Int32.TryParse(zeilestr, out zeile);
                if (zeilcor)
                {
                    using(BtmContext context = new BtmContext())
                    {
                        if (p.Count == 0)
                        {
                            main.l_aktion.Content = "";
                        }
                        else
                        {
                            var entry = context.Einträge.FirstOrDefault(item => item.EintragId == p[zeile].EintragId);
                            switch (farbe)
                            {
                                case "Rot":
                                    entry.Farbe = "Red";
                                    break;
                                case "Grün":
                                    entry.Farbe = "Green";
                                    break;
                                case "Gelb":
                                    entry.Farbe = "Yellow";
                                    break;
                                case "Lila":
                                    entry.Farbe = "Purple";
                                    break;
                                case "Nichts":
                                    entry.Farbe = null;
                                    break;
                            }
                            context.Einträge.Update(entry);
                        }
                    
                        context.SaveChanges();
                    }
                
                }
                else
                {
                    MessageBox.Show("Keine gültige Zeile", "Warnung");
                }
            }
            main.UpdateTabelle(main.BezeichnungSpace);
            this.Close();
        }
    }
}
