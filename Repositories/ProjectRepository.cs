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

public class ProjectRepository : IProjectRepository
{
    public void AddProject(Project project) => Projects.Add(project);

}

