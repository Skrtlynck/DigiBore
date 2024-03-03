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
        InitData();
    }

    public void AddProject(Project project) => Projects.Add(project);

    public void InitData()
    {
        //werken van Geolab of werken van thuis
        //var directories = Directory.GetDirectories("L:\\Technische data\\Dossiers);
        var directories = Directory.GetDirectories("C:\\Technische data\\Dossiers");

        foreach (var jaarmap in directories)
        {
            // Skip voorbeeldfile

            var dossiers = Directory.GetDirectories(jaarmap);
            dossiers = dossiers.Skip(1).ToArray();

            foreach (var dossierPath in dossiers)
            {
                var project = dossierPath.Split('\\');
                var projectInfo = project[4].Split("_");
                var lastEdited = Directory.GetLastWriteTime(dossierPath);
                var jaar = jaarmap.Substring(jaarmap.Length - 2);
                Projects.Add(new()
                {
                    ProjectPath = dossierPath,
                    ProjectNumber = projectInfo[0],
                    Customer = projectInfo[1],
                    Location = projectInfo[2],
                    LastEdited = lastEdited,
                    ProjectYear = jaar,
                });
            }
        }
    }
}


