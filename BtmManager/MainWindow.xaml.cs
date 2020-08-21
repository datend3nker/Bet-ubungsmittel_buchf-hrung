using BtmManager.Data;
using BtmManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public string Produktbezeichnung;
        public string MaterialName;
        public string Bezeichnung;
        public MainWindow()
        {
            InitializeComponent();
            UpdateTreeView();

        }

        public void UpdateTabelle(string suche)
        {
            if(suche == null)
            {
                return;
            }
            using (BtmContext context = new BtmContext())
            {
                var result = (from Eintrag in context.Einträge
                              where Eintrag.Bezeichnung == suche
                              select Eintrag).ToList();
                this.DataGrid.ItemsSource = result;
                var Markierung = from Eintrag in context.Einträge
                                 where Eintrag.Bezeichnung == suche && Eintrag.WurdeMarkiert == true
                                 select Eintrag;
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
                    ContextMenu contextProject = new ContextMenu();
                    MenuItem itemProject = new MenuItem();
                    TreeViewItem newProject = new TreeViewItem
                    {
                        Header = proj.Produktbezeichnung
                    };
                    this.Produktbezeichnung = proj.Produktbezeichnung;
                    foreach (var stuf in stu)
                    {
                        if (stuf.ProjektId == proj.ProjektId)
                        {
                            ContextMenu contextStufe = new ContextMenu();
                            MenuItem itemStufe = new MenuItem();
                            TreeViewItem newStufe = new TreeViewItem
                            {
                                Header = stuf.MaterialName
                            };
                            this.MaterialName = stuf.MaterialName;
                            itemStufe.Header = "Löschen";
                            itemStufe.Click += Stufe_Löschen;
                            contextStufe.Items.Add(itemStufe);
                            newStufe.ContextMenu = contextStufe;
                            newProject.Items.Add(newStufe);
                            foreach (var eint in ein)
                            {
                                if (eint.StufId == stuf.StufId)
                                {
                                    if (eint.IsFirst == true)
                                    {
                                        ContextMenu contextEintrag = new ContextMenu();
                                        MenuItem itemEintrag = new MenuItem();
                                        TreeViewItem newEintrag = new TreeViewItem
                                        {
                                            Header = eint.Bezeichnung,
                                        };
                                        this.Bezeichnung = eint.Bezeichnung;
                                        itemEintrag.Header = "Löschen";
                                        itemEintrag.Click += Eintrag_Löschen;
                                        contextEintrag.Items.Add(itemEintrag);
                                        newEintrag.ContextMenu = contextEintrag;
                                        newEintrag.Selected += NewEintrag_Selected;
                                        newStufe.Items.Add(newEintrag);
                                    }
                                }
                            }
                        }

                    }
                    itemProject.Header = "Löschen";
                    itemProject.Click += Projekt_Löschen;
                    contextProject.Items.Add(itemProject);
                    newProject.ContextMenu = contextProject;
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
            l_aktion.Content = "erstelle neues Projekt";
            NeuesProjekt neuesprojekt = new NeuesProjekt();
            neuesprojekt.ShowDialog();
            UpdateTreeView();
            l_aktion.Content = "";
        }

        private void m_beenden_Click(object sender, RoutedEventArgs e)
        {
            l_aktion.Content = "Program wird beendet";
            Application.Current.Shutdown();
        }

        private void m_stufe_Click(object sender, RoutedEventArgs e)
        {
            l_aktion.Content = "erstelle neue Stufe";
            NeueStufe neuestufe = new NeueStufe();
            neuestufe.ShowDialog();
            UpdateTreeView();
            l_aktion.Content = "";
        }

        private void m_logbuch_Click(object sender, RoutedEventArgs e)
        {
            l_aktion.Content = "erstelle neues Logbuch";
            NeuerEntrag neuereintrag = new NeuerEntrag();
            neuereintrag.ShowDialog();
            UpdateTreeView();
        }

        private void m_übertragen_Click(object sender, RoutedEventArgs e)
        {
            pb_progress.Value = 0;
            l_aktion.Content = "Daten werden Übertragen";
            using (BtmContext context = new BtmContext())
            {
                List<Eintrag> p = this.DataGrid.Items.OfType<Eintrag>().ToList();
                if(p.Count == 0)
                {
                    l_aktion.Content = "";
                    return;
                }
                var deleteobject = from Eintrag in context.Einträge
                                   where Eintrag.Bezeichnung == this.BezeichnungSpace
                                   select Eintrag; 
                context.RemoveRange(deleteobject);
                float progress = 100 / p.Count;
                foreach (var ein in p)
                {
                    Eintrag einleer = new Eintrag()
                    {
                        Einheit = EinheitSpace,
                        Bezeichnung = BezeichnungSpace,
                        EintragId = ein.EintragId,
                        LfdNr = ein.LfdNr,
                        Datum = ein.Datum,
                        Anfangsbestand = ein.Anfangsbestand,
                        TheroZugang = ein.TheroZugang,
                        PrakZugang = ein.PrakZugang,
                        Arbeitsverlust = ein.Arbeitsverlust,
                        Abgang = ein.Abgang,
                        AktuellerBestand = ein.AktuellerBestand,
                        StufId = ein.StufId,
                        WurdeMarkiert = ein.WurdeMarkiert,
                        Farbe = ein.Farbe,
                    };
                    if(ein == p[0])
                    {
                        einleer.IsFirst = true;
                    }
                    context.Einträge.Add(einleer);
                    pb_progress.Value += progress;
                }
                context.SaveChanges();
                UpdateTabelle(this.BezeichnungSpace);

            }
            l_aktion.Content = "";
            pb_progress.Value = 0;
        }

        private void btn_berechnen_Click(object sender, RoutedEventArgs e)
        {
            pb_progress.Value = 0;
            l_aktion.Content = "Tabelle wird berechnet";
            this.speichern();
            float calc = 0;
            using (BtmContext context = new BtmContext())
            {
                List<Eintrag> p = this.DataGrid.Items.OfType<Eintrag>().ToList();
                if (p.Count == 0)
                {
                    l_aktion.Content = "";
                    return;
                }
                float progress = 100 / p.Count;
                foreach (var ein in p)
                {
                    var entry = context.Einträge.FirstOrDefault(item => item.EintragId == ein.EintragId);
                    
                    var x = p[0].Equals(ein);
                    if (!x)
                    {
                        entry.Anfangsbestand = calc;
                    }
                    entry.AktuellerBestand = entry.Anfangsbestand + entry.PrakZugang - entry.Arbeitsverlust - entry.Abgang;
                    context.Einträge.Update(entry);
                    context.SaveChanges();
                    calc = entry.Anfangsbestand + entry.PrakZugang - entry.Arbeitsverlust - entry.Abgang;
                    pb_progress.Value += progress;
                }
               
            }
            UpdateTabelle(this.BezeichnungSpace);
            l_aktion.Content = "";
            pb_progress.Value = 0;
        }
        void speichern()
        {
            pb_progress.Value = 0;
            l_aktion.Content = "Datenbank wird gespeichert";
            using (BtmContext context = new BtmContext())
            {
                List<Eintrag> p = this.DataGrid.Items.OfType<Eintrag>().ToList();
                if (p.Count == 0)
                {
                    l_aktion.Content = "";
                    return;
                }
                var deleteobject = from Eintrag in context.Einträge
                                   where Eintrag.Bezeichnung == this.BezeichnungSpace
                                   select Eintrag;
                context.RemoveRange(deleteobject);
                float progress = 100 / p.Count;
                foreach (var ein in p)
                {
                    Eintrag einleer = new Eintrag()
                    {
                        Einheit = EinheitSpace,
                        Bezeichnung = BezeichnungSpace,
                        EintragId = ein.EintragId,
                        LfdNr = ein.LfdNr,
                        Datum = ein.Datum,
                        Anfangsbestand = ein.Anfangsbestand,
                        TheroZugang = ein.TheroZugang,
                        PrakZugang = ein.PrakZugang,
                        Arbeitsverlust = ein.Arbeitsverlust,
                        Abgang = ein.Abgang,
                        AktuellerBestand = ein.AktuellerBestand,
                        StufId = ein.StufId,
                        WurdeMarkiert = ein.WurdeMarkiert,
                        Farbe = ein.Farbe,
                    };
                    if (ein == p[0])
                    {
                        einleer.IsFirst = true;
                    }
                    context.Einträge.Add(einleer);
                    pb_progress.Value += progress;
                }
                context.SaveChanges();
                UpdateTabelle(this.BezeichnungSpace);
                l_aktion.Content = "";
            }
            pb_progress.Value = 0;
        }

        private void m_markieren_Click(object sender, RoutedEventArgs e)
        {
            l_aktion.Content = "Markiere Zeile";
            Reihe_markieren markieren = new Reihe_markieren(this.DataGrid);
            markieren.ShowDialog();
            this.UpdateTabelle(BezeichnungSpace);
            l_aktion.Content = "";
        }

        private void m_info_Click(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            info.ShowDialog();
        }


        private void Projekt_Löschen(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Alle dazugehörigen Stufen und Einträge werden unwiderruflich gelöscht!!! \n Sind sie sicher?", "Warnung", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    using (BtmContext context = new BtmContext())
            {
                var deleteProject = from Projekt in context.Projekte
                                    where Projekt.Produktbezeichnung == this.Produktbezeichnung
                                    select Projekt;
                var deleteProjectlist = (from Projekt in context.Projekte
                                    where Projekt.Produktbezeichnung == this.Produktbezeichnung
                                    select Projekt).ToList();
                var deleteStufe = from Stufe in context.Stufen
                                    where Stufe.ProjektId == deleteProjectlist[0].ProjektId
                                    select Stufe;
                var deleteStufelist = (from Stufe in context.Stufen
                                  where Stufe.ProjektId == deleteProjectlist[0].ProjektId
                                  select Stufe).ToList();
                var deleteEintrag = from Eintrag in context.Einträge
                                  where Eintrag.StufId == deleteStufelist[0].StufId
                                  select Eintrag;

                context.RemoveRange(deleteProject);
                context.RemoveRange(deleteStufe);
                context.RemoveRange(deleteEintrag);
                context.SaveChanges();
            }
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
            this.UpdateTreeView();            
        }

        private void Stufe_Löschen(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Alle dazugehörigen Logbücher werden unwiderruflich gelöscht!!! \n Sind sie sicher?", "Warnung", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    using (BtmContext context = new BtmContext())
                    {
                        var deleteStufe = from Stufe in context.Stufen
                                          where Stufe.MaterialName == this.MaterialName
                                          select Stufe;
                        var deleteStufelist = (from Stufe in context.Stufen
                                               where Stufe.MaterialName == this.MaterialName
                                               select Stufe).ToList();
                        var deleteEintrag = from Eintrag in context.Einträge
                                            where Eintrag.StufId == deleteStufelist[0].StufId
                                            select Eintrag;

                        context.RemoveRange(deleteStufe);
                        context.RemoveRange(deleteEintrag);
                        context.SaveChanges();
                    }
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
            this.UpdateTreeView();
        }

        private void Eintrag_Löschen(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Dieses Logbuch wird unwiderruflich gelöscht!!! \n Sind sie sicher?", "Warnung", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    using (BtmContext context = new BtmContext())
                    {
                        var deleteEintrag = from Eintrag in context.Einträge
                                            where Eintrag.Bezeichnung == this.Bezeichnung
                                            select Eintrag;

                        context.RemoveRange(deleteEintrag);
                        context.SaveChanges();
                    }
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
            this.UpdateTreeView();
        }
    }
}
