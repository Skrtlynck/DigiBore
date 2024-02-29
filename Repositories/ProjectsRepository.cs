using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DigiBore.Model;
using DigiBore.Repositories.Interfaces;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.WindowsAppSDK.Runtime.Packages;

namespace DigiBore.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    public ObservableCollection<Project> Projects { get; set; } = new();

    public ProjectsRepository()
    {
        string currentYear = DateTime.Now.Year.ToString();
        InitData(currentYear);
    }

    public void AddProject(Project project) => Projects.Add(project);

    public void InitData(string year)
    {
        var directories = Directory.GetDirectories("L:\\Technische data\\Dossiers\\Dossiers " + year);

        // Skip header
        directories = directories.Skip(1).ToArray();

        foreach (var projectPath in directories)
        {
            var project = projectPath.Split('\\');
            var projectInfo = project[4].Split("_");
            var lastEdited = Directory.GetLastWriteTime(projectPath);
            Projects.Add(new()
            {
                ProjectPath = projectPath,
                ProjectNumber = projectInfo[0],
                Customer = projectInfo[1],
                Location = projectInfo[2],
                LastEdited = lastEdited,
            });
        }
    }
}


