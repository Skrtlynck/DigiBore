using ABI.Windows.Foundation;
using CommunityToolkit.WinUI.UI.Controls;
using DigiBore.Model;
using DigiBore.Repositories;
using DigiBore.Views;
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
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            SetContentPage(typeof(HomePage).FullName!);
        }
        public void SetContentPage(string pageName)
        {
            var pageType = Type.GetType(pageName);
            contentFrame.Navigate(pageType);
        }
        private void LoadProjectsButtonClick(object sender, RoutedEventArgs e)
        {
            LoadProjectsButton.Visibility = Visibility.Collapsed;
            SetContentPage(typeof(ProjectsPage).FullName!);
        }
    }
}
