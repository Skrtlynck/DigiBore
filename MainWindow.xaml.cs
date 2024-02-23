using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DigiBore
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            string currentYear = DateTime.Now.Year.ToString();
            LoadProjectList(currentYear);
            Year.Text = currentYear;

        }

        private void LoadProjectList(string year) 
        {
            var directories = Directory.GetDirectories("L:\\Technische data\\Dossiers\\Dossiers " + year);
            ProjectList.ItemsSource = directories;
            Year.Text = year;
        }
        private void previousyearclick(object sender, RoutedEventArgs e)
        {
            string yearstring = Year.Text;
            int year = int.Parse(yearstring);
            if (year != 2023)
            {
                int newyear = year - 1;
                LoadProjectList(newyear.ToString());
                Error.Text = "";
            }
            else
            {
                Error.Text = "Geen data beschikbaar pre 2023, open deze bestanden met Geodrill";
            }


        }

        private void nextyearclick(object sender, RoutedEventArgs e)
        {
            string yearstring = Year.Text;
            int year = int.Parse(yearstring);
            if (year != DateTime.Now.Year)
            {
                int newyear = year + 1;
                LoadProjectList(newyear.ToString());
                Error.Text = "";
            }
            else
            {
                Error.Text = "Geen data beschikbaar uit de toekomst";
            }

        }
    }
}
