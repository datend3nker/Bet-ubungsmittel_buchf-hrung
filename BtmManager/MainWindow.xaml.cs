using BtmManager.Data;
using BtmManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        public string BezeichnungSpace;
        public byte EinheitSpace;
        public int StufIdSpace;
        public MainWindow()
        {
            InitializeComponent();
            UpdateTreeView();

        }

        public void UpdateTabelle(string suche)
        {
            using (BtmContext context = new BtmContext())
            {
                var result = (from Eintrag in context.Einträge
                              where Eintrag.Bezeichnung == suche
                              select Eintrag).ToList();
                this.DataGrid.ItemsSource = result;
            }
        }

        public void UpdateTreeView()
        {
            TreeView.Items.Clear();
            using (BtmContext context = new BtmContext())
            {
                var pro = (from Projekt in context.Projekte select Projekt).ToList();
                var stu = (from Stufe in context.Stufen select Stufe).ToList();
                var ein = (from Eintrag in context.Einträge select Eintrag).ToList();
                foreach (var proj in pro)
                {
                    TreeViewItem newProject = new TreeViewItem
                    {
                        Header = proj.Produktbezeichnung
                    };
                    foreach (var stuf in stu)
                    {
                        if (stuf.ProjektId == proj.ProjektId)
                        {
                            TreeViewItem newStufe = new TreeViewItem
                            {
                                Header = stuf.MaterialName
                            };
                            newProject.Items.Add(newStufe);
                            foreach (var eint in ein)
                            {
                                if (eint.StufId == stuf.StufId)
                                {
                                    if (eint.IsFirst == true)
                                    {
                                        TreeViewItem newEintrag = new TreeViewItem
                                        {
                                            Header = eint.Bezeichnung,
                                        };
                                        newEintrag.Selected += NewEintrag_Selected;
                                        newStufe.Items.Add(newEintrag);
                                    }
                                }
                            }
                        }

                    }
                    TreeView.Items.Add(newProject);
                }
            }


        }

        private void NewEintrag_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem gh = (TreeViewItem)sender;
            string sendrename = (string)gh.Header;
            this.BezeichnungSpace = sendrename;

            this.UpdateTabelle(sendrename);
            this.l_logbuch.Content = sendrename;
            using (BtmContext context = new BtmContext())
            {
                this.EinheitSpace = ((from Eintrag in context.Einträge
                                      where Eintrag.Bezeichnung == this.BezeichnungSpace
                                      select Eintrag.Einheit).ToList())[0];

                this.StufIdSpace = ((from Eintrag in context.Einträge
                                     where Eintrag.Bezeichnung == this.BezeichnungSpace
                                     select Eintrag.StufId).ToList())[0];

                var stufasparentID = (from Eintrag in context.Einträge
                                      where Eintrag.Bezeichnung == sendrename
                                      select Eintrag.StufId).ToList();

                l_stufe.Content = ((from Stufe in context.Stufen
                                    where Stufe.StufId == stufasparentID[0]
                                    select Stufe.MaterialName).ToList())[0];

                var projasparentID = (from Stufe in context.Stufen
                                      where Stufe.StufId == stufasparentID[0]
                                      select Stufe.ProjektId).ToList();

                l_projektname.Content = ((from Projekt in context.Projekte
                                          where Projekt.ProjektId == projasparentID[0]
                                          select Projekt.Produktbezeichnung).ToList())[0];
                switch (this.EinheitSpace)
                {
                    case 1:
                        rbtn_gramm.IsChecked = true;
                        rbtn_kilogramm.IsChecked = false;
                        break;
                    case 2:
                        rbtn_kilogramm.IsChecked = true;
                        rbtn_gramm.IsChecked = false;
                        break;
                }
            }

        }

        private void m_Projekt_Click(object sender, RoutedEventArgs e)
        {

            NeuesProjekt neuesprojekt = new NeuesProjekt();
            neuesprojekt.ShowDialog();
            UpdateTreeView();
        }

        private void m_beenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void m_stufe_Click(object sender, RoutedEventArgs e)
        {
            NeueStufe neuestufe = new NeueStufe();
            neuestufe.ShowDialog();
            UpdateTreeView();
        }

        private void m_logbuch_Click(object sender, RoutedEventArgs e)
        {
            NeuerEntrag neuereintrag = new NeuerEntrag();
            neuereintrag.ShowDialog();
            UpdateTreeView();
        }

        private void m_übertragen_Click(object sender, RoutedEventArgs e)
            
        {
            
            using(BtmContext context = new BtmContext())
            {
                List<Eintrag> p = this.DataGrid.Items.OfType<Eintrag>().ToList();
                var deleteobject = from Eintrag in context.Einträge
                                   where Eintrag.Bezeichnung == this.BezeichnungSpace
                                   select Eintrag; 

                foreach(var ein in p)
                {
                    Eintrag einleer = new Eintrag()
                    {
                        Einheit = EinheitSpace,
                        Bezeichnung = BezeichnungSpace,
                        LfdNr = ein.LfdNr,
                        Datum = ein.Datum,
                        Anfangsbestand = ein.Anfangsbestand,
                        TheroZugang = ein.TheroZugang,
                        PrakZugang = ein.PrakZugang,
                        Arbeitsverlust = ein.Arbeitsverlust,
                        Abgang = ein.Abgang,
                        AktuellerBestand = ein.AktuellerBestand,
                        StufId = ein.StufId,
                    };
                    if(ein == p[0])
                    {
                        einleer.IsFirst = true;
                    }
                    context.Einträge.Add(einleer);
                    context.RemoveRange(deleteobject);
                }
                context.SaveChanges();
                UpdateTabelle(this.BezeichnungSpace);
            }
           
        }
    }
}
