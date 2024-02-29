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

namespace DigiBore.Views;

public sealed partial class ProjectPage : Page
{
    private readonly ProjectsViewModel _projectsViewModel = App.GetService<ProjectsViewModel>();

    public ProjectPage()
    {
        InitializeComponent();
        DataContext = _projectsViewModel;
        Year.Text = DateTime.Now.Year.ToString();
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
            
            //hier nog fixen dat er nieuwe projecten worden geladen OF dat alle projecten worden geladen bij openen, en dat de grid nieuwe itemssource krijgt voor bijhorend jaar
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
            
            //hier nog fixen dat er nieuwe projecten worden geladen OF dat alle projecten worden geladen bij openen, en dat de grid nieuwe itemssource krijgt voor bijhorend jaar

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
                       ? _projectsViewModel.Projects.OrderBy(p => propertyInfo!.GetValue(p))
                       : _projectsViewModel.Projects.OrderByDescending(p => propertyInfo!.GetValue(p));

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


}


