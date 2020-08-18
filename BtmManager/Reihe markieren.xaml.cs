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
    /// Interaktionslogik für Reihe_markieren.xaml
    /// </summary>
    public partial class Reihe_markieren : Window
    {
        public Reihe_markieren()
        {
            InitializeComponent();
        }

        private void btn_fertig_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            string zeilestr = tb_zeile.Text;
            bool zeilcor = Int32.TryParse(zeilestr, out int zeile);
            if (zeilcor)
            {

            }
        }else{

    }
}
