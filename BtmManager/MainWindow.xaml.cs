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
            UpdateTreeView();
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
        public void UpdateTabelle()
        {
            using (BtmContext context = new BtmContext())
            {
                IQueryable<Eintrag> result = from Eintrag in context.Einträge select Eintrag;
                DataGrid.ItemsSource = result.ToList();
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
                            foreach(var eint in ein)
                            {
                                if(eint.StufId == stuf.StufId)
                                {
                                    TreeViewItem newEintrag = new TreeViewItem
                                    {
                                        Header = eint.Bezeichnung
                                    };
                                    newStufe.Items.Add(newEintrag);
                                }
                            }
                        }
                        
                    }
                    TreeView.Items.Add(newProject);
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
    }
}
