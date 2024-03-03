using CommunityToolkit.WinUI.UI.Controls;
using DigiBore.Model;
using DigiBore.ViewModels;
using DigiBore.Repositories;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace DigiBore.Views;

public sealed partial class ProjectsPage : Page
{
    private readonly ProjectsViewModel _projectsViewModel = App.GetService<ProjectsViewModel>();

    public ProjectsPage()
    {
        InitializeComponent();
        DataContext = _projectsViewModel;
        Year.Text = DateTime.Now.Year.ToString();
        filterBox.Text = DateTime.Now.Year.ToString().Substring(2);
        ApplyDataGridFilter();
    }

    private void previousyearclick(object sender, RoutedEventArgs e)
    {
        string yearstring = Year.Text;
        int year = int.Parse(yearstring);
        if (year != 2023)
        {
            int newyear = year - 1;
            Year.Text = newyear.ToString();
            Error.Text = "";
            filterBox.Text = newyear.ToString().Substring(2);
        }
        else
        {
            Error.Text = "Geen data beschikbaar pre 2023, open deze bestanden manueel met Geodrill";
        }


    }

    private void nextyearclick(object sender, RoutedEventArgs e)
    {
        string yearstring = Year.Text;
        int year = int.Parse(yearstring);
        if (year != DateTime.Now.Year)
        {
            int newyear = year + 1;
            Year.Text = newyear.ToString();
            Error.Text = "";
            filterBox.Text = newyear.ToString().Substring(2);
        }

        else
        {
            Error.Text = "Geen data beschikbaar uit de toekomst";
        }

    }
    private void DataGrid_Sorting(object sender, CommunityToolkit.WinUI.UI.Controls.DataGridColumnEventArgs e)
    {
        if (e.Column.Tag is not null)
        {
            var tag = e.Column.Tag.ToString();
            var direction = GetColumSortDirection(e.Column);

            var type = typeof(Project);
            var propertyInfo = type.GetProperty(tag!);

            dataGrid.ItemsSource = direction == DataGridSortDirection.Ascending
                       ? _projectsViewModel.Projects.Where(p => p.ProjectYear.ToLower().Contains(filterBox.Text.ToLower())).OrderBy(p => propertyInfo!.GetValue(p))
                       : _projectsViewModel.Projects.Where(p => p.ProjectYear.ToLower().Contains(filterBox.Text.ToLower())).OrderByDescending(p => propertyInfo!.GetValue(p));
            e.Column.SortDirection = direction;
            ClearColumSortDirections(tag!);
        }
    }

    private DataGridSortDirection GetColumSortDirection(DataGridColumn dataGridColumn)
    {
        return dataGridColumn.SortDirection is null || dataGridColumn.SortDirection == DataGridSortDirection.Descending
            ? DataGridSortDirection.Ascending
            : DataGridSortDirection.Descending;
    }

    private void ClearColumSortDirections(string columnToKeep)
    {
        foreach (var column in dataGrid.Columns)
        {
            if (column.Tag != columnToKeep)
            {
                column.SortDirection = null;
            }
        }
    }

    private void OpenFolder_Click(object sender, RoutedEventArgs e)
    {
        string folderpath = ((DigiBore.Model.Project)((Microsoft.UI.Xaml.FrameworkElement)sender).DataContext).ProjectPath;
        //Process.Start(folderpath);
        //dit werkt nog niet naar behoren, mss skippen
    }
    private void OpenProject_Click(object sender, RoutedEventArgs e)
    {
        //strijden voor nieuw boorprogramma
        var xmlpath = ((DigiBore.Model.Project)((Microsoft.UI.Xaml.FrameworkElement)sender).DataContext).ProjectPath + "\\" + ((DigiBore.Model.Project)((Microsoft.UI.Xaml.FrameworkElement)sender).DataContext).ProjectNumber + "_" + ((DigiBore.Model.Project)((Microsoft.UI.Xaml.FrameworkElement)sender).DataContext).Customer + "_" + ((DigiBore.Model.Project)((Microsoft.UI.Xaml.FrameworkElement)sender).DataContext).Location + ".xml";
        var number = ((DigiBore.Model.Project)((Microsoft.UI.Xaml.FrameworkElement)sender).DataContext).ProjectNumber;
        var customer = ((DigiBore.Model.Project)((Microsoft.UI.Xaml.FrameworkElement)sender).DataContext).Customer;
        var location = ((DigiBore.Model.Project)((Microsoft.UI.Xaml.FrameworkElement)sender).DataContext).Location;
        if (File.Exists(xmlpath))
        {
            Window projectWindow = new ProjectWindow(xmlpath, number, customer, location);
            projectWindow.Activate();
        }
        else
        {
            Window projectWindow = new ProjectWindow("NewFile", number, customer, location);
            projectWindow.Activate();
        }
    }

    private void CreateRapport_Click(object sender, RoutedEventArgs e)
    {
        //vanaf folder openen gaat via een klik, via deze knop de map rapport templates laten openen
        //Process.Start(@"L:\Technische data\Rapport Templates);
    }

    private void ApplyDataGridFilterNu(object sender, TextChangedEventArgs e)
    {
        ApplyDataGridFilter();
    }

    private void ApplyDataGridFilter()
    {
        if (filterBox.Text is not null)
        {
            dataGrid.ItemsSource = _projectsViewModel.Projects.Where(p => p.ProjectYear.ToLower().Contains(filterBox.Text.ToLower()));
        }
    }
}


